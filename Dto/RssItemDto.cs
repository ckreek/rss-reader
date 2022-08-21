namespace upwork_rss.Dto;

public class RssItemDto
{
  public long Id { get; set; } = default!;
  public string Title { get; set; } = default!;
  public string Summary { get; set; } = default!;
  public string Url { get; set; } = default!;
  public DateTime PublishDate { get; set; }
}
