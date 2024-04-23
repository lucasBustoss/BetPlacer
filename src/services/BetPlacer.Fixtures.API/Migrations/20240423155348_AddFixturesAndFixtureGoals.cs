using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetPlacer.Fixtures.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFixturesAndFixtureGoals : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_fixture_goals_fixture_code",
                table: "fixture_goals",
                column: "fixture_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fixture_goals");

            migrationBuilder.DropTable(
                name: "fixtures");
        }
    }
}
