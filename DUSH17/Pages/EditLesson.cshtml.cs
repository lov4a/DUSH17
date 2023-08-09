using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace DUSH17.Pages
{
	[IgnoreAntiforgeryToken]
	public class EditLessonModel : PageModel
    {
		ApplicationDbContext context;
		public EditLessonModel(ApplicationDbContext db)
		{
			context = db;
		}
		[BindProperty]
		public Lesson? les { get; set; } = new();
		public List<Footballer> Footballers { get; set; } = null!;
		public List<Lesson> Lessons { get; set; } = null!;
		public List<Visiting> Visits { get; set; } = null!;

		public DateOnly secondDate = DateOnly.FromDateTime(DateTime.Now);
		public DateOnly firstDate;
		public async Task<IActionResult> OnGetAsync(int id)
        {
			les = await context.Lessons.FindAsync(id);
			Visits = context.Visits.Include(i => i.Footballer).Include(i => i.Lesson).AsNoTracking().ToList();
			Lessons = context.Lessons.Where(i => i.TeamId == id).OrderBy(i => i.Date).AsNoTracking().ToList();
			Footballers = context.Footballers.Where(i => i.TeamId == les.TeamId).OrderBy(i => i.Surname).ThenBy(i => i.Name).ThenBy(i => i.Patronymic).AsNoTracking().ToList();
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(List<string>? types,string? date)
		{

			Footballers = context.Footballers.Where(i => i.TeamId == les.TeamId).OrderBy(i => i.Surname).ThenBy(i => i.Name).ThenBy(i => i.Patronymic).AsNoTracking().ToList();
			Visits = context.Visits.Include(i => i.Footballer).Include(i => i.Lesson).AsNoTracking().ToList();
			Lessons = context.Lessons.Where(i => i.TeamId == les.TeamId).OrderBy(i => i.Date).AsNoTracking().ToList();
			if (date!= null)
			{
				les.Date = DateOnly.Parse(date);
			}
			context.Lessons.Update(les!);
			await context.SaveChangesAsync();
			for (int j = 0; j < Footballers.Count; j++)
			{
				if (types[j] != "ß" && Visits.Where(i => i.LessonId == les.Id && i.FootballerId == Footballers[j].Id).Count() == 0)
				{
					Visiting visit = new Visiting();
					visit.Status = types[j];
					visit.FootballerId = Footballers[j].Id;
					visit.LessonId = les.Id;
					context.Visits.Add(visit);
				}
				else if(Visits.Where(i=>i.LessonId == les.Id && i.FootballerId == Footballers[j].Id && i.Status != types[j]).Count() > 0)
				{
					var visit = context.Visits.Find(les.Id, Footballers[j].Id);
					if (types[j] == "ß")
					{
						
						if (visit !=null)
						{
							context.Visits.Remove(visit);
						}	
					}
					else
					{
						visit.Status = types[j];
						context.Visits.Update(visit!);
					}
				}
			}

			await context.SaveChangesAsync();
			return LocalRedirect("~/AttendanceLog?id=" + les.TeamId);
		}
	}
}
