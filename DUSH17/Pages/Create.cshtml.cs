using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DUSH17.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        ApplicationDbContext context;
        [BindProperty]
        public Footballer Person { get; set; } = new();
        public List<Footballer> Footballers { get; private set; } = new();
        public List<Position> PositionList { get; private set; } = new();
        public SelectList Positions { get; set; } = null!;
        public int IdofTeam;
        public CreateModel(ApplicationDbContext db)
        {
            context = db;
        }

		public async Task<IActionResult> OnPostAsync(string db, string ds)
        {

			Footballers = context.Footballers.Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();
            Person.Id = Footballers.MaxBy(i => i.Id).Id + 1;
			Person.PictureId = 4;
            Person.DateOfBirthday = DateOnly.Parse(db);
            Person.DateOfStart = DateOnly.Parse(ds);
			context.Footballers.Add(Person);
            await context.SaveChangesAsync();
			return LocalRedirect("~/TeamPage/"+Person.TeamId);
		}
        public void OnGet(int tId)
        {
			IdofTeam = tId;
            Positions = new SelectList(context.Positions, nameof(Position.Id), nameof(Position.NameOfPosition));
            Footballers = context.Footballers.Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();

        }
    }
}
