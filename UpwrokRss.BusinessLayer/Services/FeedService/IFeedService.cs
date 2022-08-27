
using UpwrokRss.BusinessLayer.Entities;

namespace UpwrokRss.BusinessLayer.Services;

public interface IFeedService
{
    Task<List<Feed>> List();
    Task<Feed?> Get(long id);
    Task<Feed> Update(Feed feed);
    Task<Feed> Add(Feed feed);
    Task Delete(Feed feed);
}