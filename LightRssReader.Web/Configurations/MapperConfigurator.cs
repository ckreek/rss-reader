using AutoMapper;
using LightRssReader.BusinessLayer.Configurations;
using LightRssReader.Web.Dto;
using LightRssReader.BusinessLayer.Entities;

namespace LightRssReader.Web.Configurations;

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
        CreateMap<RssPost, RssPostDto>();
        CreateMap<Feed, FeedDto>();
        CreateMap<PostFeedDto, Feed>();
    }
}