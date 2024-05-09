using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Fixtures.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHomeToScorePercentAtHomeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "home_to_score_percent_at_home",
                table: "fixture_stats_trade",
                newName: "home_to_score_two_zero_percent_at_home");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "home_to_score_two_zero_percent_at_home",
                table: "fixture_stats_trade",
                newName: "home_to_score_percent_at_home");
        }
    }
}
