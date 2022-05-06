namespace ECom.Services.Ordering.Persistent.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IHost UseHostConfiguration(this IHost host, IConfiguration configuration)
        {
             return host
                .AddBackgroundTasks(configuration);
        }

        private static IHost AddBackgroundTasks(this IHost host, IConfiguration configuration)
        {
            using(var scope = host.Services.CreateScope())
            {
                var persistentTopic = configuration.GetSection("Kafka")["PersistentTopic"];
                var task = scope.ServiceProvider.GetRequiredService<IConsumerTask<Null,string>>();
                var persistentService = new ConsumePersistentRequestService(task, persistentTopic);
                var persistentService1 = new ConsumePersistentRequestService(task, "ngocth");

                persistentService.Execute();
                persistentService1.Execute();
            }
            return host;
        }
    }
}
