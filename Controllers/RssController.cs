using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;
using upwork_rss.Dto;

namespace upwork_rss.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RssController : ControllerBase
{
    private readonly ILogger<RssController> _logger;

    public RssController(ILogger<RssController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<RssItem> Get()
    {
        var url = "https://www.upwork.com/ab/feed/jobs/rss?sort=recency&subcategory2_uid=531770282589057025%2C531770282589057026%2C531770282589057024%2C531770282589057032%2C531770282584862733&job_type=hourly&duration_v3=month%2Csemester%2Congoing&hourly_rate=50-&paging=0%3B10&api_params=1&q=&securityToken=d0f7ff1791a629b47c022570eb3112e06ba7f7fbc2baa72b1777ca354183abc796443a3fff62018d934cb488bf45cfd99234ae0ffa071cf5408cc6a9b204a5ba&userUid=1044182107692912640&orgUid=1044182107697106945";
        using var reader = XmlReader.Create(url);
        var feed = SyndicationFeed.Load(reader);

        var items = feed
            .Items
            .Select(x => new RssItem
            {
                Id = x.Id,
                PublishDate = x.PublishDate,
                Summary = x.Summary.Text,
                Title = x.Title.Text,
            });

        return items;
    }
}
