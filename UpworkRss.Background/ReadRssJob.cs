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
    private readonly ILogger _logger;

    public ReadRssJob(
        IMapper mapper,
        IRssItemService rssItemService,
        IFeedService feedService,
        UpworkRssClient upworkRssClient,
        ILoggerFactory loggerFactory
    )
    {
        _mapper = mapper;
        _rssItemService = rssItemService;
        _feedService = feedService;
        _upworkRssClient = upworkRssClient;
        _logger = loggerFactory.CreateLogger("Read RSS");
    }

    public async Task Execute()
    {
        _logger.LogInformation("---------- Started -----------");
        var feeds = await _feedService.List();
        foreach (var feed in feeds)
        {
            var newItems = _upworkRssClient.GetItems(feed.Url);
            var mappedItems = newItems.Select(_mapper.Map<RssItem>);

            var newItemsCount = await _rssItemService.SaveNewItems(feed.Id, mappedItems);
            _logger.LogTrace($"{feed.Name} posts found: {newItemsCount}{System.Environment.NewLine}");
            await _feedService.Update(feed);
        }
        _logger.LogInformation("---------- Finished ----------");
    }
}