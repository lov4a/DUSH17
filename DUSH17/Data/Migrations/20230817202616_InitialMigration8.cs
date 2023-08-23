using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "GroupTournaments",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetitionId1 = table.Column<int>(type: "integer", nullable: false),
                    GroupsCount = table.Column<int>(type: "integer", nullable: false),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    Winers = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTournaments", x => x.CompetitionId);
                    table.ForeignKey(
                        name: "FK_GroupTournaments_Competitions_CompetitionId1",
                        column: x => x.CompetitionId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTournaments_CompetitionId1",
                schema: "xgb_dushbase",
                table: "GroupTournaments",
                column: "CompetitionId1");
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
