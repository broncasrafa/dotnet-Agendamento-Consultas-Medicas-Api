using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logger) =>
    {
        IHostEnvironment env = context.HostingEnvironment;
        IConfigurationSection config = context.Configuration.GetSection("Logging");
        logger.AddConfiguration(config);
    })
    .Build();

await host.RunAsync();
