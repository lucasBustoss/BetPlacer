using BetPlacer.Core.API.Service;
using BetPlacer.Core.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiConfiguration();

// Add services to the container.

builder.Services.AddScoped<IFootballApiService, FootballApiService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();

