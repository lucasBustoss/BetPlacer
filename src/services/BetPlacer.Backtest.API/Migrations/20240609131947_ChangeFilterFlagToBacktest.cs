using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Backtest.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFilterFlagToBacktest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uses_in_fixture",
                table: "backtest_filters");

            migrationBuilder.AddColumn<bool>(
                name: "uses_in_fixture",
                table: "backtest",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uses_in_fixture",
                table: "backtest");

            migrationBuilder.AddColumn<bool>(
                name: "uses_in_fixture",
                table: "backtest_filters",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
