namespace BetPlacer.Core.API.Utils
{
    public static class PinnacleUtils
    {
        public static int GetPinnacleLeagueCode(int leagueCode)
        {
            int pinnacleLeagueCode = 0;
            
            switch (leagueCode)
            {
                // Brazil Serie A
                case 1665:
                    pinnacleLeagueCode = 1834;
                    break;
                
                // Brazil Serie B
                case 1666:
                    pinnacleLeagueCode = 1835;
                    break;
                
                // Norway Eliteserien
                case 1656:
                    pinnacleLeagueCode = 2333;
                    break;

                // USA MLS
                case 1645:
                    pinnacleLeagueCode = 2663;
                    break;

                // Egypt Egyptian Premier League
                case 1683:
                    pinnacleLeagueCode = 9885;
                    break;

                // Japan J1 League
                case 1682:
                    pinnacleLeagueCode = 2157;
                    break;

                // Mexico Liga MX
                case 1672:
                    pinnacleLeagueCode = 2242;
                    break;

                // Japan J2 League
                case 1681:
                    pinnacleLeagueCode = 2159;
                    break;

                // Bolivia LFPB
                case 1675:
                    pinnacleLeagueCode = 5595;
                    break;

                // Uruguay Primera División
                case 1679:
                    pinnacleLeagueCode = 5593;
                    break;

                // Sweden Allsvenskan
                case 1667:
                    pinnacleLeagueCode = 1728;
                    break;

                default:
                    break;
            }

            return pinnacleLeagueCode;
        }
    }
}
