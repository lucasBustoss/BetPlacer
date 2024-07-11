namespace BetPlacer.Punter.API.Repositories
{
    public static class Queries
    {
        public static string GetMatchBaseDataQuery(int leagueCode)
        {
            return $@"
                
                
                
                WITH base_league_data AS (
                    SELECT 
                        f.code as ""MatchCode"",
                        f.status as ""Status"",
                        ls.year AS ""Season"",
                        f.start_date as ""UtcDate"",
                        TO_CHAR(DATE(f.start_date- INTERVAL '3 hours'), 'dd/MM/yyyy') AS ""Date"",
                        f.home_team_name AS ""HomeTeam"",
                        f.away_team_name AS ""AwayTeam"",
                        fo.home_odd AS ""HomeOdd"",
                        fo.draw_odd AS ""DrawOdd"",
                        fo.away_odd AS ""AwayOdd"",
                        fo.over25_odd AS ""Over25Odd"",
                        fo.UNDER25_odd AS ""Under25Odd"",
                        fo.btts_yes_odd AS ""BttsYesOdd"",
                        fo.btts_no_odd AS ""BttsNoOdd"",
                        COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) AS ""HomeGoals"",
                        COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) AS ""AwayGoals"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                                > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 'H'
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                                < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 'A'
                            ELSE 'D'
                        END AS ""MatchResult"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                                + COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) > 2 THEN 'OV'
                            ELSE 'UN'
                        END AS ""GoalsResult"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) > 0
                                AND COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) > 0 THEN 'S'
                            ELSE 'N'
                        END AS ""BttsResult"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                                > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 3
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                                < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 0
                            ELSE 1
                        END AS ""HomePoints"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                                > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 0
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                                < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 3
                            ELSE 1
                        END AS ""AwayPoints"",
                        COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                            -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) AS ""HomeGoalsDifference"",
                        COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) 
                            -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) AS ""AwayGoalsDifference"",
                        COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) AS ""HomeGoalsHT"",
                        COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) AS ""AwayGoalsHT"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                                > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 'H'
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                                < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 'A'
                            ELSE 'D'
                        END AS ""MatchResultHT"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                                > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 3
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                                < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 0
                            ELSE 1
                        END AS ""HomePointsHT"",
                        CASE
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                                > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 0
                            WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                                < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 3
                            ELSE 1
                        END AS ""AwayPointsHT"",
								COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                            -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) AS ""HomeGoalsDifferenceHT"",
                        COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) 
                            -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) AS ""AwayGoalsDifferenceHT"",
                        (1 / fo.home_odd) AS ""HomePercentageOdd"",
                        (1 / fo.away_odd) AS ""AwayPercentageOdd""
                    FROM fixtures f
                    INNER JOIN fixture_odds fo ON fo.fixture_code = f.code
                    INNER JOIN league_seasons ls ON ls.""code"" = f.season_code
                    WHERE ls.league_code = {leagueCode} AND f.status = 'complete'
                    ORDER BY f.start_date, f.code
                )

                SELECT
                    ""MatchCode"",
                    ""Status"",
                    ""Season"",
                    ""UtcDate"",
                    ""Date"",
                    ""HomeTeam"",
                    ""AwayTeam"",
                    ""HomeOdd"",
                    ""DrawOdd"",
                    ""AwayOdd"",
                    ""Over25Odd"",
                    ""Under25Odd"",
                    ""BttsYesOdd"",
                    ""BttsNoOdd"",
                    ""HomeGoals"",
                    ""AwayGoals"",
                    ""MatchResult"",
                    ""GoalsResult"",
                    ""BttsResult"",
                    ""HomePoints"",
                    ""AwayPoints"",
                    ""HomeGoalsDifference"",
                    ""AwayGoalsDifference"",
                    ""HomeGoalsHT"",
                    ""AwayGoalsHT"",
                    ""MatchResultHT"",
                    ""HomePointsHT"",
                    ""AwayPointsHT"",
                    ""HomeGoalsDifferenceHT"",
                    ""AwayGoalsDifferenceHT"",
                    ""HomePercentageOdd"",
                    ""AwayPercentageOdd"",
                    (""HomeGoals"" * ""AwayPercentageOdd"") AS ""HomeScoredGoalValue"",
                    CASE
                        WHEN ""HomeGoals"" > 0
                        THEN ""HomePercentageOdd"" / ""HomeGoals""
                        ELSE 1
                    END AS ""HomeScoredGoalCost"",
                    ""AwayGoals"" * ""HomePercentageOdd"" AS ""AwayScoredGoalValue"",
                    CASE
                        WHEN ""AwayGoals"" > 0
                        THEN ""AwayPercentageOdd"" / ""AwayGoals""
                        ELSE 1
                    END AS ""AwayScoredGoalCost"",
                    ""AwayGoals"" * ""AwayPercentageOdd"" AS ""HomeConcededGoalValue"",
                    CASE
                        WHEN ""AwayGoals"" > 0
                        THEN ""HomePercentageOdd"" / ""AwayGoals""
                        ELSE 0
                    END AS ""HomeConcededGoalCost"",
                    ""HomeGoals"" * ""HomePercentageOdd"" AS ""AwayConcededGoalValue"",
                    CASE
                        WHEN ""HomeGoals"" > 0
                        THEN ""AwayPercentageOdd"" / ""HomeGoals""
                        ELSE 0
                    END AS ""AwayConcededGoalCost"",
                    ""HomePoints"" * ""AwayPercentageOdd"" AS ""HomePointsValue"",
                    ""AwayPoints"" * ""HomePercentageOdd"" AS ""AwayPointsValue"",
                    ""HomeGoalsDifference"" * ""AwayPercentageOdd"" AS ""HomeGoalsDifferenceValue"",
                    ""AwayGoalsDifference"" * ""HomePercentageOdd"" AS ""AwayGoalsDifferenceValue"",
                    ""HomeGoalsHT"" * ""AwayPercentageOdd"" AS ""HomeScoredGoalValueHT"",
                    CASE
                        WHEN ""HomeGoalsHT"" > 0
                        THEN ""HomePercentageOdd"" / ""HomeGoalsHT""
                        ELSE 1
                    END AS ""HomeScoredGoalCostHT"",
                    ""AwayGoalsHT"" * ""HomePercentageOdd"" AS ""AwayScoredGoalValueHT"",
                    CASE
                        WHEN ""AwayGoalsHT"" > 0
                        THEN ""AwayPercentageOdd"" / ""AwayGoalsHT""
                        ELSE 1
                    END AS ""AwayScoredGoalCostHT"",
                    ""AwayGoalsHT"" * ""AwayPercentageOdd"" AS ""HomeConcededGoalValueHT"",
                    CASE
                        WHEN ""AwayGoalsHT"" > 0
                        THEN ""HomePercentageOdd"" / ""AwayGoalsHT""
                        ELSE 0
                    END AS ""HomeConcededGoalCostHT"",
                    ""HomeGoalsHT"" * ""HomePercentageOdd"" AS ""AwayConcededGoalValueHT"",
                    CASE
                        WHEN ""HomeGoalsHT"" > 0
                        THEN ""AwayPercentageOdd"" / ""HomeGoalsHT""
                        ELSE 0
                    END AS ""AwayConcededGoalCostHT"",
                    ""HomePointsHT"" * ""AwayPercentageOdd"" AS ""HomePointsValueHT"",
                    ""AwayPointsHT"" * ""HomePercentageOdd"" AS ""AwayPointsValueHT"",
                    ""HomeGoalsDifferenceHT"" * ""AwayPercentageOdd"" AS ""HomeGoalsDifferenceValueHT"",
                    ""AwayGoalsDifferenceHT"" * ""HomePercentageOdd"" AS ""AwayGoalsDifferenceValueHT""
                FROM base_league_data;
            
            
            
            ";
        }

        public static string GetLastMatches(int leagueCode)
        {
            return $@"
                
                 WITH base_league_data AS (
                 SELECT 
                     f.code as ""MatchCode"",
                     f.status as ""Status"",
                     ls.year AS ""Season"",
                     f.start_date as ""UtcDate"",
                     TO_CHAR(DATE(f.start_date), 'dd/MM/yyyy') AS ""Date"",
                     f.home_team_name AS ""HomeTeam"",
                     f.away_team_name AS ""AwayTeam"",
                     fo.home_odd AS ""HomeOdd"",
                     fo.draw_odd AS ""DrawOdd"",
                     fo.away_odd AS ""AwayOdd"",
                     fo.over25_odd AS ""Over25Odd"",
                     fo.UNDER25_odd AS ""Under25Odd"",
                     fo.btts_yes_odd AS ""BttsYesOdd"",
                     fo.btts_no_odd AS ""BttsNoOdd"",
                     COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) AS ""HomeGoals"",
                     COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) AS ""AwayGoals"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                             > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 'H'
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                             < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 'A'
                         ELSE 'D'
                     END AS ""MatchResult"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                             + COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) > 2 THEN 'OV'
                         ELSE 'UN'
                     END AS ""GoalsResult"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) > 0
                             AND COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) > 0 THEN 'S'
                         ELSE 'N'
                     END AS ""BttsResult"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                             > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 3
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                             < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 0
                         ELSE 1
                     END AS ""HomePoints"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                             > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 0
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                             < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) THEN 3
                         ELSE 1
                     END AS ""AwayPoints"",
                     COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) 
                         -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) AS ""HomeGoalsDifference"",
                     COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id), 0) 
                         -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id), 0) AS ""AwayGoalsDifference"",
                     COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) AS ""HomeGoalsHT"",
                     COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) AS ""AwayGoalsHT"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                             > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 'H'
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                             < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 'A'
                         ELSE 'D'
                     END AS ""MatchResultHT"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                             > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 3
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                             < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 0
                         ELSE 1
                     END AS ""HomePointsHT"",
                     CASE
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                             > COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 0
                         WHEN COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                             < COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) THEN 3
                         ELSE 1
                     END AS ""AwayPointsHT"",
					            COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) 
                         -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) AS ""HomeGoalsDifferenceHT"",
                     COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.away_team_id AND MINUTE::FLOAT < 46), 0) 
                         -COALESCE((SELECT COUNT(*) FROM fixture_goals WHERE fixture_code = f.code AND team_id = f.home_team_id AND MINUTE::FLOAT < 46), 0) AS ""AwayGoalsDifferenceHT"",
                     (1 / fo.home_odd) AS ""HomePercentageOdd"",
                     (1 / fo.away_odd) AS ""AwayPercentageOdd""
                 FROM fixtures f
                 INNER JOIN fixture_odds fo ON fo.fixture_code = f.code
                 INNER JOIN league_seasons ls ON ls.""code"" = f.season_code
                 WHERE ls.league_code = {leagueCode} AND f.status = 'complete'
                 ORDER BY f.start_date desc, f.code
             )

             SELECT
                 ""MatchCode"",
                 ""Status"",
                 ""Season"",
                 ""UtcDate"",
                 ""Date"",
                 ""HomeTeam"",
                 ""AwayTeam"",
                 ""HomeOdd"",
                 ""DrawOdd"",
                 ""AwayOdd"",
                 ""Over25Odd"",
                 ""Under25Odd"",
                 ""BttsYesOdd"",
                 ""BttsNoOdd"",
                 ""HomeGoals"",
                 ""AwayGoals"",
                 ""MatchResult"",
                 ""GoalsResult"",
                 ""BttsResult"",
                 ""HomePoints"",
                 ""AwayPoints"",
                 ""HomeGoalsDifference"",
                 ""AwayGoalsDifference"",
                 ""HomeGoalsHT"",
                 ""AwayGoalsHT"",
                 ""MatchResultHT"",
                 ""HomePointsHT"",
                 ""AwayPointsHT"",
                 ""HomeGoalsDifferenceHT"",
                 ""AwayGoalsDifferenceHT"",
                 ""HomePercentageOdd"",
                 ""AwayPercentageOdd"",
                 ""HomeGoals"" * ""AwayPercentageOdd"" AS ""HomeScoredGoalValue"",
                 CASE
                     WHEN ""HomeGoals"" > 0
                     THEN (""HomePercentageOdd"" / ""HomeGoals"")
                     ELSE 1
                 END AS ""HomeScoredGoalCost"",
                 (""AwayGoals"" * ""HomePercentageOdd"") AS ""AwayScoredGoalValue"",
                 CASE
                     WHEN ""AwayGoals"" > 0
                     THEN (""AwayPercentageOdd"" / ""AwayGoals"")
                     ELSE 1
                 END AS ""AwayScoredGoalCost"",
                 (""AwayGoals"" * ""AwayPercentageOdd"") AS ""HomeConcededGoalValue"",
                 CASE
                     WHEN ""AwayGoals"" > 0
                     THEN (""HomePercentageOdd"" / ""AwayGoals"")
                     ELSE 0
                 END AS ""HomeConcededGoalCost"",
                 (""HomeGoals"" * ""HomePercentageOdd"") AS ""AwayConcededGoalValue"",
                 CASE
                     WHEN ""HomeGoals"" > 0
                     THEN (""AwayPercentageOdd"" / ""HomeGoals"")
                     ELSE 0
                 END AS ""AwayConcededGoalCost"",
                 (""HomePoints"" * ""AwayPercentageOdd"") AS ""HomePointsValue"",
                 (""AwayPoints"" * ""HomePercentageOdd"") AS ""AwayPointsValue"",
                 (""HomeGoalsDifference"" * ""AwayPercentageOdd"") AS ""HomeGoalsDifferenceValue"",
                 (""AwayGoalsDifference"" * ""HomePercentageOdd"") AS ""AwayGoalsDifferenceValue"",
                 (""HomeGoalsHT"" * ""AwayPercentageOdd"") AS ""HomeScoredGoalValueHT"",
                 CASE
                     WHEN ""HomeGoalsHT"" > 0
                     THEN (""HomePercentageOdd"" / ""HomeGoalsHT"")
                     ELSE 1
                 END AS ""HomeScoredGoalCostHT"",
                 (""AwayGoalsHT"" * ""HomePercentageOdd"") AS ""AwayScoredGoalValueHT"",
                 CASE
                     WHEN ""AwayGoalsHT"" > 0
                     THEN (""AwayPercentageOdd"" / ""AwayGoalsHT"")
                     ELSE 1
                 END AS ""AwayScoredGoalCostHT"",
                 (""AwayGoalsHT"" * ""AwayPercentageOdd"") AS ""HomeConcededGoalValueHT"",
                 CASE
                     WHEN ""AwayGoalsHT"" > 0
                     THEN (""HomePercentageOdd"" / ""AwayGoalsHT"")
                     ELSE 0
                 END AS ""HomeConcededGoalCostHT"",
                 (""HomeGoalsHT"" * ""HomePercentageOdd"") AS ""AwayConcededGoalValueHT"",
                 CASE
                     WHEN ""HomeGoalsHT"" > 0
                     THEN (""AwayPercentageOdd"" / ""HomeGoalsHT"")
                     ELSE 0
                 END AS ""AwayConcededGoalCostHT"",
                 (""HomePointsHT"" * ""AwayPercentageOdd"") AS ""HomePointsValueHT"",
                 (""AwayPointsHT"" * ""HomePercentageOdd"") AS ""AwayPointsValueHT"",
                 (""HomeGoalsDifferenceHT"" * ""AwayPercentageOdd"") AS ""HomeGoalsDifferenceValueHT"",
                 (""AwayGoalsDifferenceHT"" * ""HomePercentageOdd"") AS ""AwayGoalsDifferenceValueHT""
             FROM base_league_data;
            
            ";
        }

        public static string GetNextMatches(string date, int leagueCode)
        {
            return $@"
                SELECT 
	                f.code as ""MatchCode"",
	                ls.year AS ""Season"",
                    f.start_date as ""UtcDate"",
	                TO_CHAR(DATE(f.start_date), 'dd/MM/yyyy') AS ""Date"",
	                f.home_team_name AS ""HomeTeam"",
	                f.away_team_name AS ""AwayTeam"",
	                COALESCE(fo.home_odd, 0) AS ""HomeOdd"",
	                COALESCE(fo.draw_odd, 0) AS ""DrawOdd"",
	                COALESCE(fo.away_odd, 0) AS ""AwayOdd"",
	                COALESCE(fo.over25_odd, 0) AS ""Over25Odd"",
	                COALESCE(fo.UNDER25_odd, 0) AS ""Under25Odd"",
	                COALESCE(fo.btts_yes_odd, 0) AS ""BttsYesOdd"",
	                COALESCE(fo.btts_no_odd, 0) AS ""BttsNoOdd""
                FROM fixtures f
                INNER JOIN league_seasons ls ON ls.code = f.season_code
                LEFT JOIN fixture_odds fo ON fo.fixture_code = f.code
                WHERE ls.league_code = {leagueCode} AND status = 'incomplete'
                AND f.start_date BETWEEN DATE_TRUNC('day', TIMESTAMP '{date}') + INTERVAL '3 hours' AND ((DATE_TRUNC('day', TIMESTAMP '{date}') + INTERVAL '3 hours') + INTERVAL '1 day')
        ";
        }
    }
}
