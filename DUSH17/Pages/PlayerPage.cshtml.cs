using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class PlayerPageModel : PageModel
    {
		private readonly ApplicationDbContext context;
		public PlayerPageModel(ApplicationDbContext db)
		{
			context = db;
		}
		public List<Footballer> Footballers { get; set; } = null!;
		public List<Actionn> Actions { get; set; } = null!;
		public List<Match> Matches { get; set; } = null!;
		public List<Replace> Replaces { get; set; } = null!;
		public List<Protocol> Protocols { get; set; } = null!;
		public void OnGet(int id)
        {
			Footballers = context.Footballers.Where(i=>i.Id == id).Include(g => g.Team).ThenInclude(c=>c.Coach).Include(p => p.Position)
				.Include(i => i.Picture).Include(pos=>pos.Position).Include(pa=>pa.FootballerAchievements).ThenInclude(aa=>aa.Achievement)
				.ThenInclude(comp=>comp.Competition).ThenInclude(l=>l.CompetitionLevel).AsNoTracking().ToList();
			Actions = context.Actions.Where(i=>i.FootballerId == id).Include(a => a.ActionType).Include(m=>m.Match).ThenInclude(c=>c.Competition).ThenInclude(l=>l.CompetitionLevel).AsNoTracking().ToList();
			Matches = context.Matches.Include(c => c.Competition).ThenInclude(l => l.CompetitionLevel).Include(t => t.Team)
				.Include(p => p.Protocols).ThenInclude(f => f.Footballer).Include(i=>i.Opponent).ThenInclude(i=>i.Picture).AsNoTracking().ToList();
			Replaces = context.Replaces.Where(i=>i.FootballerInId == id || i.FootballerOutId == id).AsNoTracking().ToList();
			Protocols = context.Protocols.Where(i=>i.FootballerId == id).AsNoTracking().ToList();

		}
		public IActionResult OnPostStat(int pId, int tId, int am)
		{
			return LocalRedirect("~/PlayerStat?pId=" + pId + "&tId=" + tId + "&am=" + am);
		}
	}
}
