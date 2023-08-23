using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class GroupTournamentModel : PageModel
    {
		private readonly ApplicationDbContext context;
		public GroupTournamentModel(ApplicationDbContext db)
		{
			context = db;
		}
		public List <Championship> Championship { get; set; } = new();
		public List<TeamList> TeamList { get; set; } = new();
		public List<OpponentList> OpponentList { get; set; } = new();
		public List<Participant> Participants { get; set; } = new();
		public List<OpponentsMatch> Oppsmatches { get; set; } = null!;
		public List<Match> Matches { get; set; } = new();
		public void OnGet(int id)
        {
			Championship = context.Championships.Where(i=>i.CompetitionId == id).AsNoTracking().ToList();
			TeamList = context.TeamList.Where(i=>i.CompetitionId==id).AsNoTracking().ToList();
			OpponentList = context.OpponentList.Where(i=>i.CompetitionId==id).Include(i => i.Opponent).ThenInclude(i => i.Picture).AsNoTracking().ToList();
			Oppsmatches = context.OpponentsMatches.Where(i => i.CompetitionId == id).Include(i => i.Opponent1).Include(i => i.Opponent2).AsNoTracking().ToList();
			Matches = context.Matches.Where(i => i.CompetitionId == id).Include(i => i.Team).Include(i => i.Opponent).AsNoTracking().ToList();
			Participants = Participants.OrderByDescending(i => i.wins * 3 + i.draws).ThenByDescending(i => i.goalsScored - i.goalsConceded).ToList();
		}
    }
}
