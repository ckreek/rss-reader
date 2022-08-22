using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UpworkRss.Background;
using UpworkRss.Background.Configurations;
using UpworkRss.BusinessLayer.Configurations;

var services = new ServiceCollection();

services.AddScoped<ReadRssJob>();
services.ConfigureDb("upwork-rss.db");
services.ConfigureServices();
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

while (true)
{
    using var scope = serviceProvider.CreateScope();

    var readRssJob = scope.ServiceProvider.GetRequiredService<ReadRssJob>();

    await readRssJob.Execute();

    await Task.Delay(1000 * 60 * 3);
}