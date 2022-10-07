using LightRssReader.BusinessLayer.Models;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace LightRssReader.BusinessLayer.Services;

public class TelegramClient : ITelegramClient
{
    private readonly TelegramBotClient _client;
    private readonly TelegramOptions _telegramOptions;

    public TelegramClient(IOptions<TelegramOptions> telegramOptions)
    {
        _telegramOptions = telegramOptions.Value;
        _client = new TelegramBotClient(_telegramOptions.Token);
    }

    public async Task SendMessage(string text)
    {
        try {
            await _client.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(_telegramOptions.ChatId), text);
        } catch (Exception e) {
            
        }
    }
}