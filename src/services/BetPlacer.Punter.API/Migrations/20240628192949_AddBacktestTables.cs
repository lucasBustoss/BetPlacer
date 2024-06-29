using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetPlacer.Punter.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBacktestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "punter_backtest",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    league_code = table.Column<int>(type: "integer", nullable: false),
                    result_after_classification = table.Column<double>(type: "double precision", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_punter_backtest", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "punter_backtest_classification",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    punter_backtest_code = table.Column<int>(type: "integer", nullable: false),
                    classification = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_punter_backtest_classification", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "punter_backtest_combined_interval",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    punter_backtest_model = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    percent_matches = table.Column<double>(type: "double precision", nullable: false),
                    result = table.Column<double>(type: "double precision", nullable: false),
                    coefficient_variation = table.Column<double>(type: "double precision", nullable: false),
                    inferior_limit = table.Column<double>(type: "double precision", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_punter_backtest_combined_interval", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "punter_backtest_interval",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    punter_backtest_code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    initial_value = table.Column<double>(type: "double precision", nullable: false),
                    final_value = table.Column<double>(type: "double precision", nullable: false),
                    coefficient_variation = table.Column<double>(type: "double precision", nullable: false),
                    inferior_limit = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_punter_backtest_interval", x => x.code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "punter_backtest");

            migrationBuilder.DropTable(
                name: "punter_backtest_classification");

            migrationBuilder.DropTable(
                name: "punter_backtest_combined_interval");

            migrationBuilder.DropTable(
                name: "punter_backtest_interval");
        }
    }
}
