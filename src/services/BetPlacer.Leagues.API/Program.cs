using BetPlacer.Core.Config;
using BetPlacer.Leagues.Config;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Leagues.API.Repositories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<LeaguesDbContext>(options =>
    options.UseNpgsql(connection),
    ServiceLifetime.Scoped);

#endregion

#region RepositoriesConfig

builder.Services.AddScoped<ILeaguesRepository, LeaguesRepository>();

#endregion

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();
