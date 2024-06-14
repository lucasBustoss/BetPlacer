using BetPlacer.Core.Config;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Punter.API.Config;
using BetPlacer.Punter.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<PunterDbContext>(options => options.UseNpgsql(connection));

var dbContextBuilder = new DbContextOptionsBuilder<PunterDbContext>();
dbContextBuilder.UseNpgsql(connection);

#endregion

#region RepositoriesConfig

builder.Services.AddSingleton(new PunterRepository(dbContextBuilder.Options));

#endregion

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();
