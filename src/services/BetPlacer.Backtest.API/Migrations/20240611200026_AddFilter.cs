using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BetPlacer.Backtest.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "filters",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    prop = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_filters", x => x.code);
                });

            migrationBuilder.InsertData(
                table: "filters",
                columns: new[] { "code", "name", "prop" },
                values: new object[,]
                {
                    { 1, "% de jogos sendo primeiro a marcar", "firstToScorePercent" },
                    { 2, "% de jogos sendo primeiro a marcar 2x0", "twoZeroPercent" },
                    { 3, "% de jogos sem sofrer gols", "cleanSheetPercent" },
                    { 4, "% de jogos em que não marcou gols", "failedToScorePercent" },
                    { 5, "% de jogos em que os dois times marcaram", "bothToScorePercent" },
                    { 6, "Média de gols marcados", "avgGoalsScored" },
                    { 7, "Média de gols sofridos", "avgGoalsConceded" },
                    { 8, "% de jogos sendo primeiro a marcar no HT", "firstToScorePercentHT" },
                    { 9, "% de jogos sendo primeiro a marcar 2x0 no HT", "twoZeroPercentHT" },
                    { 10, "% de jogos sem sofrer gols no HT", "cleanSheetPercentHT" },
                    { 11, "% de jogos em que não marcou gols no HT", "failedToScorePercentHT" },
                    { 12, "% de jogos em que os dois times marcaram no HT", "bothToScorePercentHT" },
                    { 13, "Média de gols marcados no HT", "avgGoalsScoredHT" },
                    { 14, "Média de gols sofridos no HT", "avgGoalsConcededHT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filters");
        }
    }
}
