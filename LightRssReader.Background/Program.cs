using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LightRssReader.Background;
using LightRssReader.Background.Configurations;
using LightRssReader.BusinessLayer.Configurations;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
var environmentName = args.Length > 0 ? args[0] : null;
if (!string.IsNullOrEmpty(environmentName))
{
    builder.ConfigureAppSettings(environmentName);
}
var configuration = builder
    .AddUserSecrets<Program>()
    .Build();

var services = new ServiceCollection();

services.AddScoped<ReadRssJob>();
services.ConfigureDb(configuration.GetConnectionString("DefaultConnection"));
services.ConfigureServices();
services.ConfigureOptions(configuration);
services.ConfigureMapper();

services.AddLogging(configure => configure
    .AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss ";
    })
    .AddFilter("Microsoft", LogLevel.Warning)
    .AddFilter("System", LogLevel.Warning)
    .AddFilter("Read RSS", LogLevel.Trace));

var serviceProvider = services.BuildServiceProvider();

var errorCounter = 0;
while (true && errorCounter < 3)
{
    using var scope = serviceProvider.CreateScope();

    var readRssJob = scope.ServiceProvider.GetRequiredService<ReadRssJob>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        await readRssJob.Execute();
        errorCounter = 0;
    }
    catch (Exception e)
    {
        logger.LogError(e, "ReadRssJob failed");
        errorCounter += 1;
    }

    await Task.Delay(1000 * 60 * 3);
}