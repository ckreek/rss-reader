using Microsoft.EntityFrameworkCore;
using upwork_rss.Entities;

namespace upwork_rss.Data;

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

  public DbSet<RssItem> RssItems { get; set; } = default!;
}