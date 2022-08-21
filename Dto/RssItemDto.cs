
namespace upwork_rss.Dto;

public class RssItemDto
{
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string Url => Id;
    public DateTimeOffset PublishDate { get; set; }
}
