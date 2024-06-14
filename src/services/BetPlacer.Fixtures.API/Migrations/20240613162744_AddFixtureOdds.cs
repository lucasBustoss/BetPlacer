using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetPlacer.Fixtures.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFixtureOdds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fixture_odds",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fixture_code = table.Column<int>(type: "integer", nullable: false),
                    home_odd = table.Column<double>(type: "double precision", nullable: false),
                    draw_odd = table.Column<double>(type: "double precision", nullable: false),
                    away_odd = table.Column<double>(type: "double precision", nullable: false),
                    over25_odd = table.Column<double>(type: "double precision", nullable: false),
                    under25_odd = table.Column<double>(type: "double precision", nullable: false),
                    btts_yes_odd = table.Column<double>(type: "double precision", nullable: false),
                    btts_no_odd = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_fixture_odds", x => x.code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fixture_odds");
        }
    }
}
