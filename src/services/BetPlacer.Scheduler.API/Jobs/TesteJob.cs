using Quartz;

namespace BetPlacer.Scheduler.API.Jobs
{
    public class TesteJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("teste");
            return Task.FromResult(true);
        }
    }
}
