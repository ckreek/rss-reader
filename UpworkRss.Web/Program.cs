using Microsoft.EntityFrameworkCore;
using UpworkRss.BusinessLayer.Configurations;
using UpworkRss.Web.Configurations;

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

builder.Services.ConfigureDb(builder.Configuration.GetConnectionString("DefaultConnection"));
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
