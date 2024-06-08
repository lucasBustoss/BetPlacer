using BetPlacer.Backtest.API.Config;
using BetPlacer.Backtest.API.Messages;
using BetPlacer.Backtest.API.Messages.Consumer;
using BetPlacer.Backtest.API.Repositories;
using BetPlacer.Backtest.API.Services;
using BetPlacer.Core.Config;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<BacktestDbContext>(options => options.UseNpgsql(connection));

var dbContextBuilder = new DbContextOptionsBuilder<BacktestDbContext>();
dbContextBuilder.UseNpgsql(connection);

#endregion

#region RepositoriesConfig

builder.Services.AddSingleton(new BacktestRepository(dbContextBuilder.Options));

#endregion

builder.Services.AddSingleton<IBacktestOrchestrator, BacktestOrchestrator>();

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();
