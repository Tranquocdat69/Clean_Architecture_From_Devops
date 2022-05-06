namespace ECom.Services.Ordering.Persistent.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public const string DB_CONNECTION_KEY = "OrderDB";
        public static IServiceCollection UseServiceCollectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddPersistentConfiguration(configuration)
                .AddSingletonServices(configuration);
        }

        private static IServiceCollection AddSingletonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IKafkaConsumer<Null, string>, KafkaConsumer<Null, string>>(sp =>
            {
                var kafka = configuration.GetSection("Kafka");
                var consumerConfig = new KafkaConsumerConfig
                {
                    BootstrapServers = kafka["BootstrapServers"],
                    GroupId = kafka["PersistentGroupId"],
                    EnableAutoCommit = false
                };
                return new KafkaConsumer<Null, string>(consumerConfig);
            });
            services.AddSingleton<IConsumerTask<Null, string>, ConsumerTask<Null, string>>();
            return services;
        }
        private static IServiceCollection AddPersistentConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KafkaOffsetDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString(DB_CONNECTION_KEY)));
            return services;
        }
    }
}
