using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
	[IgnoreAntiforgeryToken]
	public class CreateMatchModel : PageModel
    {
		ApplicationDbContext context;
		public CreateMatchModel(ApplicationDbContext db)
		{
			context = db;
		}
		[BindProperty]
		public Match match { get; set; } = new();
		public SelectList Competitions { get; set; } = null!;
		public Competition? comp { get; set; } = null!;
		public List<Footballer> Footballers { get; private set; } = new();
		public List<Footballer> Footballers2 { get; private set; } = new();
		public List<Match> Matches { get; private set; } = new();
		public List<Protocol> Protocols { get; set; } = new List<Protocol>();
		public SelectList Opponents { get; set; } = null!;
		public List<OpponentList> OpponentLists { get; set; } = new();

		public int IdofTeam;
		public int CompId;
        public async Task<IActionResult> OnPostAsync(List<int>? PlayersChecked, string date)
		{
			Matches = context.Matches.Include(c=>c.Competition).Include(t=>t.Team).AsNoTracking().ToList();
			if (Matches.Count > 0)
			{
				match.Id = Matches.MaxBy(i => i.Id).Id + 1;
			}
			else
			{
				match.Id = 1;
			}

				match.Date = DateOnly.Parse(date);
			context.Matches.Add(match);
			await context.SaveChangesAsync();
			foreach (var player in PlayersChecked)
			{
				Protocols.Add(new Protocol { FootballerId = player, MatchId = match.Id });
			}
			context.Protocols.AddRange(Protocols);
			await context.SaveChangesAsync();


			return LocalRedirect("~/SetActions?mId=" + match.Id);
		}
		public void OnGet(int tId, int year,int cId)
		{
			comp = context.Competitions.Find(cId);
			OpponentLists = context.OpponentList.Include(i => i.Opponent).Where(i => i.CompetitionId == cId).AsNoTracking().ToList();
			Opponents = new SelectList((from s in OpponentLists
									  select new
									  {
										  ID = s.OpponentId,
										  OName = s.Opponent.Name
									  }), "ID", "OName", null);
            Matches = context.Matches.Include(c => c.Competition).Include(t => t.Team).AsNoTracking().ToList();
			Footballers2 = context.Footballers.Include(t => t.Team).Include(p => p.Position).Where(y => y.TeamId == tId).OrderBy(p => p.Surname).AsNoTracking().ToList();
			Footballers = context.Footballers.Include(t => t.Team).Include(p => p.Position).Where(y => y.Team.Year - year <= 3 && y.Team.Year - year > -2 && y.TeamId != tId).OrderBy(y => y.Team.Year).ThenBy(p => p.PositionId).AsNoTracking().ToList();
			IdofTeam = tId;
		}
    }
}
