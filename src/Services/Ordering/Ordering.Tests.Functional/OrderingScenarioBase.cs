namespace Ordering.Tests.Functional
#nullable disable
{
    public class OrderingScenarioBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(OrderingScenarioBase))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<Startup>()
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<RouteOptions>(hostContext.Configuration);
                    services.UseServiceCollectionConfiguration(hostContext.Configuration);
                });

            var testServer = new TestServer(hostBuilder);

            return testServer;
        }
    }
    public static class Get
    {
        public static string Orders = "api/Order";
    }
    public static class Delete
    {
        public static string DeleteOrders = "api/Order";
    }

}
