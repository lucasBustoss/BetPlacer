namespace BetPlacer.Core.API.Utils
{
    public static class PinnacleUtils
    {
        public static int GetPinnacleLeagueCode(int leagueCode)
        {
            int pinnacleLeagueCode = 0;
            
            switch (leagueCode)
            {
                case 1665:
                    pinnacleLeagueCode = 1834;
                    break;

                case 1666:
                    pinnacleLeagueCode = 1835;
                    break;

                case 1656:
                    pinnacleLeagueCode = 2333;
                    break;

                case 1645:
                    pinnacleLeagueCode = 2663;
                    break;

                default:
                    break;
            }

            return pinnacleLeagueCode;
        }
    }
}
