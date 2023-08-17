using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                schema: "xgb_dushbase",
                table: "OpponentsMatches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OpponentsMatches_CompetitionId",
                schema: "xgb_dushbase",
                table: "OpponentsMatches",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpponentsMatches_Competitions_CompetitionId",
                schema: "xgb_dushbase",
                table: "OpponentsMatches",
                column: "CompetitionId",
                principalSchema: "xgb_dushbase",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpponentsMatches_Competitions_CompetitionId",
                schema: "xgb_dushbase",
                table: "OpponentsMatches");

            migrationBuilder.DropIndex(
                name: "IX_OpponentsMatches_CompetitionId",
                schema: "xgb_dushbase",
                table: "OpponentsMatches");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                schema: "xgb_dushbase",
                table: "OpponentsMatches");
        }
    }
}
