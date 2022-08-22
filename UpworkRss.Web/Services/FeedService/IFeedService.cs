
using UpworkRss.Web.Entities;

namespace UpworkRss.Web.Services;

public interface IFeedService
{
    Task<List<Feed>> List();
    Task<Feed?> Get(long id);
}