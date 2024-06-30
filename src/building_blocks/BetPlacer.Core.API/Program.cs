using BetPlacer.Core.API.Service.FootballApi;
using BetPlacer.Core.API.Service.PinnacleOdds;
using BetPlacer.Core.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiConfiguration();

// Add services to the container.

builder.Services.AddScoped<IFootballApiService, FootballApiService>();
builder.Services.AddScoped<IPinnacleOddsService, PinnacleOddsService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();

