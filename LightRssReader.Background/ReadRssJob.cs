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
    private readonly ITelegramClient _telegramClient;

    public ReadRssJob(
        IMapper mapper,
        IRssPostService rssPostService,
        IFeedService feedService,
        RssClient rssClient,
        ILoggerFactory loggerFactory,
        ITelegramClient telegramClient
    )
    {
        _mapper = mapper;
        _rssPostService = rssPostService;
        _feedService = feedService;
        _rssClient = rssClient;
        _logger = loggerFactory.CreateLogger("Read RSS");
        _telegramClient = telegramClient;
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
            await _feedService.Update(feed);

            var logMessage = $"{feed.Name} posts found: {newItemsCount}{System.Environment.NewLine}";
            _logger.LogTrace(logMessage);

            if (newItemsCount > 0) {
                await _telegramClient.SendMessage(logMessage);
            }
        }
        _logger.LogInformation("---------- Finished ----------");
    }
}