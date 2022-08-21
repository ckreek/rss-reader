
using Microsoft.EntityFrameworkCore;
using upwork_rss.Data;
using upwork_rss.Entities;

namespace upwork_rss.Services;

public class RssItemService : IRssItemService
{
  private readonly AppDbContext _context;

  public RssItemService(AppDbContext context)
  {
    _context = context;
  }

  public async Task SaveNewItems(IEnumerable<RssItem> items)
  {
    var urls = items.Select(x => x.Url);
    var uploadedUrls = await _context.RssItems.AsNoTracking()
        .Where(x => urls.Contains(x.Url))
        .Select(x => x.Url)
        .ToListAsync();
    var notUploaded = items.Where(x => !uploadedUrls.Contains(x.Url));
    if (notUploaded.Count() > 0)
    {
      _context.RssItems.AddRange(notUploaded);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<List<RssItem>> List()
  {
    var items = await _context.RssItems.AsNoTracking()
        .Where(x => !x.Hidden)
        .OrderByDescending(x => x.PublishDate)
        .ToListAsync();
    return items;
  }
}