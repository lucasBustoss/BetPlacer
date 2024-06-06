using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Fixtures.API.Migrations
{
    /// <inheritdoc />
    public partial class AddHTFTColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "away_both_to_score_percent_ft_at_away",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_both_to_score_percent_ft_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_both_to_score_percent_ht_at_away",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_both_to_score_percent_ht_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_first_to_score_percent_ft_at_away",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_first_to_score_percent_ft_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_first_to_score_percent_ht_at_away",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_first_to_score_percent_ht_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_to_score_two_zero_percent_ft_at_away",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_to_score_two_zero_percent_ft_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_to_score_two_zero_percent_ht_at_away",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "away_to_score_two_zero_percent_ht_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_both_to_score_percent_ft_at_home",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_both_to_score_percent_ft_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_both_to_score_percent_ht_at_home",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_both_to_score_percent_ht_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_first_to_score_percent_ft_at_home",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_first_to_score_percent_ft_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_first_to_score_percent_ht_at_home",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_first_to_score_percent_ht_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_to_score_two_zero_percent_ft_at_home",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_to_score_two_zero_percent_ft_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_to_score_two_zero_percent_ht_at_home",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "home_to_score_two_zero_percent_ht_total",
                table: "fixture_stats_trade",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "away_both_to_score_percent_ft_at_away",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_both_to_score_percent_ft_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_both_to_score_percent_ht_at_away",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_both_to_score_percent_ht_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_first_to_score_percent_ft_at_away",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_first_to_score_percent_ft_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_first_to_score_percent_ht_at_away",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_first_to_score_percent_ht_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_to_score_two_zero_percent_ft_at_away",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_to_score_two_zero_percent_ft_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_to_score_two_zero_percent_ht_at_away",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "away_to_score_two_zero_percent_ht_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_both_to_score_percent_ft_at_home",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_both_to_score_percent_ft_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_both_to_score_percent_ht_at_home",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_both_to_score_percent_ht_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_first_to_score_percent_ft_at_home",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_first_to_score_percent_ft_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_first_to_score_percent_ht_at_home",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_first_to_score_percent_ht_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_to_score_two_zero_percent_ft_at_home",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_to_score_two_zero_percent_ft_total",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_to_score_two_zero_percent_ht_at_home",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_to_score_two_zero_percent_ht_total",
                table: "fixture_stats_trade");
        }
    }
}
