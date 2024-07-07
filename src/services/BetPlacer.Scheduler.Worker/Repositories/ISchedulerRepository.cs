using BetPlacer.Scheduler.Worker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetPlacer.Scheduler.Worker.Repositories
{
    public interface ISchedulerRepository
    {
        SchedulerModel GetSchedulerExecution();
        void UpdateExecution(SchedulerModel execution);
    }
}
