using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class MatchModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public MatchModel(ApplicationDbContext db)
        {
            context = db;
        }
        public Match Match { get; set; } = null!;
        public List<Protocol> protocols { get; set; } = null!;
        public List<Actionn> actionns { get; set; } = null!;
        public List<Replace> replaces { get; set; } = null!;
        public List<Footballer> squad { get; set; } = new();
        public List<Footballer> bench { get; set; } = new();
        public void OnGet(int id)
        {
            Match = context.Matches.Include(i => i.Team).Include(i => i.Opponent).ThenInclude(i=>i.Picture).Include(i => i.Competition).FirstOrDefault(i => i.Id == id);
            protocols = context.Protocols.Where(i=>i.MatchId == id).Include(i=>i.Footballer).ThenInclude(i=>i.Position).AsNoTracking().ToList();
            actionns = context.Actions.Where(i=>i.MatchId == id).AsNoTracking().ToList();
            replaces = context.Replaces.Where(i=>i.MatchID == id).Include(i=>i.FootballerIn).AsNoTracking().ToList();
            foreach (var person in protocols)
            {
                Footballer foot = context.Footballers.Include(i=>i.Position).First(i => i.Id == person.FootballerId);

				if (actionns.Exists(i=>i.FootballerId == person.FootballerId && i.ActionTypeId == 6 && i.Time == 0))
                {
                    if (foot != null)
                    {
						squad.Add(foot);
					}
                }
                else
                {
					if (foot != null)
					{
						bench.Add(foot);
					}
				}
            }

        }
    }
}
