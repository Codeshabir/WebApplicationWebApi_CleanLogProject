using Client.Areas.Identity.Data;
using Client.Data;
using Client.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ClientContextConnection") ?? throw new InvalidOperationException("Connection string 'ClientContextConnection' not found.");

// Build the configuration
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with connection string from configuration
builder.Services.AddDbContext<LogDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ClientUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LogDbContext>();

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

// Add CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder => builder
			.AllowAnyOrigin()    // Allow all origins
			.AllowAnyMethod()    // Allow all HTTP methods
			.AllowAnyHeader());  // Allow all headers
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
builder.Services.AddRazorPages(); 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable CORS policy
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=CleaningLogs}/{action=Index}/{id?}");
    endpoints.MapRazorPages(); // Map Razor Pages endpoints
});
app.Run();
