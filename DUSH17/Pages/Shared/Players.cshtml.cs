using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class PlayersModel : PageModel
    {
        ApplicationDbContext context;
        public List<Footballer> Footballers { get; private set; } = null!;
        public PlayersModel(ApplicationDbContext db)
        {
            context = db;
        }
        public void OnGet()
        {
			Footballers = context.Footballers.Include(g=> g.Team).AsNoTracking().ToList();
		}
    }
}
