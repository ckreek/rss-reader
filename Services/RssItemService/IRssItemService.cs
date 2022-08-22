
using upwork_rss.Entities;

namespace upwork_rss.Services;

public interface IRssItemService
{
    Task SaveNewItems(long feedId, IEnumerable<RssItem> items);
    Task<List<RssItem>> List(long feedId);
    Task<RssItem?> Get(long id);
    Task Hide(RssItem item);
}