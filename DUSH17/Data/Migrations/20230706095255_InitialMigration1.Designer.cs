﻿// <auto-generated />
using System;
using DUSH17.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DUSH17.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230706095255_InitialMigration1")]
    partial class InitialMigration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("xgb_dushbase")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DUSH17.Models.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompetitionId")
                        .HasColumnType("integer");

                    b.Property<int>("Place")
                        .HasColumnType("integer");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("TeamId");

                    b.ToTable("achievements", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.ActionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ActionTypes", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Actionn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("FootballerId")
                        .HasColumnType("integer");

                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActionTypeId");

                    b.HasIndex("FootballerId");

                    b.HasIndex("MatchId");

                    b.ToTable("Actions", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NameOfCategory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("DateOfBirthday")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PictureId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PictureId");

                    b.ToTable("Coaches", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CompetitionLevelId")
                        .HasColumnType("integer");

                    b.Property<int>("CountOfTeams")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("NameOfCompetition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionLevelId");

                    b.ToTable("Competitions", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.CompetitionLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CompetitionLevels", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Footballer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateOfBirthday")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DateOfStart")
                        .HasColumnType("date");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PictureId")
                        .HasColumnType("integer");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PictureId");

                    b.HasIndex("PositionId");

                    b.HasIndex("TeamId");

                    b.ToTable("Footballers", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.FootballerAchievement", b =>
                {
                    b.Property<int>("AchievementId")
                        .HasColumnType("integer");

                    b.Property<int>("FootballerId")
                        .HasColumnType("integer");

                    b.HasKey("AchievementId", "FootballerId");

                    b.HasIndex("FootballerId");

                    b.ToTable("FAs", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Lessons", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompetitionId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Goals")
                        .HasColumnType("integer");

                    b.Property<string>("Opponent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OpponentGoals")
                        .HasColumnType("integer");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("TeamId");

                    b.ToTable("Matches", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pictures", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NameOfPosition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Positions", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Protocol", b =>
                {
                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.Property<int>("FootballerId")
                        .HasColumnType("integer");

                    b.HasKey("MatchId", "FootballerId");

                    b.HasIndex("FootballerId");

                    b.ToTable("Protocols", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Replace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FootballerInId")
                        .HasColumnType("integer");

                    b.Property<int>("FootballerOutId")
                        .HasColumnType("integer");

                    b.Property<int>("MatchID")
                        .HasColumnType("integer");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FootballerInId");

                    b.HasIndex("FootballerOutId");

                    b.HasIndex("MatchID");

                    b.ToTable("Replaces", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CoachId")
                        .HasColumnType("integer");

                    b.Property<int>("PictureId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("PictureId");

                    b.ToTable("Groups", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.TeamList", b =>
                {
                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.Property<int>("CompetitionId")
                        .HasColumnType("integer");

                    b.HasKey("TeamId", "CompetitionId");

                    b.HasIndex("CompetitionId");

                    b.ToTable("TeamList", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Visiting", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<int>("FootballerId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LessonId", "FootballerId");

                    b.HasIndex("FootballerId");

                    b.ToTable("Visits", "xgb_dushbase");
                });

            modelBuilder.Entity("DUSH17.Models.Achievement", b =>
                {
                    b.HasOne("DUSH17.Models.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DUSH17.Models.Actionn", b =>
                {
                    b.HasOne("DUSH17.Models.ActionType", "ActionType")
                        .WithMany()
                        .HasForeignKey("ActionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Footballer", "Footballer")
                        .WithMany()
                        .HasForeignKey("FootballerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActionType");

                    b.Navigation("Footballer");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("DUSH17.Models.Coach", b =>
                {
                    b.HasOne("DUSH17.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("DUSH17.Models.Competition", b =>
                {
                    b.HasOne("DUSH17.Models.CompetitionLevel", "CompetitionLevel")
                        .WithMany()
                        .HasForeignKey("CompetitionLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompetitionLevel");
                });

            modelBuilder.Entity("DUSH17.Models.Footballer", b =>
                {
                    b.HasOne("DUSH17.Models.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Picture");

                    b.Navigation("Position");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DUSH17.Models.FootballerAchievement", b =>
                {
                    b.HasOne("DUSH17.Models.Achievement", "Achievement")
                        .WithMany("FootballerAchievements")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Footballer", "Footballer")
                        .WithMany("FootballerAchievements")
                        .HasForeignKey("FootballerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("Footballer");
                });

            modelBuilder.Entity("DUSH17.Models.Lesson", b =>
                {
                    b.HasOne("DUSH17.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DUSH17.Models.Match", b =>
                {
                    b.HasOne("DUSH17.Models.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DUSH17.Models.Protocol", b =>
                {
                    b.HasOne("DUSH17.Models.Footballer", "Footballer")
                        .WithMany("Protocols")
                        .HasForeignKey("FootballerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Match", "Match")
                        .WithMany("Protocols")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Footballer");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("DUSH17.Models.Replace", b =>
                {
                    b.HasOne("DUSH17.Models.Footballer", "FootballerIn")
                        .WithMany()
                        .HasForeignKey("FootballerInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Footballer", "FootballerOut")
                        .WithMany()
                        .HasForeignKey("FootballerOutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootballerIn");

                    b.Navigation("FootballerOut");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("DUSH17.Models.Team", b =>
                {
                    b.HasOne("DUSH17.Models.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("DUSH17.Models.TeamList", b =>
                {
                    b.HasOne("DUSH17.Models.Competition", "Competition")
                        .WithMany("TeamLists")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Team", "Team")
                        .WithMany("TeamLists")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DUSH17.Models.User", b =>
                {
                    b.HasOne("DUSH17.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DUSH17.Models.Visiting", b =>
                {
                    b.HasOne("DUSH17.Models.Footballer", "Footballer")
                        .WithMany("Visits")
                        .HasForeignKey("FootballerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUSH17.Models.Lesson", "Lesson")
                        .WithMany("Visits")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Footballer");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("DUSH17.Models.Achievement", b =>
                {
                    b.Navigation("FootballerAchievements");
                });

            modelBuilder.Entity("DUSH17.Models.Competition", b =>
                {
                    b.Navigation("TeamLists");
                });

            modelBuilder.Entity("DUSH17.Models.Footballer", b =>
                {
                    b.Navigation("FootballerAchievements");

                    b.Navigation("Protocols");

                    b.Navigation("Visits");
                });

            modelBuilder.Entity("DUSH17.Models.Lesson", b =>
                {
                    b.Navigation("Visits");
                });

            modelBuilder.Entity("DUSH17.Models.Match", b =>
                {
                    b.Navigation("Protocols");
                });

            modelBuilder.Entity("DUSH17.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DUSH17.Models.Team", b =>
                {
                    b.Navigation("TeamLists");
                });
#pragma warning restore 612, 618
        }
    }
}
