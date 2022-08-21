using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upwork_rss.Data;
using upwork_rss.Dto;
using upwork_rss.Entities;
using upwork_rss.Services;

namespace upwork_rss.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RssController : ControllerBase
{
  private readonly ILogger<RssController> _logger;
  private readonly IMapper _mapper;
  private readonly AppDbContext _context;
  private readonly UpworkRssClient _upworkRssClient;
  private readonly IRssItemService _rssItemService;

  public RssController(
    ILogger<RssController> logger,
    IMapper mapper,
    AppDbContext context,
    UpworkRssClient upworkRssClient,
    IRssItemService rssItemService
    )
  {
    _logger = logger;
    _mapper = mapper;
    _context = context;
    _upworkRssClient = upworkRssClient;
    _rssItemService = rssItemService;
  }

  [HttpGet]
  public async Task<IEnumerable<RssItemDto>> Get()
  {
    var newItems = _upworkRssClient.GetItems();
    var mappedItems = newItems.Select(_mapper.Map<RssItem>);
    await _rssItemService.SaveNewItems(mappedItems);
    var items = await _rssItemService.List();
    return items.Select(_mapper.Map<RssItemDto>);
  }

  [HttpPatch("{id}/hide")]
  public async Task<IActionResult> Hide(long id)
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
}
