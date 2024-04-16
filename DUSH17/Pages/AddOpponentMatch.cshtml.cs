using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class AddOpponentMatchModel : PageModel
    {
		private readonly ApplicationDbContext context;
		public AddOpponentMatchModel(ApplicationDbContext db)
		{
			context = db;
		}
		[BindProperty]
		public OpponentsMatch match { get; set; } = new();
		public List<OpponentsMatch> matches { get; set; } = null!;
		public int HomeId { get; set; }
		public int AwayId { get; set; }
		public int CompId { get; set; }
		public Competition? Competition { get; set; } = new();
		public Opponent? Opponent1 { get; set; } = new();
		public Opponent? Opponent2 { get; set; } = new();
		public void OnGet(int home, int away, int comp)
        {
			Opponent1 = context.Opponents.Include(i => i.Picture).First(i=>i.Id == home);
			Opponent2 = context.Opponents.Include(i => i.Picture).First(i => i.Id == away);
			Competition = context.Competitions.Find(comp);
        }
		public async Task<IActionResult> OnPostAsync(string date)
		{
			matches = context.OpponentsMatches.AsNoTracking().ToList();
			if (matches.Count> 0)
			{
				match.Id = matches.MaxBy(i => i.Id).Id + 1;
			}
			else
			{
				match.Id = 1;
			}
			match.Date = DateOnly.Parse(date);
			context.OpponentsMatches.Add(match);
			await context.SaveChangesAsync();
			return LocalRedirect("~/ChampionshipPage?id=" + match.CompetitionId);
		}
	}
}
