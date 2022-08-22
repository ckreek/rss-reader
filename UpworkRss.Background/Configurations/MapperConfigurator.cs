using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using UpworkRss.BusinessLayer.Configurations;

namespace UpworkRss.Background.Configurations;

public static class MapperConfigurator
{
    public static void ConfigureMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
          cfg.AddProfile<BusinessLayerMapperProfile>();
        });

        services.AddScoped(m => config.CreateMapper());
    }
}
