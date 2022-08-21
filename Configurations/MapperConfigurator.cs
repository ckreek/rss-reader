using AutoMapper;
using upwork_rss.Dto;
using upwork_rss.Entities;

namespace upwork_rss.Configurations;

public static class MapperConfigurator
{
    public static void ConfigureMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<RssItem, RssItemDto>();
        });

        services.AddScoped(m => config.CreateMapper());
    }
}
