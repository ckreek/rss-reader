using System.Globalization;
using Microsoft.EntityFrameworkCore;
using UpwrokRss.BusinessLayer.Entities;

namespace UpwrokRss.BusinessLayer.Data;

public class AppDbContext : DbContext
{
    // public string DbPath { get; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // public AppDbContext()
    // {
    //   var folder = Environment.SpecialFolder.LocalApplicationData;
    //   var path = Environment.GetFolderPath(folder);
    //   DbPath = System.IO.Path.Join(path, "upwork-rss.db");
    // }

    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdatedOn = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RssItem>()
            .HasIndex(x => x.Url);

        builder.Entity<RssItem>()
            .Property(x => x.PublishDate)
            .HasConversion(
                x => x.ToString("yyyy-MM-ddThh:mm:ss%K")
                // , x => DateTime.Parse(x, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
                , x => DateTime.ParseExact(x, new [] {"yyyy-MM-ddThh:mm:ss%K", "yyyy-MM-dd hh:mm:ss"}, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
            );
    }

    public DbSet<RssItem> RssItems { get; set; } = default!;

    public DbSet<Feed> Feeds { get; set; } = default!;
}