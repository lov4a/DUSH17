using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
        [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        ApplicationDbContext context;
        [BindProperty]
        public Footballer? Person { get; set; }
        public List<Footballer> Footballers { get; private set; } = new();
        public SelectList Positions { get; set; } = null!;

        public EditModel(ApplicationDbContext db)
        {
            context = db;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Positions = new SelectList(context.Positions, nameof(Position.Id), nameof(Position.NameOfPosition));

            Footballers = context.Footballers.Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();
            Person = await context.Footballers.FindAsync(id);
			if (Person == null) return NotFound();
			foreach (var item in Positions)
            {
                if (item.Value == Convert.ToString(Person.PositionId))
                {
                    item.Selected = true;
                    break;
                }
            }

             
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string ds, string db)
        {
            Person.DateOfBirthday = DateOnly.Parse(db);
            Person.DateOfStart = DateOnly.Parse(ds);
            context.Footballers.Update(Person!);
            await context.SaveChangesAsync();
			return LocalRedirect("~/TeamPage/"+Person.TeamId);
		}
    }
}
