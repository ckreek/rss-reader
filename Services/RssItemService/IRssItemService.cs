
using upwork_rss.Entities;

namespace upwork_rss.Services;

public interface IRssItemService
{
    Task SaveNewItems(IEnumerable<RssItem> items);
    Task<List<RssItem>> List();
}