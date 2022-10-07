using System.ServiceModel.Syndication;
using System.Xml;

namespace LightRssReader.BusinessLayer.Services;

public class RssClient
{
  public IEnumerable<RssPostModel> GetRssPosts(string url)
  {
    using var reader = XmlReader.Create(url);
    var feed = SyndicationFeed.Load(reader);

    var posts = feed
        .Items
        .Select(x => new RssPostModel
        {
          Id = x.Id,
          PublishDate = x.PublishDate,
          Summary = x.Summary.Text,
          Title = x.Title.Text,
        });

    return posts;
  }
}