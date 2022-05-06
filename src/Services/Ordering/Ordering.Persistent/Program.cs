IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.UseServiceCollectionConfiguration(configuration);
    })
    .Build();

await host
    .UseHostConfiguration(configuration)
    .RunAsync();
