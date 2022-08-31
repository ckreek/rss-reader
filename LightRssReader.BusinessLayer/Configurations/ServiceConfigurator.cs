using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LightRssReader.BusinessLayer.Services;

namespace LightRssReader.BusinessLayer.Configurations;

public static class ServiceConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<RssClient>();
        services.AddScoped<IRssPostService, RssPostService>();
        services.AddScoped<IFeedService, FeedService>();
    }

    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
    }
}
