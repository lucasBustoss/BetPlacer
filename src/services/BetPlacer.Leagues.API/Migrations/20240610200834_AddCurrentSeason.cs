using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlacer.Leagues.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentSeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "current",
                table: "league_seasons",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current",
                table: "league_seasons");
        }
    }
}
