
using Microsoft.EntityFrameworkCore;
using LightRssReader.BusinessLayer.Data;
using LightRssReader.BusinessLayer.Entities;
using LightRssReader.BusinessLayer.Models;

namespace LightRssReader.BusinessLayer.Services;

public class RssPostService : IRssPostService
{
    private readonly AppDbContext _context;

    public RssPostService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveNewItems(long feedId, IEnumerable<RssPost> items)
    {
        var urls = items.Select(x => x.Url);
        var uploadedUrls = await _context.RssPosts.AsNoTracking()
            .Where(x => urls.Contains(x.Url))
            .Select(x => x.Url)
            .ToListAsync();
        var notUploaded = items.Where(x => !uploadedUrls.Contains(x.Url)).ToList();
        if (notUploaded.Count() > 0)
        {
            foreach (var item in notUploaded)
            {
                item.FeedId = feedId;
            }
            _context.RssPosts.AddRange(notUploaded);
            await _context.SaveChangesAsync();
        }
        return notUploaded.Count;
    }

    public async Task<List<RssPost>> List(RssPostFilters filters)
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

    public async Task<int> Count(RssPostFilters filters)
    {
        var items = await QueryFeedItems(filters)
            .CountAsync();
        return items;
    }

    public async Task<RssPost?> Get(long id)
    {
        var item = await _context.RssPosts.FindAsync(id);
        return item;
    }

    public async Task Hide(RssPost item)
    {
        item.Hidden = !item.Hidden;
        await _context.SaveChangesAsync();
    }

    public async Task Read(RssPost item)
    {
        item.Read = !item.Read;
        await _context.SaveChangesAsync();
    }

    private IQueryable<RssPost> QueryFeedItems(RssPostFilters filters)
    {
        var query = _context.RssPosts
            .Where(x => !x.Hidden)
            .Where(x => !x.Feed.IsDeleted);

        if (filters.FeedId > 0)
        {
            query = query.Where(x => x.FeedId == filters.FeedId);
        }

        if (!filters.ShowRead)
        {
            query = query.Where(x => !x.Read);
        }

        return query;
    }
}