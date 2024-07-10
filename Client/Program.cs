using Client.Data;
using Client.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Build the configuration
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with connection string from configuration
builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Register the service
builder.Services.AddScoped<CleaningLogsService>();
builder.Services.AddScoped<LandscapingLogsService>();
builder.Services.AddScoped<SnowLogsService>();

// Add a named HttpClient with the base URL for API requests
var baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;

builder.Services.AddHttpClient<CleaningLogsService>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<LandscapingLogsService>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<SnowLogsService>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CleaningLogs}/{action=Index}/{id?}");

app.Run();
