using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Backtest.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBacktestFilterCodeAndName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "backtest_filters",
                newName: "filter_name");

            migrationBuilder.AddColumn<int>(
                name: "filter_code",
                table: "backtest_filters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "filter_code",
                table: "backtest_filters");

            migrationBuilder.RenameColumn(
                name: "filter_name",
                table: "backtest_filters",
                newName: "name");
        }
    }
}
