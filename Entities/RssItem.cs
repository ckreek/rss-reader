
namespace upwork_rss.Entities;

public class RssItem : BaseEntity
{
    public long Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Url { get; set; } = default!;
    public DateTime PublishDate { get; set; }
    public bool Hidden { get; set; }
}
