namespace UpworkRss.Web.Services;

public class UpworkRssItemModel
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public DateTimeOffset PublishDate { get; set; }
}
