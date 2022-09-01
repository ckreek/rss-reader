using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LightRssReader.BusinessLayer.Data;
using LightRssReader.BusinessLayer.Helpers;
using Microsoft.Extensions.Configuration;

namespace LightRssReader.BusinessLayer.Configurations;

public static class AppSettingsConfigurator
{
    public static void ConfigureAppSettings(this IConfigurationBuilder config, string environmentName)
    {
        var appSettingsPath = FileHelper.FindPath("appsettings/appsettings.json");
        var appSettingsEnvPath = FileHelper.FindPath($"appsettings/appsettings.{environmentName}.json");

        if (!string.IsNullOrWhiteSpace(appSettingsPath))
            config.AddJsonFile(appSettingsPath, false, false);

        if (!string.IsNullOrWhiteSpace(appSettingsEnvPath))
            config.AddJsonFile(appSettingsEnvPath, false, false);
    }
}
