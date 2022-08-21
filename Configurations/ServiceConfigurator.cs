using upwork_rss.Services;

namespace upwork_rss.Configurations;

public static class ServiceConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<UpworkRssClient>();
        services.AddScoped<IRssItemService, RssItemService>();
    }

    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
    }
}
