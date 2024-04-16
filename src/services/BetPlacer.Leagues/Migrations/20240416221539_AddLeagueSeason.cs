using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetPlacer.Leagues.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLeagueSeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "league_seasons",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    league_code = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_league_seasons", x => x.code);
                    table.ForeignKey(
                        name: "f_k_league_seasons_leagues_league_code",
                        column: x => x.league_code,
                        principalTable: "leagues",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_league_seasons_league_code",
                table: "league_seasons",
                column: "league_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "league_seasons");
        }
    }
}
