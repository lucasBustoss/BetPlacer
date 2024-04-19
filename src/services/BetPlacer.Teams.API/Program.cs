using BetPlacer.Core.Config;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Teams.Config;
using BetPlacer.Teams.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<TeamsDbContext>(options => options.UseNpgsql(connection));

var dbContextBuilder = new DbContextOptionsBuilder<TeamsDbContext>();
dbContextBuilder.UseNpgsql(connection);

#endregion

#region RepositoriesConfig

builder.Services.AddSingleton(new TeamsRepository(dbContextBuilder.Options));

#endregion

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();
