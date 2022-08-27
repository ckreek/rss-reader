using AutoMapper;
using UpworkRss.BusinessLayer.Configurations;
using UpworkRss.Web.Dto;
using UpwrokRss.BusinessLayer.Entities;

namespace UpworkRss.Web.Configurations;

public static class MapperConfigurator
{
    public static void ConfigureMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
          cfg.AddProfile<WebMapperProfile>();
          cfg.AddProfile<BusinessLayerMapperProfile>();
        });

        services.AddScoped(m => config.CreateMapper());
    }
}


public class WebMapperProfile : Profile
{
    public WebMapperProfile()
    {
        CreateMap<RssItem, RssItemDto>();
        CreateMap<Feed, FeedDto>();
        CreateMap<FeedCreateDto, Feed>();
    }
}