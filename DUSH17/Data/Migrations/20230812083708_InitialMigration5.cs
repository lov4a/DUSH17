using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpponentsMatches",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Opponent1Id = table.Column<int>(type: "integer", nullable: false),
                    Opponent2Id = table.Column<int>(type: "integer", nullable: false),
                    Goals1 = table.Column<int>(type: "integer", nullable: false),
                    Goals2 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpponentsMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpponentsMatches_Opponents_Opponent1Id",
                        column: x => x.Opponent1Id,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Opponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpponentsMatches_Opponents_Opponent2Id",
                        column: x => x.Opponent2Id,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Opponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpponentsMatches_Opponent1Id",
                schema: "xgb_dushbase",
                table: "OpponentsMatches",
                column: "Opponent1Id");

            migrationBuilder.CreateIndex(
                name: "IX_OpponentsMatches_Opponent2Id",
                schema: "xgb_dushbase",
                table: "OpponentsMatches",
                column: "Opponent2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpponentsMatches",
                schema: "xgb_dushbase");
        }
    }
}
