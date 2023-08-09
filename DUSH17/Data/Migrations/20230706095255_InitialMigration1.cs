using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DUSH17.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "xgb_dushbase");

            migrationBuilder.CreateTable(
                name: "ActionTypes",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfCategory = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionLevels",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfPosition = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfCompetition = table.Column<string>(type: "text", nullable: false),
                    CompetitionLevelId = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CountOfTeams = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitions_CompetitionLevels_CompetitionLevelId",
                        column: x => x.CompetitionLevelId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "CompetitionLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    DateOfBirthday = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PictureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coaches_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoachId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    PictureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "achievements",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Place = table.Column<int>(type: "integer", nullable: false),
                    CompetitionId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_achievements_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_achievements_Groups_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Footballers",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    DateOfBirthday = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfStart = table.Column<DateOnly>(type: "date", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PictureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footballers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Footballers_Groups_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Footballers_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Footballers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Groups_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetitionId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    Opponent = table.Column<string>(type: "text", nullable: false),
                    Goals = table.Column<int>(type: "integer", nullable: false),
                    OpponentGoals = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Groups_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamList",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    CompetitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamList", x => new { x.TeamId, x.CompetitionId });
                    table.ForeignKey(
                        name: "FK_TeamList_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamList_Groups_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAs",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    FootballerId = table.Column<int>(type: "integer", nullable: false),
                    AchievementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAs", x => new { x.AchievementId, x.FootballerId });
                    table.ForeignKey(
                        name: "FK_FAs_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FAs_achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    FootballerId = table.Column<int>(type: "integer", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => new { x.LessonId, x.FootballerId });
                    table.ForeignKey(
                        name: "FK_Visits_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    FootballerId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false),
                    ActionTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "ActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actions_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actions_Matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Protocols",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    FootballerId = table.Column<int>(type: "integer", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocols", x => new { x.MatchId, x.FootballerId });
                    table.ForeignKey(
                        name: "FK_Protocols_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Protocols_Matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replaces",
                schema: "xgb_dushbase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatchID = table.Column<int>(type: "integer", nullable: false),
                    FootballerInId = table.Column<int>(type: "integer", nullable: false),
                    FootballerOutId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replaces_Footballers_FootballerInId",
                        column: x => x.FootballerInId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replaces_Footballers_FootballerOutId",
                        column: x => x.FootballerOutId,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replaces_Matches_MatchID",
                        column: x => x.MatchID,
                        principalSchema: "xgb_dushbase",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievements_CompetitionId",
                schema: "xgb_dushbase",
                table: "achievements",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_achievements_TeamId",
                schema: "xgb_dushbase",
                table: "achievements",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ActionTypeId",
                schema: "xgb_dushbase",
                table: "Actions",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_FootballerId",
                schema: "xgb_dushbase",
                table: "Actions",
                column: "FootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_MatchId",
                schema: "xgb_dushbase",
                table: "Actions",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_CategoryId",
                schema: "xgb_dushbase",
                table: "Coaches",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_PictureId",
                schema: "xgb_dushbase",
                table: "Coaches",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_CompetitionLevelId",
                schema: "xgb_dushbase",
                table: "Competitions",
                column: "CompetitionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_FAs_FootballerId",
                schema: "xgb_dushbase",
                table: "FAs",
                column: "FootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_PictureId",
                schema: "xgb_dushbase",
                table: "Footballers",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_PositionId",
                schema: "xgb_dushbase",
                table: "Footballers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_TeamId",
                schema: "xgb_dushbase",
                table: "Footballers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CoachId",
                schema: "xgb_dushbase",
                table: "Groups",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PictureId",
                schema: "xgb_dushbase",
                table: "Groups",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TeamId",
                schema: "xgb_dushbase",
                table: "Lessons",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionId",
                schema: "xgb_dushbase",
                table: "Matches",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId",
                schema: "xgb_dushbase",
                table: "Matches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_FootballerId",
                schema: "xgb_dushbase",
                table: "Protocols",
                column: "FootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_Replaces_FootballerInId",
                schema: "xgb_dushbase",
                table: "Replaces",
                column: "FootballerInId");

            migrationBuilder.CreateIndex(
                name: "IX_Replaces_FootballerOutId",
                schema: "xgb_dushbase",
                table: "Replaces",
                column: "FootballerOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Replaces_MatchID",
                schema: "xgb_dushbase",
                table: "Replaces",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamList_CompetitionId",
                schema: "xgb_dushbase",
                table: "TeamList",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "xgb_dushbase",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_FootballerId",
                schema: "xgb_dushbase",
                table: "Visits",
                column: "FootballerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "FAs",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Protocols",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Replaces",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "TeamList",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Visits",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "ActionTypes",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "achievements",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Footballers",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Lessons",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Competitions",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "CompetitionLevels",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Coaches",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "xgb_dushbase");

            migrationBuilder.DropTable(
                name: "Pictures",
                schema: "xgb_dushbase");
        }
    }
}
