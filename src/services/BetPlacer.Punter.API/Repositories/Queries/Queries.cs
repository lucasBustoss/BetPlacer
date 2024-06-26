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
                        ls.year AS ""Season"",
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
                        ROUND((1 / fo.home_odd)::numeric, 2) AS ""HomePercentageOdd"",
                        ROUND((1 / fo.away_odd)::numeric, 2) AS ""AwayPercentageOdd""
                    FROM fixtures f
                    INNER JOIN fixture_odds fo ON fo.fixture_code = f.code
                    INNER JOIN league_seasons ls ON ls.""code"" = f.season_code
                    WHERE ls.league_code = {leagueCode}
                    ORDER BY f.start_date, f.code
                )

                SELECT
                    ""MatchCode"",
                    ""Season"",
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
                    ROUND((""HomeGoals"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomeScoredGoalValue"",
                    CASE
                        WHEN ""HomeGoals"" > 0
                        THEN ROUND((""HomePercentageOdd"" / ""HomeGoals"")::NUMERIC, 2)
                        ELSE 1
                    END AS ""HomeScoredGoalCost"",
                    ROUND((""AwayGoals"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayScoredGoalValue"",
                    CASE
                        WHEN ""AwayGoals"" > 0
                        THEN ROUND((""AwayPercentageOdd"" / ""AwayGoals"")::NUMERIC, 2)
                        ELSE 1
                    END AS ""AwayScoredGoalCost"",
                    ROUND((""AwayGoals"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomeConcededGoalValue"",
                    CASE
                        WHEN ""AwayGoals"" > 0
                        THEN ROUND((""HomePercentageOdd"" / ""AwayGoals"")::numeric, 2)
                        ELSE 0
                    END AS ""HomeConcededGoalCost"",
                    ROUND((""HomeGoals"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayConcededGoalValue"",
                    CASE
                        WHEN ""HomeGoals"" > 0
                        THEN ROUND((""AwayPercentageOdd"" / ""HomeGoals"")::NUMERIC, 2)
                        ELSE 0
                    END AS ""AwayConcededGoalCost"",
                    ROUND((""HomePoints"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomePointsValue"",
                    ROUND((""AwayPoints"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayPointsValue"",
                    ROUND((""HomeGoalsDifference"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomeGoalsDifferenceValue"",
                    ROUND((""AwayGoalsDifference"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayGoalsDifferenceValue"",
                    ROUND((""HomeGoalsHT"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomeScoredGoalValueHT"",
                    CASE
                        WHEN ""HomeGoalsHT"" > 0
                        THEN ROUND((""HomePercentageOdd"" / ""HomeGoalsHT"")::NUMERIC, 2)
                        ELSE 1
                    END AS ""HomeScoredGoalCostHT"",
                    ROUND((""AwayGoalsHT"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayScoredGoalValueHT"",
                    CASE
                        WHEN ""AwayGoalsHT"" > 0
                        THEN ROUND((""AwayPercentageOdd"" / ""AwayGoalsHT"")::NUMERIC, 2)
                        ELSE 1
                    END AS ""AwayScoredGoalCostHT"",
                    ROUND((""AwayGoalsHT"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomeConcededGoalValueHT"",
                    CASE
                        WHEN ""AwayGoalsHT"" > 0
                        THEN ROUND((""HomePercentageOdd"" / ""AwayGoalsHT"")::numeric, 2)
                        ELSE 0
                    END AS ""HomeConcededGoalCostHT"",
                    ROUND((""HomeGoalsHT"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayConcededGoalValueHT"",
                    CASE
                        WHEN ""HomeGoalsHT"" > 0
                        THEN ROUND((""AwayPercentageOdd"" / ""HomeGoalsHT"")::NUMERIC, 2)
                        ELSE 0
                    END AS ""AwayConcededGoalCostHT"",
                    ROUND((""HomePointsHT"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomePointsValueHT"",
                    ROUND((""AwayPointsHT"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayPointsValueHT"",
                    ROUND((""HomeGoalsDifferenceHT"" * ""AwayPercentageOdd"")::NUMERIC, 2) AS ""HomeGoalsDifferenceValueHT"",
                    ROUND((""AwayGoalsDifferenceHT"" * ""HomePercentageOdd"")::NUMERIC, 2) AS ""AwayGoalsDifferenceValueHT""
                FROM base_league_data;
            
            ";
        }
    }
}
