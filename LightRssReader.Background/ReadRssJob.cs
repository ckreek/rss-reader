using AutoMapper;
using Microsoft.Extensions.Logging;
using LightRssReader.BusinessLayer.Entities;
using LightRssReader.BusinessLayer.Services;

namespace LightRssReader.Background;

public class ReadRssJob
{
    private readonly IMapper _mapper;
    private readonly IRssPostService _rssPostService;
    private readonly IFeedService _feedService;
    private readonly RssClient _rssClient;
    private readonly ILogger _logger;

    public ReadRssJob(
        IMapper mapper,
        IRssPostService rssPostService,
        IFeedService feedService,
        RssClient rssClient,
        ILoggerFactory loggerFactory
    )
    {
        _mapper = mapper;
        _rssPostService = rssPostService;
        _feedService = feedService;
        _rssClient = rssClient;
        _logger = loggerFactory.CreateLogger("Read RSS");
    }

    public async Task Execute()
    {
        _logger.LogInformation("---------- Started -----------");
        var feeds = await _feedService.List();
        foreach (var feed in feeds)
        {
            var newItems = _rssClient.GetItems(feed.Url);
            var mappedItems = newItems.Select(_mapper.Map<RssPost>);

            var newItemsCount = await _rssPostService.SaveNewItems(feed.Id, mappedItems);
            _logger.LogTrace($"{feed.Name} posts found: {newItemsCount}{System.Environment.NewLine}");
            await _feedService.Update(feed);
        }
        _logger.LogInformation("---------- Finished ----------");
    }
}