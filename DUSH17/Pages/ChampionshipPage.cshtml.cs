using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DUSH17.Pages
{
    public class ChampionshipPageModel : PageModel
    {
		private readonly ApplicationDbContext context;
		public ChampionshipPageModel(ApplicationDbContext db)
		{
			context = db;
		}
		public Competition Competittion { get; set; } = null!;
		public Championship Championship { get; set; } = null!;
		public List<OpponentList> opps { get; set; } = null!;
		public List<Opponent> opponents { get; set; } = null!;
		public List<OpponentsMatch> Oppsmatches { get; set; } = null!;
		public List<Participant> Participants { get; set; } = new();
		public List<TeamList> Teams { get; set; } = new();
		public List<Match> Matches { get; set; } = new();

		public void OnGet(int id)
        {
			Championship = context.Championships.Include(i=>i.Competition).First(i => i.CompetitionId == id);
			opps = context.OpponentList.Where(i => i.CompetitionId == id).Include(i=>i.Opponent).ThenInclude(i=>i.Picture).AsNoTracking().ToList();
			Oppsmatches = context.OpponentsMatches.Where(i=>i.CompetitionId == id).Include(i=>i.Opponent1).Include(i=>i.Opponent2).AsNoTracking().ToList();
			Teams = context.TeamList.Where(i => i.CompetitionId == id).Include(i => i.Team).ThenInclude(i=>i.Picture).AsNoTracking().ToList();
			Matches = context.Matches.Where(i=>i.CompetitionId == id).Include(i=>i.Team).Include(i=>i.Opponent).AsNoTracking().ToList();

			foreach (var opponent in opps)
			{
				Participants.Add(new Participant
				{
					Id = opponent.Opponent.Id,
					Name = opponent.Opponent.Name,
					Picture = opponent.Opponent.Picture,
					wins = Oppsmatches.Where(i => (i.Opponent1Id == opponent.Opponent.Id && i.Goals1 > i.Goals2) || (i.Opponent2Id == opponent.Opponent.Id && i.Goals2 > i.Goals1)).Count() +
					Matches.Where(i => i.OpponentId == opponent.Opponent.Id && i.Goals < i.OpponentGoals).Count(),
					draws = Oppsmatches.Where(i => (i.Opponent1Id == opponent.Opponent.Id || i.Opponent2Id == opponent.Opponent.Id) && (i.Goals1 == i.Goals2)).Count() +
					Matches.Where(i => i.OpponentId == opponent.Opponent.Id && i.Goals == i.OpponentGoals).Count(),
					loses = Oppsmatches.Where(i => (i.Opponent1Id == opponent.Opponent.Id && i.Goals1 < i.Goals2) || (i.Opponent2Id == opponent.Opponent.Id && i.Goals2 < i.Goals1)).Count()+
					Matches.Where(i => i.OpponentId == opponent.Opponent.Id && i.Goals > i.OpponentGoals).Count(),
					goalsScored = Oppsmatches.Where(i => i.Opponent1Id == opponent.Opponent.Id).Sum(i => i.Goals1) + Oppsmatches.Where(i => i.Opponent2Id == opponent.Opponent.Id).Sum(i => i.Goals2) +
					Matches.Where(i=>i.OpponentId == opponent.Opponent.Id ).Sum(i=>i.OpponentGoals),
					goalsConceded = Oppsmatches.Where(i => i.Opponent1Id == opponent.Opponent.Id).Sum(i => i.Goals2) + Oppsmatches.Where(i => i.Opponent2Id == opponent.Opponent.Id).Sum(i => i.Goals1) +
					Matches.Where(i => i.OpponentId == opponent.Opponent.Id).Sum(i => i.Goals),
					type = 1
				}) ;
			}
			foreach (var team in Teams)
			{
				Participants.Add(new Participant
				{
					Id = team.Team.Id,
					Name = "��-17",
					Picture = context.Pictures.Find(18),
					wins = Matches.Where(i => i.TeamId == team.TeamId && i.Goals > i.OpponentGoals).Count(),
					draws = Matches.Where(i => i.TeamId == team.TeamId && i.Goals == i.OpponentGoals).Count(),
					loses = Matches.Where(i => i.TeamId == team.TeamId && i.Goals < i.OpponentGoals).Count(),
					goalsScored = Matches.Where(i => i.TeamId == team.TeamId).Sum(i => i.Goals),
					goalsConceded = Matches.Where(i => i.TeamId == team.TeamId).Sum(i => i.OpponentGoals),
					type = 2
				}) ;
			}
			Participants = Participants.OrderByDescending(i => i.wins * 3 + i.draws).ThenByDescending(i => i.goalsScored - i.goalsConceded).ToList();
		}
    }
}
