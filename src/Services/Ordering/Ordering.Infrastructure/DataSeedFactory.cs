namespace FPTS.FIT.BDRD.Services.Ordering.Infrastructure
{
    public class DataSeedFactory
    {
        public Task CreateSeed(OrderDbContext context, ILogger<DataSeedFactory> logger, IOrderRepository repository)
        {
            var policy = CreatePolicy(logger, nameof(DataSeedFactory));
            policy.ExecuteAsync(() => {
                // Thêm data seed ở đây
                // Order hiện tại ko cần
                AddDataToCache(context.Orders, repository);
                return Task.CompletedTask;
            });
            return Task.CompletedTask;  
        }

        private void AddDataToCache(IEnumerable<Order> orders, IOrderRepository repository)
        {
            foreach(var order in orders)
            {
                repository.Add(order);
            }
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<DataSeedFactory> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<Exception>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
