using Microsoft.EntityFrameworkCore;
using UpworkRss.Web.Configurations;
using UpwrokRss.BusinessLayer.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
  options.AddPolicy(MyAllowSpecificOrigins,
                        policy =>
                        {
                          policy.WithOrigins("http://localhost:3000")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                        });
});


var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
var dbName = builder.Configuration.GetConnectionString("DefaultConnection");
var dbPath = System.IO.Path.Join(path, dbName);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.ConfigureServices();
builder.Services.ConfigureMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
