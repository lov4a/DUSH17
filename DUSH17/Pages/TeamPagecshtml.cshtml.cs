using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class TeamPagecshtmlModel : PageModel
    {
		ApplicationDbContext context;
		public List<Team> Teams { get; private set; } = new();
		public TeamPagecshtmlModel(ApplicationDbContext db)
		{
			context = db;
		}
		public void OnGet()
		{
			Teams = context.Groups.Include(c => c.Coach).Include(p => p.Picture).AsNoTracking().ToList();
		}
	}
}
