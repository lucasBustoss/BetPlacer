using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Punter.API.Models.Entities
{
    public class PunterBacktestClassificationModel
    {
        public PunterBacktestClassificationModel(int punterBacktestCode, string classification)
        {
            PunterBacktestCode = punterBacktestCode;
            Classification = classification;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Code { get; set; }
        public int PunterBacktestCode { get; set; }
        public string Classification { get; set; }
    }
}
