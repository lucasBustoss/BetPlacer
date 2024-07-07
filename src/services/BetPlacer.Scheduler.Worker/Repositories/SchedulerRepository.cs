using BetPlacer.Scheduler.Worker.Config;
using BetPlacer.Scheduler.Worker.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Scheduler.Worker.Repositories
{
    public class SchedulerRepository : ISchedulerRepository
    {
        private readonly SchedulerDbContext _context;

        public SchedulerRepository(DbContextOptions<SchedulerDbContext> db)
        {
            _context = new SchedulerDbContext(db);
        }

        public SchedulerModel GetSchedulerExecution()
        {
            string formattedDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
            SchedulerModel execution = _context.SchedulerExecution.Where(s => s.Date == formattedDate).FirstOrDefault();

            if (execution == null)
                execution = CreateSchedulerExecution();

            return execution;

        }

        public void UpdateExecution(SchedulerModel execution)
        {
            var existingExecution = _context.SchedulerExecution.Where(s => s.Code == execution.Code).FirstOrDefault();

            var newExecution = execution;
            newExecution.Code = existingExecution.Code;
            _context.Entry(existingExecution).CurrentValues.SetValues(newExecution);
            _context.SaveChanges();
        }

        #region Private methods

        public SchedulerModel CreateSchedulerExecution()
        {
            SchedulerModel execution = new SchedulerModel(DateTime.UtcNow.ToString("dd/MM/yyyy"));
            _context.SchedulerExecution.Add(execution);
            _context.SaveChanges();
            
            return execution;
        }

        #endregion
    }
}
