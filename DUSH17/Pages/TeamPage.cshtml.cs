using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
	public class TeamPageModel : PageModel
	{
		ApplicationDbContext context;
		public List<Team> Teams { get; private set; } = new();
		public List<Footballer> Footballers { get; private set; } = new();
		public List<Coach> Coachess { get; private set; } = new();
		public List<Achievement> Achievments { get; private set; } = new();
		public List<Actionn> Actions { get; private set; } = new();
		public List<Match> Matches { get; private set; } = new();
		public List<Replace> Replaces { get; set; } = null!;
		public List<Protocol> Protocols { get; set; } = null!;
		public List<Lesson> Lessons { get; set; } = null!;
		public List<Visiting> Visits { get; set; } = null!;

		public TeamPageModel(ApplicationDbContext db)
		{
			context = db;
		}
		public void OnGet(int id)
		{
			Teams = context.Groups.Where(i=>i.Id == id).Include(c => c.Coach).Include(p => p.Picture).AsNoTracking().ToList();
			Coachess = context.Coaches.Include(p => p.Picture).Include(c=>c.Category).AsNoTracking().ToList();
			Footballers = context.Footballers.Where(i=>i.TeamId == id).Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();
			Achievments = context.achievements.Where(i => i.TeamId == id).Include(t => t.Competition).ThenInclude(l => l.CompetitionLevel).AsNoTracking().ToList();
			Actions = context.Actions.Include(i=>i.Footballer).Where(i=>i.Footballer.TeamId == id).Include(a=>a.ActionType).AsNoTracking().ToList();
			Matches = context.Matches.Where(i=>i.TeamId == id).Include(i => i.Opponent).ThenInclude(i=>i.Picture).Include(c => c.Competition).ThenInclude(l => l.CompetitionLevel).Include(t => t.Team)
	.Include(p => p.Protocols).ThenInclude(f => f.Footballer).OrderBy(i=>i.Date).ThenByDescending(i=>i.Id).AsNoTracking().ToList();
			Replaces = context.Replaces.AsNoTracking().ToList();
			Protocols = context.Protocols.AsNoTracking().ToList();
			Visits = context.Visits.AsNoTracking().ToList();
			Lessons = context.Lessons.AsNoTracking().ToList();
		}
        public async Task<IActionResult> OnPostDeleteAsync(int id, char letter,int teamId)
        {
			if (letter == 'f')
			{
				var user = await context.Footballers.FindAsync(id);

				if (user != null)
				{
					context.Footballers.Remove(user);
					await context.SaveChangesAsync();
				}
			}
			else if(letter == 'a')
			{
				var achievement = await context.achievements.FindAsync(id);

				if (achievement != null)
				{
					context.achievements.Remove(achievement);
					await context.SaveChangesAsync();
				}
			}


            return LocalRedirect("~/TeamPage/"+teamId);
		}

		public IActionResult OnPostAtend(int id)
		{
			return LocalRedirect("~/AttendanceLog?id=" + id);
		}
		public IActionResult OnPostDiagram(int id)
		{
			return LocalRedirect("~/SelectDiagram?id=" + id);
		}

	}
}
