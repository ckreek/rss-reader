using AutoMapper;
using upwork_rss.Dto;
using upwork_rss.Entities;
using upwork_rss.Services;

namespace upwork_rss.Configurations;

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
    });

    services.AddScoped(m => config.CreateMapper());
  }
}
