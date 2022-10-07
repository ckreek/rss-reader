namespace LightRssReader.BusinessLayer.Models;

public class TelegramOptions
{
    public string Token { get; set; } = default!;
    public long ChatId { get; set; } = default!;
}
