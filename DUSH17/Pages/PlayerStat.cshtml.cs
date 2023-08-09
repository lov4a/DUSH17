using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class PlayerStatModel : PageModel
    {
		private readonly ApplicationDbContext context;
		public PlayerStatModel(ApplicationDbContext db)
		{
			context = db;
		}
		public List<Footballer> Footballers { get; set; } = null!;
		public List<Footballer> FootballersCateg { get; set; } = null!;
		public Footballer? foot { get; set; } = null!;
		public List<Match> Matches { get; set; } = null!;
		public List<Actionn> Actions { get; private set; } = new();
		public List<Replace> Replaces { get; set; } = null!;
		public List<Protocol> Protocols { get; set; } = null!;
		public Team? team = new Team();
		public int amount = 10;
		public double x = 5.25;
		public string cat = "Среднее значение по команде";
		public SelectList Squad { get; set; } = null!;
		public void OnGet(int pId, int tId, int am, int? category)
        {
			amount = am;
			foot = context.Footballers.Find(pId);
			team = context.Groups.Find(tId);
			Footballers = context.Footballers.Where(i => i.TeamId == tId && i.Id != pId).Include(g => g.Team).Include(p => p.Position).OrderBy(p => p.PositionId).ThenBy(s => s.Surname).AsNoTracking().ToList();
			if (category != null)
			{
				if (category == -6)
				{
					Footballers = Footballers.Where(i => i.PositionId != 1).ToList();
					cat = "Среднее значение по полевым игрокам команды";
				}
				else if (category == -5)
				{
					Footballers = Footballers.Where(i => i.PositionId == 2 || i.PositionId == 3).ToList();
					cat = "Среднее значение по зашитникам и полузащитникам команды";
				}
				else if (category == -4)
				{
					Footballers = Footballers.Where(i => i.PositionId == 3 || i.PositionId == 4).ToList();
					cat = "Среднее значение по полузащитникам и нападающим команды";
				}
				else if (category == -3)
				{
					Footballers = Footballers.Where(i => i.PositionId == 1).ToList();
					cat = "Среднее значение по вратарям команды";
				}
				else if (category == -2)
				{
					Footballers = Footballers.Where(i => i.PositionId == 2).ToList();
					cat = "Среднее значение по защитникам команды";
				}
				else if (category == -1)
				{
					Footballers = Footballers.Where(i => i.PositionId == 3).ToList();
					cat = "Среднее значение по полузащитникам команды";
				}
				else if (category == 0)
				{
					Footballers = Footballers.Where(i => i.PositionId == 4).ToList();
					cat = "Среднее значение по нападающим команды";
				}
				else
				{
					Footballers = Footballers.Where(i => i.Id == category).ToList();
					cat = Footballers.First().Surname + " " + Footballers.First().Name;

				}

			}


			Matches = context.Matches.Where(i => i.TeamId == tId).Include(c => c.Competition).ThenInclude(l => l.CompetitionLevel).Include(t => t.Team)
			.Include(p => p.Protocols).ThenInclude(f => f.Footballer).OrderByDescending(i => i.Date).Take(amount).AsNoTracking().ToList();
			Replaces = context.Replaces.AsNoTracking().ToList();
			Protocols = context.Protocols.AsNoTracking().ToList();
			Actions = context.Actions.Include(a => a.ActionType).AsNoTracking().ToList();
			FootballersCateg = context.Footballers.Where(i => i.TeamId == tId && i.Id != pId).OrderBy(p => p.PositionId).ThenBy(s => s.Surname).AsNoTracking().ToList();
			List<Footballer> Category = FootballersCateg.ToList();

			Category.Insert(0, new Footballer { Id = -6, Name = "Полевые игроки", Surname = "" });
			Category.Insert(1, new Footballer { Id = -5, Name = "Защитники + Полузащитники", Surname = "" });
			Category.Insert(2, new Footballer { Id = -4, Name = "Полузащитники + Нападающие", Surname = "" });
			Category.Insert(3, new Footballer { Id = -3, Name = "Вратари", Surname = "" });
			Category.Insert(4, new Footballer { Id = -2, Name = "Защитники", Surname = "" });
			Category.Insert(5, new Footballer { Id = -1, Name = "Полузащитники", Surname = "" });
			Category.Insert(6, new Footballer { Id = 0, Name = "Нападающие", Surname = "" });
			Squad = new SelectList((from s in Category
									 select new
									 {
										 ID = s.Id,
										 FullName = s.Surname + " " + s.Name
									 }), "ID", "FullName", null);

		}
    }
}
