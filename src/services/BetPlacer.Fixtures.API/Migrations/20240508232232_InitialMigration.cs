using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetPlacer.Fixtures.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fixtures",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_code = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    home_team_id = table.Column<int>(type: "integer", nullable: false),
                    away_team_id = table.Column<int>(type: "integer", nullable: false),
                    home_team_name = table.Column<string>(type: "text", nullable: false),
                    away_team_name = table.Column<string>(type: "text", nullable: false),
                    home_team_image = table.Column<string>(type: "text", nullable: false),
                    away_team_image = table.Column<string>(type: "text", nullable: false),
                    home_team_goals = table.Column<int>(type: "integer", nullable: false),
                    away_team_goals = table.Column<int>(type: "integer", nullable: false),
                    home_team_p_p_g = table.Column<double>(type: "double precision", nullable: false),
                    away_team_p_p_g = table.Column<double>(type: "double precision", nullable: false),
                    home_team_x_g = table.Column<double>(type: "double precision", nullable: false),
                    away_team_x_g = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_fixtures", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "fixture_goals",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fixture_code = table.Column<int>(type: "integer", nullable: false),
                    minute = table.Column<string>(type: "text", nullable: false),
                    team_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_fixture_goals", x => x.code);
                    table.ForeignKey(
                        name: "f_k_fixture_goals__fixtures_fixture_code",
                        column: x => x.fixture_code,
                        principalTable: "fixtures",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fixture_stats_trade",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fixture_code = table.Column<int>(type: "integer", nullable: false),
                    home_ppg_total = table.Column<double>(type: "double precision", nullable: false),
                    home_wins_total = table.Column<int>(type: "integer", nullable: false),
                    home_wins_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    home_first_to_score_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    home_to_score_two_zero_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    home_clean_sheets_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    home_failed_to_score_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    home_both_to_score_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_total = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_total = table.Column<int>(type: "integer", nullable: false),
                    home_average_goals_scored_total = table.Column<double>(type: "double precision", nullable: false),
                    home_average_goals_conceded_total = table.Column<double>(type: "double precision", nullable: false),
                    home_wins_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    home_draws_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    home_losses_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    home_failed_to_score_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    home_clean_sheets_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_ht_total = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_ht_total = table.Column<int>(type: "integer", nullable: false),
                    home_avg_goals_scored_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    home_average_goals_conceded_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    home_wins_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    home_draws_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    home_losses_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    home_failed_to_score_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    home_clean_sheets_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_ft_total = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_ft_total = table.Column<int>(type: "integer", nullable: false),
                    home_avg_goals_scored_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    home_average_goals_conceded_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_0_to_15 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_0_to_15_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_16_to_30 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_16_to_30_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_31_to_45 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_31_to_45_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_46_to_60 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_46_to_60_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_61_to_75 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_61_to_75_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_75_to_90 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_75_to_90_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_0_to_15 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_0_to_15_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_16_to_30 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_16_to_30_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_31_to_45 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_31_to_45_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_46_to_60 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_46_to_60_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_61_to_75 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_61_to_75_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_75_to_90 = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_75_to_90_percent = table.Column<double>(type: "double precision", nullable: false),
                    home_ppg_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_wins_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_wins_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_first_to_score_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_to_score_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_clean_sheets_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_failed_to_score_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_both_to_score_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_average_goals_scored_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_average_goals_conceded_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_wins_percent_ht_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_draws_percent_ht_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_losses_percent_ht_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_failed_to_score_percent_ht_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_clean_sheets_percent_ht_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_ht_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_ht_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_avg_goals_scored_ht_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_avg_goals_conceded_ht_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_wins_percent_ft_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_draws_percent_ft_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_losses_percent_ft_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_failed_to_score_percent_ft_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_clean_sheets_percent_ft_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_ft_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_ft_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_avg_goals_scored_ft_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_avg_goals_conceded_ft_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_0_to_15_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_0_to_15_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_15_to_30_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_15_to_30_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_31_to_45_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_31_to_45_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_46_to_60_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_46_to_60_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_61_to_75_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_61_to_75_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_scored_at_76_to_90_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_scored_at_76_to_90_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_0_to_15_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_0_to_15_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_15_to_30_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_15_to_30_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_31_to_45_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_31_to_45_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_46_to_60_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_46_to_60_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_61_to_75_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_61_to_75_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    home_goals_conceded_at_76_to_90_at_home = table.Column<int>(type: "integer", nullable: false),
                    home_goals_conceded_at_76_to_90_percent_at_home = table.Column<double>(type: "double precision", nullable: false),
                    away_ppg_total = table.Column<double>(type: "double precision", nullable: false),
                    away_wins_total = table.Column<int>(type: "integer", nullable: false),
                    away_wins_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    away_first_to_score_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    away_to_score_two_zero_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    away_clean_sheets_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    away_failed_to_score_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    away_both_to_score_percent_total = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_total = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_total = table.Column<int>(type: "integer", nullable: false),
                    away_average_goals_scored_total = table.Column<double>(type: "double precision", nullable: false),
                    away_average_goals_conceded_total = table.Column<double>(type: "double precision", nullable: false),
                    away_wins_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    away_draws_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    away_losses_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    away_failed_to_score_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    away_clean_sheets_percent_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_ht_total = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_ht_total = table.Column<int>(type: "integer", nullable: false),
                    away_avg_goals_scored_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    away_average_goals_conceded_ht_total = table.Column<double>(type: "double precision", nullable: false),
                    away_wins_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    away_draws_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    away_losses_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    away_failed_to_score_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    away_clean_sheets_percent_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_ft_total = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_ft_total = table.Column<int>(type: "integer", nullable: false),
                    away_avg_goals_scored_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    away_average_goals_conceded_ft_total = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_0_to_15 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_0_to_15_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_16_to_30 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_16_to_30_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_31_to_45 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_31_to_45_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_46_to_60 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_46_to_60_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_61_to_75 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_61_to_75_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_75_to_90 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_75_to_90_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_0_to_15 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_0_to_15_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_16_to_30 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_16_to_30_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_31_to_45 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_31_to_45_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_46_to_60 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_46_to_60_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_61_to_75 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_61_to_75_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_75_to_90 = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_75_to_90_percent = table.Column<double>(type: "double precision", nullable: false),
                    away_ppg_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_wins_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_wins_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_first_to_score_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_to_score_two_zero_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_clean_sheets_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_failed_to_score_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_both_to_score_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_average_goals_scored_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_average_goals_conceded_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_wins_percent_ht_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_draws_percent_ht_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_losses_percent_ht_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_failed_to_score_percent_ht_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_clean_sheets_percent_ht_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_ht_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_ht_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_avg_goals_scored_ht_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_avg_goals_conceded_ht_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_wins_percent_ft_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_draws_percent_ft_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_losses_percent_ft_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_failed_to_score_percent_ft_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_clean_sheets_percent_ft_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_ft_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_ft_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_avg_goals_scored_ft_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_avg_goals_conceded_ft_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_0_to_15_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_0_to_15_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_15_to_30_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_15_to_30_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_31_to_45_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_31_to_45_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_46_to_60_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_46_to_60_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_61_to_75_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_61_to_75_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_scored_at_76_to_90_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_scored_at_76_to_90_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_0_to_15_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_0_to_15_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_15_to_30_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_15_to_30_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_31_to_45_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_31_to_45_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_46_to_60_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_46_to_60_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_61_to_75_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_61_to_75_percent_at_away = table.Column<double>(type: "double precision", nullable: false),
                    away_goals_conceded_at_76_to_90_at_away = table.Column<int>(type: "integer", nullable: false),
                    away_goals_conceded_at_76_to_90_percent_at_away = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_fixture_stats_trade", x => x.code);
                    table.ForeignKey(
                        name: "f_k_fixture_stats_trade_fixtures_fixture_code",
                        column: x => x.fixture_code,
                        principalTable: "fixtures",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fixture_goals_fixture_code",
                table: "fixture_goals",
                column: "fixture_code");

            migrationBuilder.CreateIndex(
                name: "IX_fixture_stats_trade_fixture_code",
                table: "fixture_stats_trade",
                column: "fixture_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fixture_goals");

            migrationBuilder.DropTable(
                name: "fixture_stats_trade");

            migrationBuilder.DropTable(
                name: "fixtures");
        }
    }
}
