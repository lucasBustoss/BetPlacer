using Microsoft.EntityFrameworkCore;
using BetPlacer.Scheduler.Worker.Config;
using BetPlacer.Scheduler.Worker.Jobs;
using BetPlacer.Scheduler.Worker.Repositories;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);

#region DbContextConfig

var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<SchedulerDbContext>(options =>
    options.UseNpgsql(connection),
    ServiceLifetime.Scoped);

#endregion

#region RepositoriesConfig

builder.Services.AddScoped<ISchedulerRepository, SchedulerRepository>();

#endregion

#region SchedulerConfig

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("VerificationJob");
    q.AddJob<VerificationJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("VerificationJob-trigger")
        .WithCronSchedule("0 0/30 * * * ?"));
});

#endregion

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var host = builder.Build();
host.Run();
