
namespace UpworkRss.Web.Entities;

public class RssItem : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Url { get; set; } = default!;
    public DateTime PublishDate { get; set; }
    public bool Hidden { get; set; }
    public bool Read { get; set; }
    public long FeedId { get; set; }
    public Feed Feed { get; set; } = default!;
}
