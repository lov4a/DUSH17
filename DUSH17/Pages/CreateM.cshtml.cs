using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace DUSH17.Pages
{
    public class CreateMModel : PageModel
    {
        ApplicationDbContext context;
        [BindProperty]
        public Match Match { get; set; } = new();
        public List<Match> Matches { get; private set; } = new();
        public CreateMModel(ApplicationDbContext db)
        {
            context = db;
        }
        public int tid;
        public async Task<IActionResult> OnPostAsync(string db)
        {

            Matches = context.Matches.Include(c => c.Competition).Include(t => t.Team).AsNoTracking().ToList();
            Match.Date = DateOnly.Parse(db);
            context.Matches.Add(Match);
            await context.SaveChangesAsync();
            return LocalRedirect("~/TeamPage/" + Match.TeamId);
        }
        public void OnGet()
        {
            tid = 1;
        }
    }
}
