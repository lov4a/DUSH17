using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class TournamentsModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public TournamentsModel(ApplicationDbContext db)
        {
            context = db;
        }
        public List<Competition> Competitions { get; set; } = null!;
        public List<Championship> Championships { get; set; } = null!;
		public List<TeamList> teams { get; set; } = null!;
        public List<Picture> picture { get; set; }
        public void OnGet()
        {
            picture = context.Pictures.ToList();

			Competitions = context.Competitions.OrderByDescending(i => i.EndDate).Include(i => i.TeamLists).ThenInclude(i => i.Team).Include(i => i.Picture).AsNoTracking().ToList();
			Championships = context.Championships.Where(i=>i.PreviousChampId == null).OrderByDescending(i => i.Competition.EndDate).
                Include(i=>i.Competition).ThenInclude(i => i.Picture).Include(i=>i.Competition).ThenInclude(i=>i.TeamLists).ThenInclude(i=>i.Team).AsNoTracking().ToList();
			teams = context.TeamList.Include(i => i.Team).ThenInclude(i => i.Coach).AsNoTracking().ToList();
        }
    }
}
