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
            var newRssPosts = _rssClient.GetRssPosts(feed.Url);
            var mappedRssPosts = newRssPosts.Select(_mapper.Map<RssPost>);

            var newRssPostsCount = await _rssPostService.SaveNewRssPosts(feed.Id, mappedRssPosts);
            await _feedService.Update(feed);

            var logMessage = $"{feed.Name} posts found: {newRssPostsCount}{System.Environment.NewLine}";
            _logger.LogTrace(logMessage);

            if (newRssPostsCount > 0) {
                await _telegramClient.SendMessage(logMessage);
            }
        }
        _logger.LogInformation("---------- Finished ----------");
    }
}