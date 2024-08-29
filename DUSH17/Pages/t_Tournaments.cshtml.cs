using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class t_TournamentsModel : PageModel
    {
		ApplicationDbContext context;
		public t_TournamentsModel(ApplicationDbContext db)
		{
			context = db;
		}
		public List<t_Competition> Competitions { get; set; }
		public void OnGet()
        {
			Competitions = context.t_Competitions.OrderByDescending(i=>i.endDate).Include(i=>i.picture).AsNoTracking().ToList();
        }
    }
}
