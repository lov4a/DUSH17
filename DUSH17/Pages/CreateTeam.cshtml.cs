using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class CreateTeamModel : PageModel
    {
		ApplicationDbContext context;
		[BindProperty]
		public Team Group { get; set; } = new();
		public List<Team> Teams { get; private set; } = new();
		public List<Coach> CoachList { get; private set; } = new();
		public SelectList Coaches { get; set; } = null!;
		public CreateTeamModel(ApplicationDbContext db)
		{
			context = db;
		}

		public async Task<IActionResult> OnPostAsync(string db)
		{
			Teams = context.Groups.AsNoTracking().ToList();
			Group.Id = Teams.MaxBy(i => i.Id).Id + 1;
			Group.PictureId = 2;
			context.Groups.Add(Group);
			await context.SaveChangesAsync();
			return LocalRedirect("~/Teams/");
		}
		public void OnGet()
		{
			CoachList = context.Coaches.AsNoTracking().ToList();
			List<Coach> coach = CoachList.ToList();
			Coaches = new SelectList((from s in coach
								   select new
								   {
									   ID = s.Id,
									   FName = s.Surname + " " + s.Name + " " + s.Patronymic
								   }), "ID", "FName", null);
		}
	}
}
