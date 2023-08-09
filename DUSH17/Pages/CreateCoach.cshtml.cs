using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class CreateCoachModel : PageModel
    {
        ApplicationDbContext context;
        [BindProperty]
        public Coach Person { get; set; } = new();
        public List<Coach> Coaches { get; private set; } = new();
        public List<Category> CategoryList { get; private set; } = new();
        public SelectList Categories { get; set; } = null!;
        public CreateCoachModel(ApplicationDbContext db)
        {
            context = db;
        }

        public async Task<IActionResult> OnPostAsync(string db)
        {
            Coaches = context.Coaches.AsNoTracking().ToList();
            Person.Id = Coaches.MaxBy(i => i.Id).Id + 1;
            Person.PictureId = 4;
            Person.DateOfBirthday = DateOnly.Parse(db);
            context.Coaches.Add(Person);
            await context.SaveChangesAsync();
            return LocalRedirect("~/Teams/");
        }
        public void OnGet()
        {
            Categories = new SelectList(context.Categories, nameof(Category.Id), nameof(Category.NameOfCategory));
        }
    }
}
