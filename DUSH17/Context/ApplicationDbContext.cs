using DUSH17.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DUSH17.Context
{
	public class ApplicationDbContext :DbContext
	{
		public DbSet<Footballer> Footballers { get; set; }
		public DbSet<Team> Groups { get; set; }
		public DbSet<Achievement> achievements { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Coach> Coaches { get; set; }
		public DbSet<Competition> Competitions { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<Visiting> Visits { get; set; }
		public DbSet<Actionn> Actions { get; set; }
		public DbSet<ActionType> ActionTypes { get; set; }
		public DbSet<Replace> Replaces { get; set; }
		public DbSet<CompetitionLevel> CompetitionLevels { get; set; }
		public DbSet<Picture> Pictures { get; set; }
		public DbSet<Protocol> Protocols { get; set; }
		public DbSet<FootballerAchievement> FAs { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<TeamList> TeamList { get; set; }
		public DbSet<Opponent> Opponents { get; set; }
		public DbSet<Championship> Championships { get; set; }
		public DbSet<OpponentList> OpponentList { get; set; }
		public DbSet<OpponentsMatch> OpponentsMatches { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.HasDefaultSchema("xgb_dushbase");
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Visiting>().HasKey(u => new { u.LessonId, u.FootballerId });
			modelBuilder.Entity<FootballerAchievement>()
			.HasKey(c => new { c.AchievementId, c.FootballerId });
			modelBuilder.Entity<Protocol>()
			.HasKey(k => new { k.MatchId, k.FootballerId });
			modelBuilder.Entity<TeamList>()
			.HasKey(t => new { t.TeamId, t.CompetitionId });
			modelBuilder.Entity<OpponentList>()
			.HasKey(t => new { t.OpponentId, t.CompetitionId });
		}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) 
		{ 
			
		}


	}

	public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseNpgsql(@"Server=postgres81.1gb.ru;Port=5432;Database=xgb_dushbase;User Id=xgb_dushbase;Password=VPEYXp-SD62V");

			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}
