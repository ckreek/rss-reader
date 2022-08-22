using AutoMapper;
using UpwrokRss.BusinessLayer.Entities;
using UpwrokRss.BusinessLayer.Services;

namespace UpworkRss.BusinessLayer.Configurations;

public class BusinessLayerMapperProfile : Profile
{
    public BusinessLayerMapperProfile()
    {
        CreateMap<UpworkRssItemModel, RssItem>()
                .ForMember(x => x.PublishDate, x => x.MapFrom(y => y.PublishDate.UtcDateTime))
                .ForMember(x => x.Url, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Id, x => x.Ignore());
    }
}

