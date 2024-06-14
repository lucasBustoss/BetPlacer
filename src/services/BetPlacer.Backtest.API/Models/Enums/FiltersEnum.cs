namespace BetPlacer.Backtest.API.Models.Enums
{
    public enum FilterCompareType
    {
        Greater = 1,
        EqualOrGreater = 2,

    }

    public enum FilterTeamType
    {
        HomeTeam = 1,
        AwayTeam = 2
    }

    public enum FilterPropType
    {
        Overall = 1,
        HomeAway = 2
    }

    public enum FilterCalculateType 
    { 
        Absolute = 1,
        Relative = 2
    }

    public enum FilterCalculateOperation 
    { 
        Sum = 1,
        Multiplication = 2,
        Subtraction = 3,
        Division = 4,
    }
}
