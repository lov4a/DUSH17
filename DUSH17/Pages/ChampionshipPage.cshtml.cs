using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Cryptography;

namespace DUSH17.Pages
{
    public class ChampionshipPageModel : PageModel
    {
		private readonly ApplicationDbContext context;
		public ChampionshipPageModel(ApplicationDbContext db)
		{
			context = db;
		}
		public Competition? Competittion { get; set; } = null!;
		public List<Championship>? PreviousChamp {get; set; } = null!;
		public List <Championship> Championship { get; set; } = null!;
		public List<OpponentList> opps { get; set; } = null!;
		public List<Opponent> opponents { get; set; } = null!;
		public List<OpponentsMatch> Oppsmatches { get; set; } = null!;
		public List<Participant> Participants { get; set; } = new();
		public List<TeamList> Teams { get; set; } = new();
		public List<Match> Matches { get; set; } = new();
		public List<Actionn> Actionns {get; set; } = new();
		public List<OpponentsMatch> Play_off { get; set; } = new();
		public List<Match> Play_offD { get; set; } = new();
		public List<Footballer> Footballers { get; set; } = new();
		public List<Best> bests { get; set; } = new();

		public char[] Letters = { '0', 'A','С','Т','У','Ф','Х','Ц'};
		public void OnGet(int id)
        {
			Competittion = context.Competitions.Find(id);
			Championship = context.Championships.Where(i => i.CompetitionId == id).Include(i => i.Competition).AsNoTracking().ToList();
			PreviousChamp = context.Championships.Where(i => i.PreviousChampId == id).Include(i => i.Competition).AsNoTracking().ToList();
			Teams = context.TeamList.Where(i => i.CompetitionId == id && i.GroupId > 0).Include(i => i.Team).ThenInclude(i => i.Picture).AsNoTracking().ToList();
			opps = context.OpponentList.Where(i => i.CompetitionId == id).Include(i=>i.Opponent).ThenInclude(i=>i.Picture).AsNoTracking().ToList();
			Oppsmatches = context.OpponentsMatches.Where(i=>(i.CompetitionId == id || i.CompetitionId == Championship.First().PreviousChampId) && i.Stage == 0 )
				.Include(i=>i.Opponent1).Include(i=>i.Opponent2).AsNoTracking().ToList();
			Play_off = context.OpponentsMatches.Include(i => i.Opponent1).Include(i => i.Opponent2).Where(i => i.CompetitionId == id && i.Stage != 0).AsNoTracking().ToList();
			Matches = context.Matches.Where(i=>(i.CompetitionId == id || i.CompetitionId == Championship.First().PreviousChampId) && i.Stage == 0 ).Include(i=>i.Team).Include(i=>i.Opponent).AsNoTracking().ToList();
			Play_offD = context.Matches.Where(i=>i.CompetitionId == id && i.Stage != 0).Include(i=>i.Team).Include(i=>i.Opponent).AsNoTracking().ToList();
			Footballers = context.Footballers.Where(i => i.Id == 0).AsNoTracking().ToList();
			Actionns = context.Actions.Include(i => i.Match).Include(i => i.Footballer).Include(i=>i.ActionType).Where(i => i.Match.CompetitionId == Competittion.Id).AsNoTracking().ToList();
			var OpMatches = from opM in Oppsmatches
						  join op in opps on opM.Opponent1Id equals op.OpponentId
						  join op2 in opps on opM.Opponent2Id equals op2.OpponentId
						  select opM;
			var Matches2 = from opM in Matches
							join op in opps on opM.OpponentId equals op.OpponentId
							select opM;
			Oppsmatches = OpMatches.ToList();
			Matches = Matches2.ToList();

			foreach (var opponent in opps)
				{
					Participants.Add(new Participant
					{
						Id = opponent.Opponent.Id,
						Name = opponent.Opponent.Name,
						Picture = opponent.Opponent.Picture,
						wins = Oppsmatches.Where(i => (i.Opponent1Id == opponent.Opponent.Id && i.Goals1 > i.Goals2 && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent2Id))
						|| (i.Opponent2Id == opponent.Opponent.Id && i.Goals2 > i.Goals1 && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent1Id))).Count() +
						Matches.Where(i => i.OpponentId == opponent.Opponent.Id && i.Goals < i.OpponentGoals && Teams.Where(j => j.GroupId == opponent.GroupId).Any(o => o.TeamId == i.TeamId)).Count(),
						draws = Oppsmatches.Where(i => (i.Opponent1Id == opponent.Opponent.Id || i.Opponent2Id == opponent.Opponent.Id) && (i.Goals1 == i.Goals2) && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent2Id) &&
						opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent1Id)).Count() +
						Matches.Where(i => i.OpponentId == opponent.Opponent.Id && i.Goals == i.OpponentGoals && Teams.Where(j => j.GroupId == opponent.GroupId).Any(o => o.TeamId == i.TeamId)).Count(),
						loses = Oppsmatches.Where(i => (i.Opponent1Id == opponent.Opponent.Id && i.Goals1 < i.Goals2 && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent2Id))
						|| (i.Opponent2Id == opponent.Opponent.Id && i.Goals2 < i.Goals1 && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent1Id))).Count() +
						Matches.Where(i => i.OpponentId == opponent.Opponent.Id && i.Goals > i.OpponentGoals && Teams.Where(j => j.GroupId == opponent.GroupId).Any(o => o.TeamId == i.TeamId)).Count(),
						goalsScored = Oppsmatches.Where(i => i.Opponent1Id == opponent.Opponent.Id && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent2Id))
						.Sum(i => i.Goals1) + Oppsmatches.Where(i => i.Opponent2Id == opponent.Opponent.Id && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent1Id)).Sum(i => i.Goals2) +
						Matches.Where(i => i.OpponentId == opponent.Opponent.Id && Teams.Where(j => j.GroupId == opponent.GroupId).Any(o => o.TeamId == i.TeamId)).Sum(i => i.OpponentGoals),
						goalsConceded = Oppsmatches.Where(i => i.Opponent1Id == opponent.Opponent.Id && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent2Id))
						.Sum(i => i.Goals2) + Oppsmatches.Where(i => i.Opponent2Id == opponent.Opponent.Id && opps.Where(j => j.GroupId == opponent.GroupId).Any(o => o.OpponentId == i.Opponent1Id)).Sum(i => i.Goals1) +
						Matches.Where(i => i.OpponentId == opponent.Opponent.Id && Teams.Where(j => j.GroupId == opponent.GroupId).Any(o => o.TeamId == i.TeamId)).Sum(i => i.Goals),
						type = 1,
						group = opponent.GroupId
					}) ;
				}
				foreach (var team in Teams)
				{
					if (team.Name != null)
					{
						Participants.Add(new Participant
						{
							Id = team.Team.Id,
							Name = team.Name,
							Picture = context.Pictures.Find(18),
							wins = Matches.Where(i => i.TeamId == team.TeamId && i.Goals > i.OpponentGoals && opps.Where(j=>j.GroupId == team.GroupId).Any(o=>o.OpponentId == i.OpponentId)).Count(),
							draws = Matches.Where(i => i.TeamId == team.TeamId && i.Goals == i.OpponentGoals && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Count(),
							loses = Matches.Where(i => i.TeamId == team.TeamId && i.Goals < i.OpponentGoals && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Count(),
							goalsScored = Matches.Where(i => i.TeamId == team.TeamId && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Sum(i => i.Goals),
							goalsConceded = Matches.Where(i => i.TeamId == team.TeamId && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Sum(i => i.OpponentGoals),
							type = 2,
							group = team.GroupId
						});
					}
				else
				{
					Participants.Add(new Participant
					{
						Id = team.Team.Id,
						Name = "би-17",
						Picture = context.Pictures.Find(18),
						wins = Matches.Where(i => i.TeamId == team.TeamId && i.Goals > i.OpponentGoals && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Count(),
						draws = Matches.Where(i => i.TeamId == team.TeamId && i.Goals == i.OpponentGoals && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Count(),
						loses = Matches.Where(i => i.TeamId == team.TeamId && i.Goals < i.OpponentGoals && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Count(),
						goalsScored = Matches.Where(i => i.TeamId == team.TeamId && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Sum(i => i.Goals),
						goalsConceded = Matches.Where(i => i.TeamId == team.TeamId && opps.Where(j => j.GroupId == team.GroupId).Any(o => o.OpponentId == i.OpponentId)).Sum(i => i.OpponentGoals),
						type = 2,
						group = team.GroupId
					});
				}

				Footballers = Footballers.Concat(context.Footballers.Where(i => i.TeamId == team.TeamId)).ToList();
				}
				foreach(var f in Footballers)
				{
				bests.Add(new Best
				{
					footballer = f,
					goals = Actionns.Where(i=>i.ActionTypeId == 1 && i.FootballerId == f.Id).Count(),
					assists = Actionns.Where(i=>i.ActionTypeId == 2 && i.FootballerId == f.Id).Count(),
					games = Actionns.Where(i=>i.ActionTypeId == 6 && i.FootballerId == f.Id).Count()
				});
				}
			Participants = Participants.OrderBy(i=>i.group).ThenByDescending(i => i.wins * 3 + i.draws).ThenByDescending(i => i.goalsScored - i.goalsConceded).ToList();
			int i = 0;
			for (int group = 0; group < Participants.Max(i=> i.group);group++)
			{
				int points = Participants.Where(g=>g.group == group+1).Max(i=>i.wins*3+i.draws+1);
				foreach (var part in Participants.Where(g=>g.group == group+1 && g.wins*3 + g.draws < points).ToList())
				{

					if (points > part.wins*3 + part.draws) 
					{
						if (Participants.Exists(j => j.Id != part.Id && (j.wins * 3 + j.draws) == (part.wins * 3 + part.draws) && j.group == part.group && j.Id != part.Id))
						{
							

							List<Participant> h2h = new List<Participant>();
							foreach (var p in Participants.Where(j => (j.wins * 3 + j.draws) == (part.wins * 3 + part.draws) && j.group == part.group))
							{
									h2h.Add(new Participant
									{
										Id = p.Id,
										Name = p.Name,
										Picture = p.Picture,
										wins = 0,
										draws = 0,
										loses = 0,
										goalsScored = 0,
										goalsConceded = 0,
										type = p.type,
										group = p.group
									});
							}
							if (h2h.Count() > 0)
							{
								List<OpponentsMatch> OppsH2h = Oppsmatches.Where(x => h2h.Any(o => o.Id == x.Opponent1Id) && h2h.Any(o2 => o2.Id == x.Opponent2Id)).ToList();
								List<Match> MatchesH2h = Matches.Where(x => h2h.Any(o => o.Id == x.OpponentId)).ToList();
								foreach (var team in h2h)
								{
									if (team.type == 2)
									{
										team.wins = MatchesH2h.Where(i => i.TeamId == team.Id && i.Goals > i.OpponentGoals).Count();
										team.draws = MatchesH2h.Where(i => i.TeamId == team.Id && i.Goals == i.OpponentGoals).Count();
									}
									else
									{
										team.wins = OppsH2h.Where(i => (i.Opponent1Id == team.Id && i.Goals1 > i.Goals2) || (i.Opponent2Id == team.Id && i.Goals2 > i.Goals1)).Count() +
									Matches.Where(i => i.OpponentId == team.Id && i.Goals < i.OpponentGoals).Count();
										team.draws = Oppsmatches.Where(i => (i.Opponent1Id == team.Id || i.Opponent2Id == team.Id) && (i.Goals1 == i.Goals2)).Count() +
									Matches.Where(i => i.OpponentId == team.Id && i.Goals == i.OpponentGoals).Count();
									}

								}
								h2h = h2h.OrderByDescending(i => i.wins * 3 + i.draws).ThenByDescending(i => i.goalsScored - i.goalsConceded).ToList();

								var temp = Participants.ToList();
							    
								for (int index = 0; index < temp.Count; index++)
								{
									if (temp[index].Name == part.Name) {
										i = index;
									}
								}
								for (int j = 0; j < h2h.Count; j++)
								{

									Participants[i + j] = temp.Where(x => x.Name == h2h[j].Name).First();
								}
								points = Participants[i].wins * 3 + Participants[i].draws;
								h2h.Clear();
								OppsH2h.Clear();
								MatchesH2h.Clear();
							}

						}
					}

				}
			}

		}
		public Opponent winner (OpponentsMatch match)
		{
			if (match.Goals1 > match.Goals2)
			{
				return match.Opponent1;
			}
			else
			{
				return match.Opponent2;
			}
		}
		public Opponent loser(OpponentsMatch match)
		{
			if (match.Goals1 < match.Goals2)
			{
				return match.Opponent1;
			}
			else
			{
				return match.Opponent2;
			}
		}
	}
}
