using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

namespace DUSH17.Pages
{
    public class CreateAchievementModel : PageModel
    {
		ApplicationDbContext context;
		[BindProperty]
		public Achievement ach { get; set; } = new();
		public Footballer foot { get; set; } = new();
		public List <FootballerAchievement> Fach { get; set; } = new List<FootballerAchievement>();
		public List<Footballer> Footballers { get; private set; } = new();
		public List<Footballer> Footballers2 { get; private set; } = new();
		public List<Achievement> Achievements { get; private set; } = new();
		public List<FootballerAchievement> FA { get; private set; } = new();
		public List<Position> PlayersList { get; private set; } = new();
		public string Message { get; private set; } = "";
		public SelectList Competitions { get; set; } = null!;
		public int IdofTeam;
		public CreateAchievementModel(ApplicationDbContext db)
		{
			context = db;
		}
		public async Task<IActionResult> OnPostAsync(List<int>? PlayersChecked)
		{
			FA = context.FAs.AsNoTracking().ToList();	
			Footballers = context.Footballers.Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();
			Achievements = context.achievements.AsNoTracking().ToList();
			ach.Id = Achievements.MaxBy(i=>i.Id).Id + 1;
			context.achievements.Add(ach);
			await context.SaveChangesAsync();
			if (PlayersChecked.Count > 0)
			{
				for (int i = 0; i < PlayersChecked.Count(); i++)
				{
					Fach.Add(new FootballerAchievement {  FootballerId = PlayersChecked[i], AchievementId = ach.Id });

				}
				context.FAs.AddRange(Fach);
			}
			context.SaveChanges();


			return LocalRedirect("~/TeamPage/" + ach.TeamId);
		}
		public void OnGet(int tId, int year)
		{
			Achievements = context.achievements.AsNoTracking().ToList();
			IdofTeam = tId;
			Competitions = new SelectList(context.Competitions, nameof(Competition.Id), nameof(Competition.NameOfCompetition));
			Footballers2 = context.Footballers.Include(t => t.Team).Include(p => p.Position).Where(y => y.TeamId == tId).OrderBy(p => p.PositionId).AsNoTracking().ToList();
			Footballers = context.Footballers.Include(t => t.Team).Include(p => p.Position).Where(y => y.Team.Year - year <= 3 && y.Team.Year - year > -2 && y.TeamId != tId).OrderBy(y => y.Team.Year).ThenBy(p => p.PositionId).AsNoTracking().ToList();


		}
	}
}
