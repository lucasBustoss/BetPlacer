using BetPlacer.Core.Config;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Teams.Config;
using BetPlacer.Teams.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<TeamsDbContext>(options =>
    options.UseNpgsql(connection),
    ServiceLifetime.Scoped);

#endregion

#region RepositoriesConfig

builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

#endregion

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();
