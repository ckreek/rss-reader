
using UpworkRss.Web.Entities;
using UpworkRss.Web.Models;

namespace UpworkRss.Web.Services;

public interface IRssItemService
{
    Task SaveNewItems(long feedId, IEnumerable<RssItem> items);
    Task<List<RssItem>> List(long feedId, Pagination pagination);
    Task<int> Count(long feedId);
    Task<RssItem?> Get(long id);
    Task Hide(RssItem item);
    Task Read(RssItem item);
}