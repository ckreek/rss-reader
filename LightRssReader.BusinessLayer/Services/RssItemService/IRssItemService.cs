using LightRssReader.BusinessLayer.Entities;

namespace LightRssReader.BusinessLayer.Services;

public interface IRssItemService
{
    Task<int> SaveNewItems(long feedId, IEnumerable<RssItem> items);
    Task<List<RssItem>> List(RssItemFilters filters);
    Task<int> Count(RssItemFilters filters);
    Task<RssItem?> Get(long id);
    Task Hide(RssItem item);
    Task Read(RssItem item);
}