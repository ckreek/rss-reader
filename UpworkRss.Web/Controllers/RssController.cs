using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UpworkRss.Web.Dto;
using UpwrokRss.BusinessLayer.Entities;
using UpwrokRss.BusinessLayer.Models;
using UpwrokRss.BusinessLayer.Services;

namespace UpworkRss.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RssController : ControllerBase
{
    private readonly ILogger<RssController> _logger;
    private readonly IMapper _mapper;
    private readonly IRssItemService _rssItemService;
    private readonly IFeedService _feedService;

    public RssController(
      ILogger<RssController> logger,
      IMapper mapper,
      IRssItemService rssItemService,
      IFeedService feedService
      )
    {
        _logger = logger;
        _mapper = mapper;
        _rssItemService = rssItemService;
        _feedService = feedService;
    }

    [HttpGet("{feedId}")]
    public async Task<IActionResult> Get(long feedId, int page)
    {
        var feed = await _feedService.Get(feedId);
        if (feed == null)
        {
            return NotFound();
        }

        var items = await _rssItemService.List(feed.Id, new Pagination(page));
        var total = await _rssItemService.Count(feed.Id);
        return Ok(new ListResult<RssItemDto>
        {
            Total = total,
            List = items.Select(_mapper.Map<RssItemDto>),
        });
    }

    [HttpPatch("{id}/hide")]
    public async Task<IActionResult> Hide(long id)
    {
        var item = await _rssItemService.Get(id);
        if (item == null)
        {
            return NotFound();
        }

        await _rssItemService.Hide(item);

        return Ok();
    }

    [HttpPatch("{id}/read")]
    public async Task<IActionResult> Read(long id)
    {
        var item = await _rssItemService.Get(id);
        if (item == null)
        {
            return NotFound();
        }

        await _rssItemService.Read(item);

        return Ok();
    }
}
