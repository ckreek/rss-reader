

using System.ServiceModel.Syndication;
using System.Xml;

namespace LightRssReader.BusinessLayer.Services;

public class RssClient
{
  public IEnumerable<RssItemModel> GetItems(string url)
  {
    using var reader = XmlReader.Create(url);
    var feed = SyndicationFeed.Load(reader);

    var items = feed
        .Items
        .Select(x => new RssItemModel
        {
          Id = x.Id,
          PublishDate = x.PublishDate,
          Summary = x.Summary.Text,
          Title = x.Title.Text,
        });

    return items;
  }
}