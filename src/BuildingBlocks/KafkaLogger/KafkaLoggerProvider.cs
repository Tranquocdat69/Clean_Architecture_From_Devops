using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ECom.BuildingBlocks.LogLib.KafkaLogger.Configs;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace ECom.BuildingBlocks.LogLib.KafkaLogger
#nullable disable
{
    /// <summary>
    /// Class format lại log thành log message và đẩy lên kafka
    /// </summary>
    public sealed class KafkaLoggerProvider : ILoggerProvider
    {
        private readonly KafkaLoggerProducer _producer;
        private KafkaLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, KafkaLogger> _logger = new ConcurrentDictionary<string, KafkaLogger>();

        public KafkaLoggerProvider(IOptions<KafkaLoggerConfiguration> options, KafkaLoggerProducer producer)
        {
            _config   = GetKafkaLoggerConfiguration(options);
            _producer = producer;
        }
        public ILogger CreateLogger(string categoryName)
            => _logger.GetOrAdd(categoryName, name => new KafkaLogger(_producer, _config, name));

        public void Dispose()
        {
            _producer.Dispose();
            _logger.Clear();
        }

        private KafkaLoggerConfiguration GetKafkaLoggerConfiguration(IOptions<KafkaLoggerConfiguration> options)
        {
            var config = options.Value;
            foreach (var keyValue in config.Targets)
            {
                var logTemplateFormat                          = GetTargetLogTemplateFormat(keyValue.Value);
                config.Targets[keyValue.Key].LogTemplateFormat = logTemplateFormat;
            }
            return config;
        }

        private LogLevelNameFormat GetLogLevelNameFormat(string mStr)
        {
            var logLevelFormat = new LogLevelNameFormat();
            var truncateShort  = GetShortParam("truncate", mStr);
            if (truncateShort > 0)
            {
                logLevelFormat.TruncateShort = truncateShort;
            }
            else
            {
                var truncateStr  = GetStringParam("truncate", mStr);
                if (truncateStr != null && truncateStr.Equals("short"))
                {
                    logLevelFormat.TruncateString = "short";
                }
            }
            logLevelFormat.Uppercase = GetBoolParam("uppercase", mStr);
            return logLevelFormat;
        }

        private LogTemplateFormat GetTargetLogTemplateFormat(Target target)
        {
            var targetLogTemplateFormat = new LogTemplateFormat();
            var template                = target.LogTemplate;
            var matches                 = Regex.Matches(template, "{[^}]+}");
            foreach (Match m in matches)
            {
                string toReplace = m.Value;
                var mStr = Regex.Replace(toReplace, "[{}]", "");
                var tags = mStr.Split(':');
                switch (tags[0])
                {
                    case "level":
                        targetLogTemplateFormat.LevelNameFormat = GetLogLevelNameFormat(mStr);
                        break;
                    case "date":
                        targetLogTemplateFormat.DatetimeFormat = GetDatetimeFormat(mStr);
                        break;
                }
            }
            return targetLogTemplateFormat;
        }

        private string GetDatetimeFormat(string mStr)
        {
            string formatStr = GetStringParam("format", mStr, ".+");
            if (!string.IsNullOrEmpty(formatStr))
            {
                return formatStr;
            }
            return "yyyy-MM-ddTHH:mm:ss.ffff";
        }

        private bool GetBoolParam(string key, string paramStr)
        {
            var m = Regex.Match(paramStr, key + "=" + "[^:]+");
            if (!string.IsNullOrEmpty(m.Value))
            {
                var value = m.Value.Split('=')[1];
                return value.ToLower().Equals("true");
            }
            return false;
        }

        private short GetShortParam(string key, string paramStr)
        {
            var m = Regex.Match(paramStr, key + "=" + "[^:]+");
            if (!string.IsNullOrEmpty(m.Value))
            {
                var valueStr = m.Value.Split('=')[1];
                try
                {
                    var value = short.Parse(valueStr);
                    return value;
                }
                catch { }
            }
            return 0;
        }

        private string GetStringParam(string key, string paramStr, string pattern = "[^:]+")
        {
            var m = Regex.Match(paramStr, key + "=" + pattern);
            if (!string.IsNullOrEmpty(m.Value))
            {
                var valueStr = m.Value.Split('=')[1];
                return valueStr;
            }
            return null;
        }
    }
}
