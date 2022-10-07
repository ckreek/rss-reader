namespace LightRssReader.BusinessLayer.Services;

public interface ITelegramClient
{
    Task SendMessage(string text);
}