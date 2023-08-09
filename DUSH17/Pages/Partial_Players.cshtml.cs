using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class Partial_PlayersModel : PageModel
    {
		ApplicationDbContext context;
		public List<Footballer> Footballers { get; set; } = null!;
		public List<Actionn> Actions { get; private set; } = new();

		// filter variable 

		public Partial_PlayersModel(ApplicationDbContext db)
		{
			context = db;
		}

		public void OnGet()
		{
			Footballers = context.Footballers.Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();
			Actions = context.Actions.Include(a => a.ActionType).AsNoTracking().ToList();

		}
	}
}
