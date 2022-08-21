using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ServiceModel.Syndication;
using System.Xml;
using upwork_rss.Data;
using upwork_rss.Dto;
using upwork_rss.Entities;

namespace upwork_rss.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RssController : ControllerBase
{
  private readonly ILogger<RssController> _logger;
  private readonly IMapper _mapper;
  private readonly AppDbContext _context;

  public RssController(ILogger<RssController> logger, IMapper mapper, AppDbContext context)
  {
    _logger = logger;
    _mapper = mapper;
    _context = context;
  }

  [HttpGet]
  public async Task<IEnumerable<RssItemDto>> Get()
  {
    var newItems = GetItems();
    await SaveNewItems(newItems);
    var items = await _context.RssItems.AsNoTracking().Where(x => !x.Hidden).ToListAsync();
    return items.Select(_mapper.Map<RssItemDto>);
  }

  [HttpPatch("{id}/hide")]
  public async Task<IActionResult> Hide(string id)
  {
    var item = await _context.RssItems.FindAsync(id);
    if (item == null)
    {
      return NotFound();
    }

    item.Hidden = true;
    await _context.SaveChangesAsync();

    return Ok();
  }

  private IEnumerable<RssItem> GetItems()
  {
    var url = "https://www.upwork.com/ab/feed/jobs/rss?sort=recency&subcategory2_uid=531770282589057025%2C531770282589057026%2C531770282589057024%2C531770282589057032%2C531770282584862733&job_type=hourly&duration_v3=month%2Csemester%2Congoing&hourly_rate=50-&paging=0%3B10&api_params=1&q=&securityToken=d0f7ff1791a629b47c022570eb3112e06ba7f7fbc2baa72b1777ca354183abc796443a3fff62018d934cb488bf45cfd99234ae0ffa071cf5408cc6a9b204a5ba&userUid=1044182107692912640&orgUid=1044182107697106945";
    using var reader = XmlReader.Create(url);
    var feed = SyndicationFeed.Load(reader);

    var items = feed
        .Items
        .Select(x => new RssItem
        {
          Id = x.Id,
          PublishDate = x.PublishDate,
          Summary = x.Summary.Text,
          Title = x.Title.Text,
        });

    return items;
  }

  private async Task SaveNewItems(IEnumerable<RssItem> items)
  {
    var ids = items.Select(x => x.Id);
    var uploadedIds = await _context.RssItems.AsNoTracking().Where(x => ids.Contains(x.Id)).Select(x => x.Id).ToListAsync();
    var notUploaded = items.Where(x => !uploadedIds.Contains(x.Id));
    if (notUploaded.Count() > 0)
    {
      _context.RssItems.AddRange(notUploaded);
      await _context.SaveChangesAsync();
    }
  }
}
