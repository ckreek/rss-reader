
using upwork_rss.Entities;

namespace upwork_rss.Services;

public interface IFeedService
{
    Task<List<Feed>> List();
    Task<Feed?> Get(long id);
}