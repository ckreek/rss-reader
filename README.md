# Light RSS Reader
RSS client with simple and clean UI

## LightRssReader.Web
ASP.NET Core API + SPA with React.JS and MobX

## LightRssReader.BusinessLayer
Services and DB layers: EF Core and SQLite

## LightRssReader.Background
Console app to fetch new posts every 3 mins and send notifications to Telegram

## Setup
dotnet user-secrets set "TelegramOptions:Token" "token"

dotnet user-secrets set "TelegramOptions:ChatId" "chatId"


