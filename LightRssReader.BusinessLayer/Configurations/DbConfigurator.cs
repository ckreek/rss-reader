using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LightRssReader.BusinessLayer.Data;

namespace LightRssReader.BusinessLayer.Configurations;

public static class DbConfigurator
{
    public static void ConfigureDb(this IServiceCollection services, string dbName)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = System.IO.Path.Join(path, dbName);
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));
    }
}
