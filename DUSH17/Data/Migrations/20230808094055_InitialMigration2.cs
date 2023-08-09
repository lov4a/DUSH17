using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Opponent",
                schema: "xgb_dushbase",
                table: "Matches");

            migrationBuilder.AddColumn<bool>(
                name: "Home",
                schema: "xgb_dushbase",
                table: "Matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OpponentId",
                schema: "xgb_dushbase",
                table: "Matches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Opponents",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PictureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opponents_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_OpponentId",
                schema: "xgb_dushbase",
                table: "Matches",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Opponents_PictureId",
                schema: "xgb_dushbase",
                table: "Opponents",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Opponents_OpponentId",
                schema: "xgb_dushbase",
                table: "Matches",
                column: "OpponentId",
                principalSchema: "xgb_dushbase",
                principalTable: "Opponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Opponents_OpponentId",
                schema: "xgb_dushbase",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Opponents",
                schema: "xgb_dushbase");

            migrationBuilder.DropIndex(
                name: "IX_Matches_OpponentId",
                schema: "xgb_dushbase",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Home",
                schema: "xgb_dushbase",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "OpponentId",
                schema: "xgb_dushbase",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "Opponent",
                schema: "xgb_dushbase",
                table: "Matches",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
