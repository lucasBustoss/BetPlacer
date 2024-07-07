using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Scheduler.Worker.Models.Entities
{
    public class SchedulerModel
    {
        public SchedulerModel(string date)
        {
            Date = date;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Date { get; set; } 
        public bool OddsFilled { get; set; }
        public bool AnalysisDone { get; set; }
    }
}
