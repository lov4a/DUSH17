using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class MatchModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public MatchModel(ApplicationDbContext db)
        {
            context = db;
        }
        public Match Match { get; set; } = null!;
        public List<Protocol> protocols { get; set; }
        public void OnGet(int id)
        {
            Match = context.Matches.Include(i => i.Team).Include(i => i.Opponent).ThenInclude(i=>i.Picture).Include(i => i.Competition).FirstOrDefault(i => i.Id == id);
            protocols = context.Protocols.Where(i=>i.MatchId == id).Include(i=>i.Footballer).ThenInclude(i=>i.Position).AsNoTracking().ToList();

        }
    }
}
