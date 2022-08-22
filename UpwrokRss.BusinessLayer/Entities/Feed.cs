
namespace UpwrokRss.BusinessLayer.Entities;

public class Feed : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
}
