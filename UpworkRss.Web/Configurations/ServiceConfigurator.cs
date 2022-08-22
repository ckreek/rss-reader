using UpworkRss.Web.Services;

namespace UpworkRss.Web.Configurations;

public static class ServiceConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<UpworkRssClient>();
        services.AddScoped<IRssItemService, RssItemService>();
        services.AddScoped<IFeedService, FeedService>();
    }

    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
    }
}
