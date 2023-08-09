using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class SelectCompetitionModel : PageModel
    {
		ApplicationDbContext context;
		public SelectCompetitionModel(ApplicationDbContext db)
		{
			context = db;
		}
		public List<Competition> Competitions { get; set; } = null!;
		public List<TeamList> Teams { get; set; } = null!;
		public Team? Team { get; set; } = null!;

		public int IdOfTeam;
		public void OnGet(int id)
        {
			IQueryable<Competition> CompIQ = from s in context.Competitions
												   select s;
			IQueryable<Competition> Comp2 = CompIQ.Where(s => s.CompetitionLevelId == 100);
			Teams = context.TeamList.Where(i=>i.TeamId== id).AsNoTracking().ToList();
			foreach (var c in Teams)
			{
				Comp2 = Comp2.Concat(CompIQ.Where(i => i.Id == c.CompetitionId));
			}
			Competitions = Comp2.AsNoTracking().OrderByDescending(i=>i.StartDate).ToList();
			IdOfTeam = id;
			Team = context.Groups.Find(id);
        }
    }
}
