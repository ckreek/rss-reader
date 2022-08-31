namespace LightRssReader.BusinessLayer.Models;

public class ListResult<T>
{
    public int Total { get; set; }
    public IEnumerable<T> List { get; set; } = new List<T>();
}
