namespace BetPlacer.Punter.API.Models.ValueObjects.Match
{
    public class MatchAnalyzed
    {
        public MatchAnalyzed(MatchBaseData match, string matchOddsClassification, string goalsClassification, string bttsClassification)
        {
            MatchCode = match.MatchCode;
            Date = match.Date;
            Season = match.Season;
            HomeOdd = match.HomeOdd;
            DrawOdd = match.DrawOdd;
            AwayOdd = match.AwayOdd;
            Over25Odd = match.Over25Odd;
            Under25Odd = match.Under25Odd;
            BttsYesOdd = match.BttsYesOdd;
            BttsNoOdd = match.BttsNoOdd;
            HomeGoals = match.HomeGoals;
            AwayGoals = match.AwayGoals;
            MatchOddsClassification = matchOddsClassification;
            GoalsClassification = goalsClassification;
            BttsClassification = bttsClassification;
        }
        public MatchAnalyzed(NextMatch match, string matchOddsClassification, string goalsClassification, string bttsClassification)
        {
            MatchCode = match.MatchCode;
            Date = match.Date;
            Season = match.Season;
            HomeOdd = match.HomeOdd;
            DrawOdd = match.DrawOdd;
            AwayOdd = match.AwayOdd;
            Over25Odd = match.Over25Odd;
            Under25Odd = match.Under25Odd;
            BttsYesOdd = match.BttsYesOdd;
            BttsNoOdd = match.BttsNoOdd;
            HomeGoals = 0;
            AwayGoals = 0;
            MatchOddsClassification = matchOddsClassification;
            GoalsClassification = goalsClassification;
            BttsClassification = bttsClassification;
        }

