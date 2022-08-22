namespace UpworkRss.Web.Dto;

public class RssItemDto
{
  public long Id { get; set; }
  public string Title { get; set; } = default!;
  public string Summary { get; set; } = default!;
  public string Url { get; set; } = default!;
  public DateTime PublishDate { get; set; }
  public bool Read { get; set; }
  public long FeedId { get; set; }
}
