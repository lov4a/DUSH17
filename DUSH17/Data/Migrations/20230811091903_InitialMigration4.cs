using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpponentList",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    OpponentId = table.Column<int>(type: "integer", nullable: false),
                    CompetitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpponentList", x => new { x.OpponentId, x.CompetitionId });
                    table.ForeignKey(
                        name: "FK_OpponentList_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpponentList_Opponents_OpponentId",
                        column: x => x.OpponentId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Opponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpponentList_CompetitionId",
                schema: "xgb_dushbase",
                table: "OpponentList",
                column: "CompetitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpponentList",
                schema: "xgb_dushbase");
        }
    }
}
