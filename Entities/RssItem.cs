
namespace upwork_rss.Entities;

public class RssItem : BaseEntity
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public DateTimeOffset PublishDate { get; set; }
    public bool Hidden { get; set; }
}
