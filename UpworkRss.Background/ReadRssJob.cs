using AutoMapper;
using Microsoft.Extensions.Logging;
using UpwrokRss.BusinessLayer.Entities;
using UpwrokRss.BusinessLayer.Services;

namespace UpworkRss.Background;

public class ReadRssJob
{
    private readonly IMapper _mapper;
    private readonly IRssItemService _rssItemService;
    private readonly IFeedService _feedService;
    private readonly UpworkRssClient _upworkRssClient;
    private readonly ILogger<ReadRssJob> _logger;

    public ReadRssJob(
        IMapper mapper,
        IRssItemService rssItemService,
        IFeedService feedService,
        UpworkRssClient upworkRssClient,
        ILogger<ReadRssJob> logger
    )
    {
        _mapper = mapper;
        _rssItemService = rssItemService;
        _feedService = feedService;
        _upworkRssClient = upworkRssClient;
        _logger = logger;
    }

    public async Task Execute()
    {
        _logger.LogInformation("Started ReadRssJob");
        var feeds = await _feedService.List();
        foreach (var feed in feeds)
        {
            _logger.LogInformation($"Read feed: {feed.Name}");
            var newItems = _upworkRssClient.GetItems(feed.Url);
            var mappedItems = newItems.Select(_mapper.Map<RssItem>);

            var newItemsCount = await _rssItemService.SaveNewItems(feed.Id, mappedItems);
            _logger.LogInformation($"New posts: {newItemsCount}");
            await _feedService.Update(feed);
        }
        _logger.LogInformation("Finished ReadRssJob");
    }
}