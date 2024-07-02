using BetPlacer.Core.Config;
using BetPlacer.Fixtures.API.Messages;
using BetPlacer.Fixtures.API.Repositories;
using BetPlacer.Fixtures.Config;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<FixturesDbContext>(options =>
    options.UseNpgsql(connection),
    ServiceLifetime.Scoped);

#endregion

#region RepositoriesConfig

builder.Services.AddScoped<IFixturesRepository, FixturesRepository>();

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
