using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                schema: "xgb_dushbase",
                table: "Competitions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_PictureId",
                schema: "xgb_dushbase",
                table: "Competitions",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Pictures_PictureId",
                schema: "xgb_dushbase",
                table: "Competitions",
                column: "PictureId",
                principalSchema: "xgb_dushbase",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Pictures_PictureId",
                schema: "xgb_dushbase",
                table: "Competitions");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_PictureId",
                schema: "xgb_dushbase",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "xgb_dushbase",
                table: "TeamList");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "xgb_dushbase",
                table: "OpponentList");

            migrationBuilder.DropColumn(
                name: "Stage",
                schema: "xgb_dushbase",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Minutes",
                schema: "xgb_dushbase",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "PictureId",
                schema: "xgb_dushbase",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "PlayOffs",
                schema: "xgb_dushbase",
                table: "Competitions");
        }
    }
}
