
using Microsoft.EntityFrameworkCore;
using UpworkRss.Web.Data;
using UpworkRss.Web.Entities;

namespace UpworkRss.Web.Services;

public class FeedService : IFeedService
{
    private readonly AppDbContext _context;

    public FeedService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Feed?> Get(long id) {
      var feed = await _context.Feeds.FindAsync(id);
      return feed;
    }

    public Task<List<Feed>> List() {
      return _context.Feeds.ToListAsync();
    }
}
