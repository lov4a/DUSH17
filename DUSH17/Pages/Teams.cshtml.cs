using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DUSH17.Pages
{

    public class TeamsModel : PageModel
    {
            ApplicationDbContext context;
            public List<Team> Teams { get; private set; } = new();

		public TeamsModel(ApplicationDbContext db)
            {
                context = db;
            }
            public void OnGet()
            {
             Teams = context.Groups.Include(c=> c.Coach).Include(p=> p.Picture).AsNoTracking().ToList();

		}

    }
}
