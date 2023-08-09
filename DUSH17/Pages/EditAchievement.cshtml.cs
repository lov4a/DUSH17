using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace DUSH17.Pages
{

    public class EditAchievementModel : PageModel
    {
        ApplicationDbContext context;
        [BindProperty]
        public Achievement? ach { get; set; } = new();
        public Footballer foot { get; set; } = new();
        public List<FootballerAchievement> Fach { get; set; } = new List<FootballerAchievement>();
        public List<Footballer> Footballers { get; private set; } = new();
        public List<Footballer> FootballersH { get; private set; } = new();
        public List<Footballer> FootballersDel { get; private set; } = new();
		public List<Achievement> Achievements { get; private set; } = new();
        public List<FootballerAchievement> FA { get; private set; } = new();
        public List<Position> PlayersList { get; private set; } = new();
        public string Message { get; private set; } = "";
        public SelectList Competitions { get; set; } = null!;
        public int IdofTeam;
        public EditAchievementModel(ApplicationDbContext db)
        {
            context = db;
        }
        public async Task<IActionResult> OnGetAsync(int tId, int year,int id)
        {
            ach = await context.achievements.FindAsync(id);
            Achievements = context.achievements.AsNoTracking().ToList();
            FA = context.FAs.Where(a => a.AchievementId == ach.Id).AsNoTracking().ToList();
			IQueryable<Footballer> footballersIQ = from s in context.Footballers
												   select s;
			IQueryable<Footballer> footballersIQ2 = footballersIQ.Where(s => s.PositionId == 100);

			IdofTeam = tId;
            Competitions = new SelectList(context.Competitions, nameof(Competition.Id), nameof(Competition.NameOfCompetition),ach.CompetitionId);
            
			foreach (var ids in FA)
			{
                footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == ids.FootballerId));
			}
            footballersIQ = footballersIQ.Except(footballersIQ2);
			
			FootballersH = footballersIQ2.Include(t => t.Team).Include(p => p.Position).Where(y => y.Team.Year >= year).OrderBy(y => y.Team.Year).ThenBy(p => p.PositionId).AsNoTracking().ToList();
			Footballers = footballersIQ.Include(t => t.Team).Include(p => p.Position).Where(y => y.Team.Year >= year).OrderBy(y => y.Team.Year).ThenBy(p => p.PositionId).AsNoTracking().ToList();
			if (ach == null) return NotFound();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(List<int>? PlayersChecked)
        {
			FA = context.FAs.Where(a => a.AchievementId == ach.Id).AsNoTracking().ToList();
			IQueryable<Footballer> footballersIQ = from s in context.Footballers
												   select s;
			IQueryable<Footballer> footballersIQ2 = footballersIQ.Where(s => s.PositionId == 100);
			IQueryable<Footballer> footballersIQ3 = footballersIQ.Where(s => s.PositionId == 100);
			foreach (var ids in FA)
			{
				footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == ids.FootballerId));
			}
            foreach (var foot in PlayersChecked)
            {
                footballersIQ3 = footballersIQ3.Concat(footballersIQ.Where(i => i.Id == foot));
			}
            footballersIQ = footballersIQ3.Except(footballersIQ2);
            footballersIQ2 = footballersIQ2.Except(footballersIQ3);
            FootballersDel = footballersIQ2.AsNoTracking().ToList();
			foreach (var save in footballersIQ)
            {
				Fach.Add(new FootballerAchievement { FootballerId = save.Id, AchievementId = ach.Id });
			}
			context.FAs.AddRange(Fach);
			foreach (var del in FootballersDel)
			{
				var f = context.FAs.Find(ach.Id, del.Id);
				if (f != null)
				{
					context.FAs.Remove(f);
				}
			}





			context.achievements.Update(ach!);
            await context.SaveChangesAsync();
            return LocalRedirect("~/TeamPage/" + ach.TeamId);
        }
    }
}
