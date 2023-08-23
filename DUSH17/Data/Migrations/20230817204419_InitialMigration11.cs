using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTournaments",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(type: "integer", nullable: false),
                    GroupsCount = table.Column<int>(type: "integer", nullable: false),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    Winers = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTournaments", x => x.CompetitionId);
                    table.ForeignKey(
                        name: "FK_GroupTournaments_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTournaments",
                schema: "xgb_dushbase");
        }
    }
}
