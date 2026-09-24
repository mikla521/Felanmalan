using Felanmalan.Server.Data;
using Felanmalan.Server.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Felanmalan")
    ?? throw new InvalidOperationException(
        "Connection string 'Felanmalan' was not found.");

// Add services to the container.
builder.Services.AddDbContext<FelanmalanDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FelanmalanDbContext>();

    if (!db.User.Any(u => u.Email == "demo@felanmalan.se"))
    {
        db.User.Add(new User
        {
            Email = "demo@felanmalan.se",
            Password = "demo123"
        });
    }

    if (!db.User.Any(u => u.Email == "support@felanmalan.se"))
    {
        db.User.Add(new User
        {
            Email = "support@felanmalan.se",
            Password = "support123"
        });
    }

    db.SaveChanges();
}

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
