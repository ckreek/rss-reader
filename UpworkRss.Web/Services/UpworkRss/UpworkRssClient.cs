

using System.ServiceModel.Syndication;
using System.Xml;

namespace UpworkRss.Web.Services;

public class UpworkRssClient
{
  public IEnumerable<UpworkRssItemModel> GetItems(string url)
  {
    using var reader = XmlReader.Create(url);
    var feed = SyndicationFeed.Load(reader);

    var items = feed
        .Items
        .Select(x => new UpworkRssItemModel
        {
          Id = x.Id,
          PublishDate = x.PublishDate,
          Summary = x.Summary.Text,
          Title = x.Title.Text,
        });

    return items;
  }
}