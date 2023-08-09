using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
	[IgnoreAntiforgeryToken]
	public class CreateCompetitionModel : PageModel
    {
		ApplicationDbContext context;
		[BindProperty]
		public Competition Comp { get; set; } = new();
		public List<Competition> Competitions { get; private set; } = new();
		public SelectList Levels { get; set; } = null!;
		public CreateCompetitionModel(ApplicationDbContext db)
		{
			context = db;
		}
		public async Task<IActionResult> OnPostAsync(string sd, string ed)
		{

			Competitions = context.Competitions.AsNoTracking().ToList();
			Comp.Id = Competitions.MaxBy(i => i.Id).Id + 1;
			Comp.StartDate = DateOnly.Parse(sd);
			Comp.EndDate = DateOnly.Parse(ed);
			context.Competitions.Add(Comp);
			await context.SaveChangesAsync();
			return RedirectToPage("Teams");
		}
		public void OnGet()
		{
			Levels = new SelectList(context.CompetitionLevels, nameof(CompetitionLevel.Id), nameof(CompetitionLevel.Level));
			Competitions = context.Competitions.AsNoTracking().ToList();

		}
	}
}
