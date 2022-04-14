using System.Collections.Concurrent;

namespace ECom.BuildingBlocks.MessageQueue.KafkaMessageQueue;

#nullable disable
public class InMemoryRequestManagement
{
    private static ConcurrentDictionary<string, Data> _store = new ConcurrentDictionary<string, Data>();

    public string GenernateRequestId()
    {
        var requestId = Guid.NewGuid().ToString();
        _store.TryAdd(requestId, new Data
        {
            AutoReset = new AutoResetEvent(false)
        });
        return requestId;
    }

    public void SetResponse(string requestId, object response)
    {
        var hasValue = _store.TryGetValue(requestId, out Data data);
        if (hasValue)
        {
            data.Response = response;
            data.AutoReset.Set();
        }
    }

    public async Task<object> GetResponseAsync(string requestId, int millisecondsTimeout = 8000)
    {
        Func<object> waitResponse = () =>
        {
            bool hasValue = _store.TryGetValue(requestId, out Data data);
            if (hasValue)
            {
                data.AutoReset.WaitOne(millisecondsTimeout);
                _store.Remove(requestId, out data);
                return data.Response;
            }
            return null;
        };
        Task<object> task = new Task<object>(waitResponse);
        task.Start();
        return await task;
    }

    private class Data
    {
        public AutoResetEvent AutoReset { get; set; }
        public object Response { get; set; }
    }
}