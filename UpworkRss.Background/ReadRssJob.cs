using AutoMapper;
using UpwrokRss.BusinessLayer.Entities;
using UpwrokRss.BusinessLayer.Services;

namespace UpworkRss.Background;

public class ReadRssJob
{
    private readonly IMapper _mapper;
    private readonly IRssItemService _rssItemService;
    private readonly IFeedService _feedService;
    private readonly UpworkRssClient _upworkRssClient;

    public ReadRssJob(
        IMapper mapper,
        IRssItemService rssItemService,
        IFeedService feedService,
        UpworkRssClient upworkRssClient
    )
    {
        _mapper = mapper;
        _rssItemService = rssItemService;
        _feedService = feedService;
        _upworkRssClient = upworkRssClient;
    }

    public async Task Execute()
    {
        var feeds = await _feedService.List();
        foreach (var feed in feeds)
        {
            var newItems = _upworkRssClient.GetItems(feed.Url);
            var mappedItems = newItems.Select(_mapper.Map<RssItem>);

            await _rssItemService.SaveNewItems(feed.Id, mappedItems);
            await _feedService.Update(feed);
        }
    }
}