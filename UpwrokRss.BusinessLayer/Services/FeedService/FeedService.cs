
using Microsoft.EntityFrameworkCore;
using UpwrokRss.BusinessLayer.Data;
using UpwrokRss.BusinessLayer.Entities;

namespace UpwrokRss.BusinessLayer.Services;

public class FeedService : IFeedService
{
    private readonly AppDbContext _context;

    public FeedService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Feed?> Get(long id)
    {
        var feed = await _context.Feeds.FindAsync(id);
        return feed;
    }

    public Task<List<Feed>> List()
    {
        return _context.Feeds.AsNoTracking().OrderByDescending(x => x.CreatedOn).ToListAsync();
    }

    public async Task<Feed> Update(Feed feed)
    {
        _context.Feeds.Update(feed);
        await _context.SaveChangesAsync();
        return feed;
    }

    public async Task<Feed> Add(Feed feed)
    {
        _context.Feeds.Add(feed);
        await _context.SaveChangesAsync();
        return feed;
    }

    public async Task Delete(Feed feed)
    {
        _context.Feeds.Remove(feed);
        await _context.SaveChangesAsync();
    }
}
