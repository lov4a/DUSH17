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
        public List<TeamList> teams { get; set; } = null!;
        public void OnGet()
        {
            Competitions = context.Competitions.OrderByDescending(i=>i.StartDate).Include(i=>i.TeamLists).ThenInclude(i=>i.Team).AsNoTracking().ToList();
            teams = context.TeamList.Include(i=>i.Team).ThenInclude(i=>i.Coach).AsNoTracking().ToList();
        }
    }
}
