using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UpworkRss.Web.Dto;
using UpwrokRss.BusinessLayer.Entities;
using UpwrokRss.BusinessLayer.Services;

namespace UpworkRss.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedController : ControllerBase
{
    private readonly ILogger<FeedController> _logger;
    private readonly IMapper _mapper;
    private readonly IFeedService _feedService;

    public FeedController(
      ILogger<FeedController> logger,
      IMapper mapper,
      IFeedService feedService
      )
    {
        _logger = logger;
        _mapper = mapper;
        _feedService = feedService;
    }

    [HttpGet]
    public async Task<IEnumerable<FeedDto>> Get()
    {
        var feeds = await _feedService.List();
        return feeds.Select(_mapper.Map<FeedDto>);
    }

    [HttpPost]
    public async Task<FeedDto> Post(FeedCreateDto dto)
    {
        var feed = _mapper.Map<Feed>(dto);
        feed = await _feedService.Add(feed);
        return _mapper.Map<FeedDto>(feed);
    }

    [HttpDelete("{feedId}")]
    public async Task<IActionResult> Delete(long feedId)
    {
        var feed = await _feedService.Get(feedId);
        if (feed == null)
        {
            return NotFound();
        }

        await _feedService.Delete(feed);

        return Ok();
    }
}
