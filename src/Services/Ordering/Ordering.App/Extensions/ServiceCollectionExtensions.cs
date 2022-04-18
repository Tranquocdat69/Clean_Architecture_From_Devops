﻿namespace ECom.Services.Ordering.App.Extensions
#nullable disable
{
    public static class ServiceCollectionExtensions
    {
        private const string DB_CONNECTION_KEY = "OrderDB";
        public static IServiceCollection UseServiceCollectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddMediatR(typeof(CreateOrderCommand))
                .AddLoggerConfiguration(configuration)
                .AddPersistentConfiguration(configuration)
                .AddOrderRingBuffer(configuration);
        }

        private static IServiceCollection AddLoggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(options =>
            {
                var allowConsoleLog = configuration.GetValue("AllowConsoleLog", false);
                if (!allowConsoleLog)
                {
                    options.ClearProviders();
                }
                options.AddKafkaLogger(config =>
                {
                    var loggerConfiguration = configuration.GetSection("KafkaLogger").Get<KafkaLoggerConfiguration>();
                    config.BootstrapServers = loggerConfiguration.BootstrapServers;
                    config.Targets          = loggerConfiguration.Targets;
                    config.Rules            = loggerConfiguration.Rules;
                });
            });
            return services;
        }

        private static IServiceCollection AddPersistentConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext Configuration
            var dbConnectionString = configuration.GetConnectionString(DB_CONNECTION_KEY);
            services.AddDbContext<OrderDbContext>(options =>
            {
                if(dbConnectionString == null)
                {
                    options.UseInMemoryDatabase(DB_CONNECTION_KEY);
                }
                else
                {
                    options.UseSqlServer(dbConnectionString);
                }
            });
            #endregion
            return services;
        }
    }
}