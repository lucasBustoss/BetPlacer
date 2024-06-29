using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetPlacer.Punter.API.Models.Entities
{
    public class PunterBacktestModel
    {
        public PunterBacktestModel(string name, int leagueCode, double resultAfterClassification)
        {
            Name = name;
            LeagueCode = leagueCode;
            ResultAfterClassification = resultAfterClassification;
            Active = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public int LeagueCode { get; set; }
        public double ResultAfterClassification { get; set; }
        public bool Active { get; set; }
    }
}
