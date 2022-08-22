
using Microsoft.EntityFrameworkCore;
using UpworkRss.Web.Data;
using UpworkRss.Web.Entities;
using UpworkRss.Web.Models;

namespace UpworkRss.Web.Services;

public class RssItemService : IRssItemService
{
    private readonly AppDbContext _context;

    public RssItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveNewItems(long feedId, IEnumerable<RssItem> items)
    {
        var urls = items.Select(x => x.Url);
        var uploadedUrls = await _context.RssItems.AsNoTracking()
            .Where(x => x.FeedId == feedId && urls.Contains(x.Url))
            .Select(x => x.Url)
            .ToListAsync();
        var notUploaded = items.Where(x => !uploadedUrls.Contains(x.Url)).ToList();
        if (notUploaded.Count() > 0)
        {
            foreach (var item in notUploaded)
            {
                item.FeedId = feedId;
            }
            _context.RssItems.AddRange(notUploaded);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<RssItem>> List(long feedId, Pagination pagination)
    {
        var items = await QueryFeedItems(feedId)
            .AsNoTracking()
            .OrderByDescending(x => x.PublishDate)
            .Skip(pagination.Skip)
            .Take(pagination.Limit)
            .ToListAsync();
        return items;
    }

    public async Task<int> Count(long feedId)
    {
        var items = await QueryFeedItems(feedId)
            .CountAsync();
        return items;
    }

    public async Task<RssItem?> Get(long id)
    {
        var item = await _context.RssItems.FindAsync(id);
        return item;
    }

    public async Task Hide(RssItem item)
    {
        item.Hidden = true;
        await _context.SaveChangesAsync();
    }

    public async Task Read(RssItem item)
    {
        item.Read = !item.Read;
        await _context.SaveChangesAsync();
    }

    private IQueryable<RssItem> QueryFeedItems(long feedId)
    {
        return _context.RssItems.Where(x => x.FeedId == feedId && !x.Hidden);
    }
}