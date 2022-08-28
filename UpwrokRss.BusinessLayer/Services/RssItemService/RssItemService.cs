
using Microsoft.EntityFrameworkCore;
using UpwrokRss.BusinessLayer.Data;
using UpwrokRss.BusinessLayer.Entities;
using UpwrokRss.BusinessLayer.Models;

namespace UpwrokRss.BusinessLayer.Services;

public class RssItemService : IRssItemService
{
    private readonly AppDbContext _context;

    public RssItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveNewItems(long feedId, IEnumerable<RssItem> items)
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
        return notUploaded.Count;
    }

    public async Task<List<RssItem>> List(RssItemFilters filters)
    {
        var pagination = new Pagination(filters.Page);

        var items = await QueryFeedItems(filters)
            .AsNoTracking()
            .OrderByDescending(x => x.PublishDate)
            .Skip(pagination.Skip)
            .Take(pagination.Limit)
            .ToListAsync();

        return items;
    }

    public async Task<int> Count(RssItemFilters filters)
    {
        var items = await QueryFeedItems(filters)
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
        item.Hidden = !item.Hidden;
        await _context.SaveChangesAsync();
    }

    public async Task Read(RssItem item)
    {
        item.Read = !item.Read;
        await _context.SaveChangesAsync();
    }

    private IQueryable<RssItem> QueryFeedItems(RssItemFilters filters)
    {
        var query = _context.RssItems.Where(x => !x.Hidden);

        if (filters.FeedId > 0) {
            query = query.Where(x => x.FeedId == filters.FeedId);
        }

        if (!filters.ShowRead) {
            query = query.Where(x => !x.Read);
        }

        return query;
    }
}