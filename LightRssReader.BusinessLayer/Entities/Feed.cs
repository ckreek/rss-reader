
namespace LightRssReader.BusinessLayer.Entities;

public class Feed : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public bool IsDeleted { get; set; }
}
