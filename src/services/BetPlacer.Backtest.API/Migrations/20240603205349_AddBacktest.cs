using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetPlacer.Backtest.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBacktest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "backtest",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    team_type = table.Column<int>(type: "integer", nullable: false),
                    filtered_fixtures = table.Column<double>(type: "double precision", nullable: false),
                    matched_fixtures = table.Column<double>(type: "double precision", nullable: false),
                    max_good_run = table.Column<int>(type: "integer", nullable: false),
                    max_bad_run = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_backtest", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "backtest_filters",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    backtest_code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    compare_type = table.Column<int>(type: "integer", nullable: false),
                    team_type = table.Column<int>(type: "integer", nullable: false),
                    prop_type = table.Column<int>(type: "integer", nullable: false),
                    initial_value = table.Column<double>(type: "double precision", nullable: false),
                    final_value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_backtest_filters", x => x.code);
                    table.ForeignKey(
                        name: "f_k_backtest_filters__backtest_backtest_code",
                        column: x => x.backtest_code,
                        principalTable: "backtest",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "backtest_league_seasons_list",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    backtest_code = table.Column<int>(type: "integer", nullable: false),
                    league_code = table.Column<int>(type: "integer", nullable: false),
                    league_season_code = table.Column<int>(type: "integer", nullable: false),
                    league_name = table.Column<string>(type: "text", nullable: false),
                    league_season_year = table.Column<string>(type: "text", nullable: false),
                    league_season_ratio = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_backtest_league_seasons_list", x => x.code);
                    table.ForeignKey(
                        name: "f_k_backtest_league_seasons_list_backtest_backtest_code",
                        column: x => x.backtest_code,
                        principalTable: "backtest",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "backtest_leagues_list",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    backtest_code = table.Column<int>(type: "integer", nullable: false),
                    league_code = table.Column<int>(type: "integer", nullable: false),
                    league_name = table.Column<string>(type: "text", nullable: false),
                    league_ratio = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_backtest_leagues_list", x => x.code);
                    table.ForeignKey(
                        name: "f_k_backtest_leagues_list_backtest_backtest_code",
                        column: x => x.backtest_code,
                        principalTable: "backtest",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "backtest_teams_list",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    backtest_code = table.Column<int>(type: "integer", nullable: false),
                    team_code = table.Column<int>(type: "integer", nullable: false),
                    team_name = table.Column<string>(type: "text", nullable: false),
                    team_ratio = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_backtest_teams_list", x => x.code);
                    table.ForeignKey(
                        name: "f_k_backtest_teams_list_backtest_backtest_code",
                        column: x => x.backtest_code,
                        principalTable: "backtest",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_backtest_filters_backtest_code",
                table: "backtest_filters",
                column: "backtest_code");

            migrationBuilder.CreateIndex(
                name: "IX_backtest_league_seasons_list_backtest_code",
                table: "backtest_league_seasons_list",
                column: "backtest_code");

            migrationBuilder.CreateIndex(
                name: "IX_backtest_leagues_list_backtest_code",
                table: "backtest_leagues_list",
                column: "backtest_code");

            migrationBuilder.CreateIndex(
                name: "IX_backtest_teams_list_backtest_code",
                table: "backtest_teams_list",
                column: "backtest_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backtest_filters");

            migrationBuilder.DropTable(
                name: "backtest_league_seasons_list");

            migrationBuilder.DropTable(
                name: "backtest_leagues_list");

            migrationBuilder.DropTable(
                name: "backtest_teams_list");

            migrationBuilder.DropTable(
                name: "backtest");
        }
    }
}
