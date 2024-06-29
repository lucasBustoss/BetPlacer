using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Punter.API.Models.Entities
{
    public class PunterBacktestCombinedIntervalModel
    {
        public PunterBacktestCombinedIntervalModel(int punterBacktestCode, string name, double percentMatches, double result, double coefficientVariation, double inferiorLimit)
        {
            PunterBacktestCode = punterBacktestCode;
            Name = name;
            PercentMatches = percentMatches;
            Result = result;
            CoefficientVariation = coefficientVariation;
            InferiorLimit = inferiorLimit;
            Active = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int PunterBacktestCode { get; set; }
        public string Name { get; set; }
        public double PercentMatches { get; set; }
        public double Result { get; set; }
        public double CoefficientVariation { get; set; }
        public double InferiorLimit { get; set; }
        public bool Active { get; set; }
    }
}
