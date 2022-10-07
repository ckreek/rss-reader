using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LightRssReader.Web.Dto;
using LightRssReader.BusinessLayer.Models;
using LightRssReader.BusinessLayer.Services;

namespace LightRssReader.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RssController : ControllerBase
{
    private readonly ILogger<RssController> _logger;
    private readonly IMapper _mapper;
    private readonly IRssPostService _rssPostService;
    private readonly IFeedService _feedService;

    public RssController(
      ILogger<RssController> logger,
      IMapper mapper,
      IRssPostService rssPostService,
      IFeedService feedService
      )
    {
        _logger = logger;
        _mapper = mapper;
        _rssPostService = rssPostService;
        _feedService = feedService;
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] RssPostFilters filters)
    {
        if (filters.FeedId > 0)
        {
            var feed = await _feedService.Get(filters.FeedId);
            if (feed == null)
            {
                return NotFound();
            }
        }

        var rssPosts = await _rssPostService.List(filters);
        var total = await _rssPostService.Count(filters);
        return Ok(new ListResult<RssPostDto>
        {
            Total = total,
            List = rssPosts.Select(_mapper.Map<RssPostDto>),
        });
    }

    [HttpPatch("{id}/hide")]
    public async Task<IActionResult> Hide(long id)
    {
        var rssPost = await _rssPostService.Get(id);
        if (rssPost == null)
        {
            return NotFound();
        }

        await _rssPostService.Hide(rssPost);

        return Ok();
    }

    [HttpPatch("{id}/read")]
    public async Task<IActionResult> Read(long id)
    {
        var rssPost = await _rssPostService.Get(id);
        if (rssPost == null)
        {
            return NotFound();
        }

        await _rssPostService.Read(rssPost);

        return Ok();
    }
}
