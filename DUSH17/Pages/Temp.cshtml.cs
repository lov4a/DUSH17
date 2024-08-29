using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class TempModel : PageModel
    {
        ApplicationDbContext context;
        public TempModel(ApplicationDbContext db)
        {
            context = db;
        }
        public List<t_Competition> competitions { get; private set; } = new();
        public List<t_Match> matches { get; private set; } = new();
        public List<DateOnly> dates { get; private set; } = new();
        public void OnGet()
        {
            competitions = context.t_Competitions.AsNoTracking().ToList();
            matches = context.t_Matches.Where(t=>t.date > DateOnly.FromDateTime(DateTime.Now.AddDays(-6))).Include(t => t.home).ThenInclude(t=>t.Picture).Include(t=>t.visitor).ThenInclude(t=>t.Picture).Include(t=>t.competition).OrderBy(t=>t.date).ThenBy(t=>t.time).AsNoTracking().ToList();
            dates = matches.Select(x => x.date).Distinct().ToList();
		}
    }
}
