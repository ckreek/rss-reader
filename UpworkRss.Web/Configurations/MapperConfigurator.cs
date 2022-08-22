using AutoMapper;
using UpworkRss.Web.Dto;
using UpworkRss.Web.Entities;
using UpworkRss.Web.Services;

namespace UpworkRss.Web.Configurations;

public static class MapperConfigurator
{
  public static void ConfigureMapper(this IServiceCollection services)
  {
    var config = new MapperConfiguration(cfg =>
    {
      cfg.CreateMap<RssItem, RssItemDto>();
      cfg.CreateMap<UpworkRssItemModel, RssItem>()
        .ForMember(x => x.PublishDate, x => x.MapFrom(y => y.PublishDate.UtcDateTime))
        .ForMember(x => x.Url, x => x.MapFrom(y => y.Id))
        .ForMember(x => x.Id, x => x.Ignore());
      cfg.CreateMap<Feed, FeedDto>();
    });

    services.AddScoped(m => config.CreateMapper());
  }
}
