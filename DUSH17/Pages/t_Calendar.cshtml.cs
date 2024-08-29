using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
	public class t_CalendarModel : PageModel
	{
		ApplicationDbContext context;
		public t_CalendarModel(ApplicationDbContext db)
		{
			context = db;
		}
		public t_Competition? Competition { get; set; } = null!;
		public List<t_Match> Matches { get; set; }
		public void OnGet(int id)
		{
			Matches = context.t_Matches.Where(i => i.competitionId == id).Include(i => i.visitor).ThenInclude(i => i.Picture).Include(i => i.home).ThenInclude(i => i.Picture).OrderBy(i=>i.date).ThenBy(i=>i.time).AsNoTracking().ToList();
			Competition = context.t_Competitions.Find(id);
		}
	}
}
