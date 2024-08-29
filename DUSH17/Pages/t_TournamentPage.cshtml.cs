using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class t_TournamentPageModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public t_TournamentPageModel(ApplicationDbContext db)
        {
            context = db;
        }
        public t_Competition? Competition { get; set; }
        public List<t_Team> T { get; set; }
        public List<t_TeamList> Teams { get; set; }
		public List<Participant> Participants { get; set; } = new();
		public List<t_Match> Matches { get; set; }

		public void OnGet(int id)
        {
            Competition = context.t_Competitions.Find(id);
            Teams = context.t_TeamList.Where(i => i.competitionId == id).Include(i=>i.team).ThenInclude(i=>i.Picture).AsNoTracking().ToList();
			Matches = context.t_Matches.Where(i => i.competitionId == id).AsNoTracking().ToList();

			foreach (var opponent in Teams)
			{
				Participants.Add(new Participant
				{
					Id = opponent.team.id,
					Name = opponent.team.name,
					Picture = opponent.team.Picture,
					wins = Matches.Where(i => (i.homeId == opponent.team.id && i.h_goals > i.v_goals && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.visitorId))
					|| (i.visitorId == opponent.team.id && i.v_goals > i.h_goals && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.homeId))).Count(),
					draws = Matches.Where(i => (i.homeId == opponent.team.id || i.visitorId == opponent.team.id) && (i.h_goals == i.v_goals && i.h_goals != null) && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.visitorId) &&
					Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.homeId)).Count(),
					loses = Matches.Where(i => (i.homeId == opponent.team.id && i.h_goals < i.v_goals && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.visitorId))
					|| (i.visitorId == opponent.team.id && i.v_goals < i.h_goals && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.homeId))).Count(),
					goalsScored = Matches.Where(i => i.homeId == opponent.team.id && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.visitorId))
					.Sum(i => i.h_goals) + Matches.Where(i => i.visitorId == opponent.team.id && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.homeId)).Sum(i => i.v_goals),
					goalsConceded = Matches.Where(i => i.homeId == opponent.team.id && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.visitorId))
					.Sum(i => i.v_goals) + Matches.Where(i => i.visitorId == opponent.team.id && Teams.Where(j => j.groupId == opponent.groupId).Any(o => o.teamId == i.homeId)).Sum(i => i.h_goals),
					type = 1,
					group = opponent.groupId
				});
			}
			Participants = Participants.OrderBy(i => i.group).ThenByDescending(i => i.wins * 3 + i.draws).ThenByDescending(i => i.goalsScored - i.goalsConceded).ThenBy(i => i.Name).ToList();
			int i = 0;
			for (int group = 0; group < Participants.Max(i => i.group); group++)
			{
				int points = Participants.Where(g => g.group == group + 1).Max(i => i.wins * 3 + i.draws + 1);
				foreach (var part in Participants.Where(g => g.group == group + 1 && g.wins * 3 + g.draws < points).ToList())
				{

					if (points > part.wins * 3 + part.draws)
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
							List<t_Match> OppsH2h = Matches.Where(x => h2h.Any(o => o.Id == x.homeId) && h2h.Any(o2 => o2.Id == x.visitorId)).ToList();
							if (h2h.Count() > 0 && OppsH2h.Count() > 0)
							{

								foreach (var team in h2h)
								{

									team.wins = OppsH2h.Where(i => (i.homeId == team.Id && i.h_goals > i.v_goals) || (i.visitorId == team.Id && i.v_goals > i.h_goals)).Count();
									team.draws = OppsH2h.Where(i => (i.homeId == team.Id || i.visitorId == team.Id) && (i.h_goals == i.v_goals && i.h_goals != null)).Count();
									team.goalsScored = OppsH2h.Where(i => i.homeId == team.Id).Sum(i => i.h_goals) + OppsH2h.Where(i => i.visitorId == team.Id).Sum(i => i.v_goals);
									team.goalsConceded = OppsH2h.Where(i => i.homeId == team.Id).Sum(i => i.v_goals) + OppsH2h.Where(i => i.visitorId == team.Id).Sum(i => i.h_goals);
									

								}
								h2h = h2h.OrderByDescending(i => i.wins * 3 + i.draws).ThenByDescending(i => i.goalsScored - i.goalsConceded).ThenByDescending(i=>i.goalsScored).ThenBy(i => i.Name).ToList();

								var temp = Participants.ToList();

								for (int index = 0; index < temp.Count; index++)
								{
									if (temp[index].Name == part.Name)
									{
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
							}

						}
					}

				}
			}
		}
		public Opponent winner(OpponentsMatch match)
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
