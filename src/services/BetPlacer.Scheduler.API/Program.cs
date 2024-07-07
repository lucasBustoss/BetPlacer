using BetPlacer.Scheduler.API.Jobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("TesteJob");
    q.AddJob<TesteJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("TesteJob-trigger")
        .WithCronSchedule("0/5 * * * * ?"));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

app.Run();
