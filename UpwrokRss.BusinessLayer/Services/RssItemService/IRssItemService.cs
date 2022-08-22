
using UpwrokRss.BusinessLayer.Entities;
using UpwrokRss.BusinessLayer.Models;

namespace UpwrokRss.BusinessLayer.Services;

public interface IRssItemService
{
    Task<int> SaveNewItems(long feedId, IEnumerable<RssItem> items);
    Task<List<RssItem>> List(long feedId, Pagination pagination);
    Task<int> Count(long feedId);
    Task<RssItem?> Get(long id);
    Task Hide(RssItem item);
    Task Read(RssItem item);
}