        public int MatchCode { get; set; }
        public string Date { get; set; }
        public string Season { get; set; }
        public double HomeOdd { get; set; }
        public double DrawOdd { get; set; }
        public double AwayOdd { get; set; }
        public double Over25Odd { get; set; }
        public double Under25Odd { get; set; }
        public double BttsYesOdd { get; set; }
        public double BttsNoOdd { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public string MatchOddsClassification { get; set; }
        public string GoalsClassification { get; set; }
        public string BttsClassification { get; set; }

        public double HomeHandicap1Odd
        {
            get
            {
                double odd = 0.16 * HomeOdd + 0.742;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd <= 1.65)
                    {
                        odd = 1.03;
                    }

                    if (HomeOdd >= 1.65 && HomeOdd <= 1.83)
                    {
                        odd = 1.05;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeHandicap075Odd
        {
            get
            {
                double odd = 0.236 * HomeOdd + 0.677;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd <= 1.4)
                    {
                        odd = 1.05;
                    }

                    if (HomeOdd > 1.40 && HomeOdd <= 1.50)
                    {
                        odd = 1.07;
                    }

                    if (HomeOdd > 1.5 && HomeOdd <= 1.62)
                    {
                        odd = 1.1;
                    }

                    if (HomeOdd > 1.62 && HomeOdd <= 1.8)
                    {
                        odd = 1.12;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeHandicap05Odd
        {
            get
            {
                double odd = 0.302 * HomeOdd + 0.65;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd <= 1.04)
                    {
                        odd = 1.03;
                    }

                    if (HomeOdd >= 1.05 && HomeOdd <= 1.07)
                    {
                        odd = 1.05;
                    }

                    if (HomeOdd >= 1.08 && HomeOdd <= 1.11)
                    {
                        odd = 1.08;
                    }

                    if (HomeOdd >= 1.12 && HomeOdd <= 1.14)
                    {
                        odd = 1.1;
                    }

                    if (HomeOdd >= 1.15 && HomeOdd <= 1.17)
                    {
                        odd = 1.12;
                    }

                    if (HomeOdd >= 1.18 && HomeOdd <= 1.25)
                    {
                        odd = 1.14;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeHandicap025Odd
        {
            get
            {
                double odd = 0.45 * HomeOdd + 0.391;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd <= 1.4)
                    {
                        odd = 1.05;
                    }

                    if (HomeOdd >= 1.41 && HomeOdd <= 1.5)
                    {
                        odd = 1.09;
                    }

                    if (HomeOdd >= 1.51 && HomeOdd <= 1.6)
                    {
                        odd = 1.12;
                    }

                    if (HomeOdd >= 1.61 && HomeOdd <= 1.7)
                    {
                        odd = 1.16;
                    }

                    if (HomeOdd >= 1.71 && HomeOdd <= 1.8)
                    {
                        odd = 1.2;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeHandicap0Odd
        {
            get
            {
                double odd = 0.711 * HomeOdd - 0.0375;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd < 1.15)
                    {
                        odd = 1.04;
                    }

                    if (HomeOdd >= 1.15 && HomeOdd < 1.25)
                    {
                        odd = 1.06;
                    }

                    if (HomeOdd >= 1.25 && HomeOdd < 1.35)
                    {
                        odd = 1.1;
                    }

                    if (HomeOdd >= 1.35 && HomeOdd <= 1.5)
                    {
                        odd = 1.15;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeHandicap025NegativeOdd
        {
            get
            {
                double odd = 0.898 * HomeOdd - 0.114;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd <= 1.06)
                    {
                        odd = 1.04;
                    }

                    if (HomeOdd >= 1.07 && HomeOdd <= 1.1)
                    {
                        odd = 1.07;
                    }

                    if (HomeOdd >= 1.1 && HomeOdd <= 1.18)
                    {
                        odd = 1.1;
                    }

                    if (HomeOdd >= 1.19 && HomeOdd <= 1.3)
                    {
                        odd = 1.14;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeHandicap05NegativeOdd
        {
            get
            {
                return HomeOdd;
            }
        }

        public double HomeHandicap075NegativeOdd
        {
            get
            {
                double odd = 1.51 * HomeOdd - 0.665;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd <= 1.09)
                    {
                        odd = 1.12;
                    }

                    if (HomeOdd >= 1.1 && HomeOdd <= 1.12)
                    {
                        odd = 1.15;
                    }

                    if (HomeOdd >= 1.13 && HomeOdd <= 1.16)
                    {
                        odd = 1.18;
                    }

                    if (HomeOdd >= 1.17 && HomeOdd <= 1.22)
                    {
                        odd = 1.25;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeHandicap1NegativeOdd
        {
            get
            {
                double odd = 2.36 * HomeOdd - 1.46;

                if (odd < 1.01)
                {
                    if (HomeOdd >= 1 && HomeOdd <= 1.15)
                    {
                        odd = 1.2;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap1Odd
        {
            get
            {
                double odd = 0.195 * AwayOdd + 0.647;

                if (odd < 1.01)
                {
                    if (AwayOdd >= 1 && AwayOdd <= 1.65)
                    {
                        odd = 1.03;
                    }

                    if (AwayOdd > 1.65 && AwayOdd <= 1.83)
                    {
                        odd = 1.05;
                    }

                    if (AwayOdd >= 1.84 && AwayOdd <= 1.93)
                    {
                        odd = 1.09;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap075Odd
        {
            get
            {
                double odd = 0.238 * AwayOdd + 0.653;

                if (odd < 1.01)
                {
                    if (AwayOdd >= 1 && AwayOdd < 1.2)
                    {
                        odd = 1.03;
                    }

                    if (AwayOdd >= 1.2 && AwayOdd < 1.32)
                    {
                        odd = 1.05;
                    }

                    if (AwayOdd >= 1.32 && AwayOdd <= 1.45)
                    {
                        odd = 1.08;
                    }

                    if (AwayOdd >= 1.46 && AwayOdd <= 1.6)
                    {
                        odd = 1.11;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap05Odd
        {
            get
            {
                double odd = 0.309 * AwayOdd + 0.609;

                if (odd < 1.01)
                {
                    if (AwayOdd >= 1 && AwayOdd <= 1.08)
                    {
                        odd = 1.04;
                    }

                    if (AwayOdd >= 1.08 && AwayOdd <= 1.11)
                    {
                        odd = 1.06;
                    }

                    if (AwayOdd >= 1.12 && AwayOdd <= 1.14)
                    {
                        odd = 1.08;
                    }

                    if (AwayOdd >= 1.15 && AwayOdd <= 1.17)
                    {
                        odd = 1.1;
                    }

                    if (AwayOdd >= 1.18 && AwayOdd <= 1.21)
                    {
                        odd = 1.12;
                    }

                    if (AwayOdd >= 1.22 && AwayOdd <= 1.28)
                    {
                        odd = 1.14;
                    }

                    if (AwayOdd >= 1.29 && AwayOdd <= 1.35)
                    {
                        odd = 1.17;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap025Odd
        {
            get
            {
                double odd = 0.456 * AwayOdd + 0.352;

                if (odd < 1.01)
                {
                    if (AwayOdd >= 1 && AwayOdd < 1.1)
                    {
                        odd = 1.03;
                    }

                    if (AwayOdd >= 1.1 && AwayOdd < 1.2)
                    {
                        odd = 1.05;
                    }

                    if (AwayOdd >= 1.2 && AwayOdd <= 1.3)
                    {
                        odd = 1.08;
                    }

                    if (AwayOdd >= 1.31 && AwayOdd <= 1.43)
                    {
                        odd = 1.11;
                    }

                    if (AwayOdd >= 1.44 && AwayOdd <= 1.55)
                    {
                        odd = 1.15;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap0Odd
        {
            get
            {
                double odd = 0.835 * AwayOdd - 0.417;

                if (odd < 1.01)
                {
                    if (AwayOdd >= 1 && AwayOdd <= 1.15)
                    {
                        odd = 1.04;
                    }

                    if (AwayOdd >= 1.16 && AwayOdd < 1.25)
                    {
                        odd = 1.07;
                    }

                    if (AwayOdd >= 1.25 && AwayOdd <= 1.3)
                    {
                        odd = 1.1;
                    }

                    if (AwayOdd >= 1.31 && AwayOdd < 1.4)
                    {
                        odd = 1.14;
                    }

                    if (AwayOdd >= 1.4 && AwayOdd <= 1.49)
                    {
                        odd = 1.19;
                    }

                    if (AwayOdd >= 1.5 && AwayOdd <= 1.65)
                    {
                        odd = 1.25;
                    }

                    if (AwayOdd > 1.65 && AwayOdd <= 1.8)
                    {
                        odd = 1.3;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap025NegativeOdd
        {
            get
            {
                double odd = 0.944 * AwayOdd - 0.27;

                if (odd < 1.01)
                {
                    if (AwayOdd >= 1 && AwayOdd <= 1.07)
                    {
                        odd = 1.02;
                    }

                    if (AwayOdd >= 1.08 && AwayOdd <= 1.14)
                    {
                        odd = 1.05;
                    }

                    if (AwayOdd >= 1.15 && AwayOdd <= 1.21)
                    {
                        odd = 1.08;
                    }

                    if (AwayOdd >= 1.22 && AwayOdd < 1.28)
                    {
                        odd = 1.12;
                    }

                    if (AwayOdd >= 1.28 && AwayOdd < 1.34)
                    {
                        odd = 1.17;
                    }

                    if (AwayOdd >= 1.34 && AwayOdd <= 1.4)
                    {
                        odd = 1.2;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap05NegativeOdd
        {
            get
            {
                return AwayOdd;
            }
        }

        public double AwayHandicap075NegativeOdd
        {
            get
            {
                double odd = 0.236 * AwayOdd + 0.677;

                if (odd < 1.01)
                {
                    if (AwayOdd >= 1 && AwayOdd <= 1.4)
                    {
                        odd = 1.05;
                    }

                    if (AwayOdd >= 1.41 && AwayOdd <= 1.49)
                    {
                        odd = 1.07;
                    }

                    if (AwayOdd >= 1.5 && AwayOdd <= 1.62)
                    {
                        odd = 1.1;
                    }

                    if (AwayOdd >= 1.63 && AwayOdd <= 1.8)
                    {
                        odd = 1.12;
                    }
                }

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double AwayHandicap1NegativeOdd
        {
            get
            {
                double odd = 2.36 * AwayOdd - 1.46;

                if (odd < 1.01)
                    throw new Exception("Odd menor que 1.01");

                return odd;
            }
        }

        public double HomeOddPercent
        {
            get
            {
                return 1 / HomeOdd;
            }
        }

        public double DrawOddPercent
        {
            get
            {
                return 1 / DrawOdd;
            }
        }

        public double AwayOddPercent
        {
            get
            {
                return 1 / AwayOdd;
            }
        }

        public double Over25OddPercent
        {
            get
            {
                return 1 / Over25Odd;
            }
        }

        public double Under25OddPercent
        {
            get
            {
                return 1 / Under25OddPercent;
            }
        }

        public double BttsYesOddPercent
        {
            get
            {
                return 1 / BttsYesOdd;
            }
        }

        public double BttsNoOddPercent
        {
            get
            {
                return 1 / BttsNoOdd;
            }
        }

        public int TotalGoals
        {
            get
            {
                return HomeGoals + AwayGoals;
            }
        }

        public int DifferenceGoals
        {
            get
            {
                return HomeGoals - AwayGoals;
            }
        }
    }
}
