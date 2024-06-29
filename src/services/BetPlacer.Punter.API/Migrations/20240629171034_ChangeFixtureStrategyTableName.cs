using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Punter.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFixtureStrategyTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "p_k_fixture_strategy_model",
                table: "fixture_strategy_model");

            migrationBuilder.RenameTable(
                name: "fixture_strategy_model",
                newName: "fixture_strategy");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_fixture_strategy",
                table: "fixture_strategy",
                column: "code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "p_k_fixture_strategy",
                table: "fixture_strategy");

            migrationBuilder.RenameTable(
                name: "fixture_strategy",
                newName: "fixture_strategy_model");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_fixture_strategy_model",
                table: "fixture_strategy_model",
                column: "code");
        }
    }
}
