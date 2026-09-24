using Felanmalan.Server.Data;
using Microsoft.EntityFrameworkCore;

string connString = "Data Source=localhost;Initial Catalog=Felanmalan;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Command Timeout=0";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FelanmalanDbContext>(options =>
                options.UseSqlServer(connString));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
