using LightRssReader.BusinessLayer.Entities;

namespace LightRssReader.BusinessLayer.Services;

public interface IRssPostService
{
    Task<int> SaveNewItems(long feedId, IEnumerable<RssPost> items);
    Task<List<RssPost>> List(RssPostFilters filters);
    Task<int> Count(RssPostFilters filters);
    Task<RssPost?> Get(long id);
    Task Hide(RssPost item);
    Task Read(RssPost item);
}