namespace LightRssReader.BusinessLayer.Services;

public class RssItemModel
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public DateTimeOffset PublishDate { get; set; }
}
