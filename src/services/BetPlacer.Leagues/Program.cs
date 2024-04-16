using BetPlacer.Core.Config;
using BetPlacer.Core.API.Service;
using BetPlacer.Leagues.Database;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Leagues.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<LeaguesDbContext>(options => options.UseNpgsql(connection));

var dbContextBuilder = new DbContextOptionsBuilder<LeaguesDbContext>();
dbContextBuilder.UseNpgsql(connection);

#endregion

#region RepositoriesConfig

builder.Services.AddSingleton(new LeaguesRepository(dbContextBuilder.Options));

#endregion

// Add services to the container.

builder.Services.AddScoped<IFootballApiService, FootballApiService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();
