using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DUSH17.Pages
{

	public class AttendanceLogModel : PageModel
    {
		ApplicationDbContext context;
		public AttendanceLogModel(ApplicationDbContext db)
		{
			context = db;
		}
		[BindProperty]
		public List<Footballer> Footballers { get; set; } = null!;
		public List<Lesson> Lessons { get; set; } = null!;
		public List<Visiting> Visits { get; set; } = null!;
		public Lesson? les { get; set; } = new();
		public List<Visiting> vis { get; set; } = new();
		public Team? team { get; set; } = null!;
		public DateOnly secondDate =DateOnly.FromDateTime(DateTime.Now);
		public DateOnly firstDate;


		public async Task<IActionResult> OnPostAsync(List<string>? types, string? date, int tId)
		{
			if (date != null)
			{
				les.Date = DateOnly.Parse(date);
				les.TeamId = tId;
				context.Lessons.Add(les);
				await context.SaveChangesAsync();
				Footballers = context.Footballers.Where(i => i.TeamId == les.TeamId).OrderBy(i => i.Surname).ThenBy(i => i.Name).ThenBy(i => i.Patronymic).AsNoTracking().ToList();
				for (int i = 0; i < Footballers.Count; i++)
				{
					if (types[i] != "ß")
					{
						vis.Add(new Visiting { FootballerId = Footballers[i].Id, LessonId = les.Id, Status = types[i] });
					}

				}
				context.Visits.AddRange(vis);
				await context.SaveChangesAsync();
			}



			return LocalRedirect("~/AttendanceLog?id=" + tId);
		}
		public void OnGet(int? id, string? fDate, string? sDate)
        {
			Visits = context.Visits.Include(i=>i.Footballer).Include(i=>i.Lesson).AsNoTracking().ToList();
			Lessons = context.Lessons.Where(i => i.TeamId == id).OrderBy(i => i.Date).AsNoTracking().ToList();
			Footballers = context.Footballers.Where(i => i.TeamId == id).OrderBy(i=>i.Surname).ThenBy(i=>i.Name).ThenBy(i=>i.Patronymic).AsNoTracking().ToList();
			team = context.Groups.Find(id);
			if (fDate != null)
			{
				firstDate = DateOnly.Parse(fDate);
			}
			else
			{
				if (Lessons.Count() > 0)
				{
					firstDate = Lessons.MinBy(i => i.Date).Date;
				}
				else
				{
					firstDate = DateOnly.FromDateTime(DateTime.Now);
				}
			}
			if (sDate != null)
			{
				secondDate = DateOnly.Parse(sDate);
			}
			else
			{
				secondDate = DateOnly.FromDateTime(DateTime.Now);
			}

		}

		public async Task<IActionResult> OnPostDeleteAsync(int lid)
		{
				var lesson = await context.Lessons.FindAsync(lid);

				if (lesson != null)
				{
					context.Lessons.Remove(lesson);
					await context.SaveChangesAsync();
				}
			


			return LocalRedirect("~/AttendanceLog?id=" + lesson.TeamId);
		}
	}
}
