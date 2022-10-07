using Microsoft.Extensions.DependencyInjection;
using LightRssReader.BusinessLayer.Services;
using LightRssReader.BusinessLayer.Models;
using Microsoft.Extensions.Configuration;

namespace LightRssReader.BusinessLayer.Configurations;

public static class ServiceConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<RssClient>();
        services.AddScoped<IRssPostService, RssPostService>();
        services.AddScoped<IFeedService, FeedService>();
        services.AddScoped<ITelegramClient, TelegramClient>();
    }

    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TelegramOptions>(configuration.GetSection(nameof(TelegramOptions)));
    }
}
