using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
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
		public TeamPageModel(ApplicationDbContext db)
		{
			context = db;
		}
		public void OnGet()
		{
			Teams = context.Groups.Include(c => c.Coach).Include(p => p.Picture).AsNoTracking().ToList();
			Coachess = context.Coaches.Include(p => p.Picture).Include(c=>c.Category).AsNoTracking().ToList();
			Footballers = context.Footballers.Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();
			Achievments = context.Achievements.Include(t => t.Competition).ThenInclude(l => l.CompetitionLevel).AsNoTracking().ToList();

		}
	}
}
