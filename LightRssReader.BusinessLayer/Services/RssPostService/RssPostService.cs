
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

    public async Task<int> SaveNewRssPosts(long feedId, IEnumerable<RssPost> rssPosts)
    {
        var urls = rssPosts.Select(x => x.Url);
        var uploadedUrls = await _context.RssPosts.AsNoTracking()
            .Where(x => urls.Contains(x.Url))
            .Select(x => x.Url)
            .ToListAsync();
        var notUploaded = rssPosts.Where(x => !uploadedUrls.Contains(x.Url)).ToList();
        if (notUploaded.Count() > 0)
        {
            foreach (var rssPost in notUploaded)
            {
                rssPost.FeedId = feedId;
            }
            _context.RssPosts.AddRange(notUploaded);
            await _context.SaveChangesAsync();
        }
        return notUploaded.Count;
    }

    public async Task<List<RssPost>> List(RssPostFilters filters)
    {
        var pagination = new Pagination(filters.Page);

        var rssPosts = await QueryRssPosts(filters)
            .AsNoTracking()
            .OrderByDescending(x => x.PublishDate)
            .Skip(pagination.Skip)
            .Take(pagination.Limit)
            .ToListAsync();

        return rssPosts;
    }

    public async Task<int> Count(RssPostFilters filters)
    {
        var rssPosts = await QueryRssPosts(filters)
            .CountAsync();
        return rssPosts;
    }

    public async Task<RssPost?> Get(long id)
    {
        var rssPost = await _context.RssPosts.FindAsync(id);
        return rssPost;
    }

    public async Task Hide(RssPost rssPost)
    {
        rssPost.Hidden = !rssPost.Hidden;
        await _context.SaveChangesAsync();
    }

    public async Task Read(RssPost rssPost)
    {
        rssPost.Read = !rssPost.Read;
        await _context.SaveChangesAsync();
    }

    private IQueryable<RssPost> QueryRssPosts(RssPostFilters filters)
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