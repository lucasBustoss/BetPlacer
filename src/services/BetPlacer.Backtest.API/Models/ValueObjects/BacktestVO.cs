using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Backtest.API.Models.Filters;

namespace BetPlacer.Backtest.API.Models.ValueObjects
{
    public class BacktestVO
    {
        public BacktestVO()
        {
            Filters = new List<BacktestFilter>();
            AdditionalInformation = new List<BacktestAdditionalInformation>();
        }

        public BacktestVO(BacktestModel model, List<BacktestFilterModel> filters, List<BacktestAdditionalInformationModel> additionalInformations)
        {
            Code = model.Code;
            Name = model.Name;
            UserId = model.UserId;
            CreationDate = model.CreationDate;
            Type = model.Type;
            TeamType = model.TeamType;
            FilteredFixtures = model.FilteredFixtures;
            MatchedFixtures = model.MatchedFixtures;
            MaxGoodRun = model.MaxGoodRun;
            MaxBadRun = model.MaxBadRun;
            UsesInFixture = model.UsesInFixture;

            if (filters != null && filters.Count > 0)
            {
                Filters = new List<BacktestFilter>();

                foreach (var filter in filters)
                {
                    BacktestFilter newFilter = new BacktestFilter();
                    newFilter.FilterCode = filter.FilterCode;
                    newFilter.FilterName = filter.FilterName;
                    newFilter.CompareType = filter.CompareType;
                    newFilter.TeamType = filter.TeamType;
                    newFilter.PropType = filter.PropType;
                    newFilter.InitialValue = filter.InitialValue;
                    newFilter.FinalValue = filter.FinalValue;

                    Filters.Add(newFilter);
                }
            }

            if (additionalInformations != null && additionalInformations.Count > 0)
            {
                AdditionalInformation = new List<BacktestAdditionalInformation>();

                foreach (var info in additionalInformations)
                {
                    BacktestAdditionalInformation newInfo = new BacktestAdditionalInformation(info.Info);
                    AdditionalInformation.Add(newInfo);
                }
            }
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public int TeamType { get; set; }
        public double FilteredFixtures { get; set; }
        public double MatchedFixtures { get; set; }
        public int MaxGoodRun { get; set; }
        public int MaxBadRun { get; set; }
        public bool UsesInFixture { get; set; }
        public List<BacktestFilter> Filters { get; set; }
        public List<BacktestAdditionalInformation> AdditionalInformation { get; set; }
    }
}
