using AutoMapper;
using LightRssReader.BusinessLayer.Entities;
using LightRssReader.BusinessLayer.Services;

namespace LightRssReader.BusinessLayer.Configurations;

public class BusinessLayerMapperProfile : Profile
{
    public BusinessLayerMapperProfile()
    {
        CreateMap<RssPostModel, RssPost>()
                .ForMember(x => x.PublishDate, x => x.MapFrom(y => y.PublishDate.UtcDateTime))
                .ForMember(x => x.Url, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Id, x => x.Ignore());
    }
}

