using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Fixtures.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHomeCountAwayAndInverse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "away_matches_count_at_home",
                table: "fixture_stats_trade");

            migrationBuilder.DropColumn(
                name: "home_matches_count_at_away",
                table: "fixture_stats_trade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "away_matches_count_at_home",
                table: "fixture_stats_trade",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "home_matches_count_at_away",
                table: "fixture_stats_trade",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
