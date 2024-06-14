using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestAdditionalInformationModel
    {
        public BacktestAdditionalInformationModel()
        {
            
        }

        public BacktestAdditionalInformationModel(int backtestCode, string additionalInformation)
        {
            BacktestCode = backtestCode;
            Info = additionalInformation;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int BacktestCode { get; set; }
        public string Info { get; set; }
    }
}
