
using LightRssReader.BusinessLayer.Entities;

namespace LightRssReader.BusinessLayer.Services;

public interface IFeedService
{
    Task<List<Feed>> List();
    Task<Feed?> Get(long id);
    Task<Feed> Update(Feed feed);
    Task<Feed> Add(Feed feed);
    Task Delete(Feed feed);
    Task Restore(Feed feed);
}