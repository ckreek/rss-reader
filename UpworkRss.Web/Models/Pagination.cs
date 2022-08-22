
namespace UpworkRss.Web.Models;

public class Pagination
{
    public int Page { get; set; }
    public int Limit => 10;
    public int Skip => Page * Limit;

    public Pagination(int page)
    {
        Page = page;
    }
}
