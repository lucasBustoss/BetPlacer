using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Punter.API.Models.Entities
{
    public class PunterBacktestIntervalModel
    {
        public PunterBacktestIntervalModel(int punterBacktestCode, string name, double initialValue, double finalValue, double coefficientVariation, double inferiorLimit)
        {
            PunterBacktestCode = punterBacktestCode;
            Name = name;
            InitialValue = initialValue;
            FinalValue = finalValue;
            CoefficientVariation = coefficientVariation;
            InferiorLimit = inferiorLimit;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int PunterBacktestCode { get; set; }
        public string Name { get; set; }
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
        public double CoefficientVariation { get; set; }
        public double InferiorLimit { get; set; }
    }
}
