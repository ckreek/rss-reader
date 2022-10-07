using LightRssReader.BusinessLayer.Entities;

namespace LightRssReader.BusinessLayer.Services;

public interface IRssPostService
{
    Task<int> SaveNewRssPosts(long feedId, IEnumerable<RssPost> rssPosts);
    Task<List<RssPost>> List(RssPostFilters filters);
    Task<int> Count(RssPostFilters filters);
    Task<RssPost?> Get(long id);
    Task Hide(RssPost rssPost);
    Task Read(RssPost rssPost);
}