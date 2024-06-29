using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Punter.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCombinedBacktestCodeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "punter_backtest_model",
                table: "punter_backtest_combined_interval",
                newName: "punter_backtest_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "punter_backtest_code",
                table: "punter_backtest_combined_interval",
                newName: "punter_backtest_model");
        }
    }
}
