using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;


namespace DUSH17.Pages
{
    public class PlayersModel : PageModel
    {
        private readonly ApplicationDbContext context;
		public PlayersModel(ApplicationDbContext db)
		{
			context = db;
		}



		public List<Footballer> Footballers { get; set; } = null!;
		public List<Actionn> Actions { get; private set; } = new();
		public List<Footballer> foot { get; private set; } = new();

		public SelectList Positions { get; set; } = null!;
		public SelectList Teams { get; set; } = null!;
		public List<Match> Matches { get; set; } = null!;
		public List<Replace> Replaces { get; set; } = null!;
		public List<Protocol> Protocols { get; set; } = null!;
		public List<Stat> stats { get; set; } = new();
		public List<Position> PositionList { get; private set; } = new();
		public List<Team> TeamList { get; private set; } = new();
		public int CurrentPos { get; set; } = 1;
		public int[] MinMaxForPlaceHolder { get; set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
		public List<int> checkedPos = new();
		public List<int> checkedTeams = new();
		public List<int> setGoals = new();
		public List<int> setAssists = new();
		public List<int> setGames = new();
		public List<double> setMinutes = new();
		public List<double> setg90 = new();
		public List<double> seta90 = new();
		public List<double> setGP = new();
		public List<double> setY = new();
		public List<double> setR = new();
		public List<string> dates = new();



		public void OnGet( int[]? games, int[]? minutes, int[]? assists,double[]? gOn90, double[]? aOn90, int[]? gp,int[]? yCards, int[]? rCards, List<int>? PosChecked, List<int>? TeamChecked, int[]? goals, string? fDate, string? sDate)
		{
			Positions = new SelectList(context.Positions, nameof(Position.Id), nameof(Position.NameOfPosition));
			Teams = new SelectList(context.Groups, nameof(Team.Id), nameof(Team.Year));
			PositionList = context.Positions.AsNoTracking().ToList();
			TeamList = context.Groups.OrderBy(i=>i.Year).Include(c => c.Coach).AsNoTracking().ToList();
			

			if (fDate != null && sDate!= null)
			{
				Actions = context.Actions.Include(a => a.ActionType).Where(i => i.Match.Date >= DateOnly.Parse(fDate) && i.Match.Date <= DateOnly.Parse(sDate)).AsNoTracking().ToList();
				Matches = context.Matches.Where(i => i.Date >= DateOnly.Parse(fDate) && i.Date <= DateOnly.Parse(sDate)).Include(c => c.Competition).ThenInclude(l => l.CompetitionLevel).Include(t => t.Team)
				.Include(p => p.Protocols).ThenInclude(f => f.Footballer).AsNoTracking().ToList();
				Protocols = context.Protocols.Where(i => i.Match.Date >= DateOnly.Parse(fDate) && i.Match.Date <= DateOnly.Parse(sDate)).AsNoTracking().ToList();
				dates.Clear();
				dates.Add(fDate);
				dates.Add(sDate);
			}
			else
			{
				Actions = context.Actions.Include(a => a.ActionType).Include(i => i.Match).AsNoTracking().ToList();
				Matches = context.Matches.Include(c => c.Competition).ThenInclude(l => l.CompetitionLevel).Include(t => t.Team)
				.Include(p => p.Protocols).ThenInclude(f => f.Footballer).AsNoTracking().ToList();
				Protocols = context.Protocols.AsNoTracking().ToList();
			}


			Replaces = context.Replaces.AsNoTracking().ToList();

			minMaxValue(Actions);

			IQueryable<Footballer> footballersIQ = from s in context.Footballers
												   select s;
			IQueryable<Footballer> footballersIQ2 = footballersIQ.Take(0);
			 
			if (PosChecked.Count > 0 && !PosChecked.Contains(0))
			{
				checkedPos.Clear();
				foreach (var pp in PosChecked)
				{
					checkedPos.Add(pp);
					footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(s => s.PositionId == pp));
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}
			if (TeamChecked.Count > 0 && !TeamChecked.Contains(-1))
			{
				checkedTeams.Clear();
				foreach (var t in TeamChecked)
				{
					checkedTeams.Add(t);
					footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(s => s.TeamId == t));
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}

			if (games.Length > 0)
			{
				setGames.Clear();
				setGames.Add(games[0]); 
				setGames.Add(games[1]);
				int gm = 0;
				foreach (var foot in footballersIQ)
				{
					gm = Protocols.Where(g => g.FootballerId == foot.Id).Count();
					if (gm >= games[0] && gm <= games[1])
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}

				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}

			if (minutes.Length > 0)
			{
				setMinutes.Clear();
				setMinutes.Add(minutes[0]);
				setMinutes.Add(minutes[1]);
				double min = 0;
				foreach(var foot in footballersIQ)
				{
					min = stats.FirstOrDefault(i => i.Id == foot.Id).minutes;
					if  (min >= minutes[0] && min <= minutes[1])
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}

				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}
			if (goals.Length > 0)
			{
				setGoals.Clear();
				setGoals.Add(goals[0]);
				setGoals.Add(goals[1]);
				int g = 0;
				foreach (var foot in footballersIQ)
				{
					g = Actions.Where(g => g.FootballerId == foot.Id && g.ActionTypeId == 1).Count();
					if (goals[0] <= g && goals[1] >= g)
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}

			if (assists.Length > 0)
			{
				setAssists.Clear();
				setAssists.Add(assists[0]);
				setAssists.Add(assists[1]);
				int a = 0;
				foreach (var foot in footballersIQ)
				{
					a = Actions.Where(g => g.FootballerId == foot.Id && g.ActionTypeId == 2).Count();
					if (assists[0] <= a && assists[1] >= a)
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}

			if (gOn90.Length > 0)
			{
				setg90.Clear();
				setg90.Add(gOn90[0]);
				setg90.Add(gOn90[1]);
				double x = 0;
				foreach (var foot in footballersIQ)
				{
					x = stats.FirstOrDefault(i => i.Id == foot.Id).g90;
					if (gOn90[0] <= x && gOn90[1] >= x)
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}
			if (aOn90.Length > 0)
			{
				seta90.Clear();
				seta90.Add(aOn90[0]);
				seta90.Add(aOn90[1]);
				double x = 0;
				foreach (var foot in footballersIQ)
				{
					x = stats.FirstOrDefault(i => i.Id == foot.Id).a90;
					if (aOn90[0] <= x && aOn90[1] >= x)
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}
			if (gp.Length > 0)
			{
				setGP.Clear();
				setGP.Add(gp[0]);
				setGP.Add(gp[1]);
				double x = 0;
				foreach (var foot in footballersIQ)
				{
					x = stats.FirstOrDefault(i => i.Id == foot.Id).gPlusA;
					if (gp[0] <= x && gp[1] >= x)
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}
			if (yCards.Length > 0)
			{
				setY.Clear();
				setY.Add(yCards[0]);
				setY.Add(yCards[1]);
				int g = 0;
				foreach (var foot in footballersIQ)
				{
					g = Actions.Where(g => g.FootballerId == foot.Id && g.ActionTypeId == 3).Count();
					if (yCards[0] <= g && yCards[1] >= g)
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}
			if (rCards.Length > 0)
			{
				setR.Clear();
				setR.Add(rCards[0]);
				setR.Add(rCards[1]);
				int g = 0;
				foreach (var foot in footballersIQ)
				{
					g = Actions.Where(g => g.FootballerId == foot.Id && g.ActionTypeId == 4).Count();
					if (rCards[0] <= g && rCards[1] >= g)
					{
						footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.Id));
					}
				}
				footballersIQ = footballersIQ2;
				footballersIQ2 = footballersIQ.Take(0);
			}
			Footballers = footballersIQ.Include(g => g.Team).Include(p => p.Position).OrderBy(p => p.Team.Year).ThenBy(p=>p.PositionId).ThenBy(s => s.Surname).AsNoTracking().ToList();
			getStats();


		}
		public void getStats()
		{
			foreach (var person in Footballers)
			{
				double totalMin = 0;
				foreach (var match in Matches.Where(f => f.TeamId == person.TeamId))
				{
					if (match.Protocols.Where(pr => pr.FootballerId == person.Id).Count() > 0)
					{
						if (Actions.Where(a => a.ActionTypeId == 6 && a.MatchId == match.Id && a.FootballerId == person.Id).Count() > 0)
						{
							int inMin = 0;
							int outMin = 90;
							foreach (var rep in Replaces.Where(fb => fb.MatchID == match.Id && (fb.FootballerInId == person.Id || fb.FootballerOutId == person.Id)))
							{
								if (rep.FootballerOutId == person.Id)
								{
									outMin = rep.Time;
								}
								if (rep.FootballerInId == person.Id)
								{
									inMin = rep.Time;
								}
							}
							totalMin += outMin - inMin;
						}


					}
				}

				stats.Add(
					new Stat {
						Id = person.Id,
						minutes = totalMin,
						g90 = Math.Round(Actions.Where(i=>i.FootballerId == person.Id && i.ActionTypeId == 1).Count() / (totalMin/90),2),
						a90 = Math.Round(Actions.Where(i=>i.FootballerId == person.Id && i.ActionTypeId == 2).Count() / (totalMin/90),2),
						gPlusA = Actions.Where(i => i.FootballerId == person.Id && ( i.ActionTypeId == 1 || i.ActionTypeId == 2)).Count()
					}
					) ;

			}
		}
		public void minMaxValue (List<Actionn> Actions)
		{
			foreach (var person in context.Footballers)
			{
				//игры
				if (MinMaxForPlaceHolder[0] < Protocols.Where(g => g.FootballerId == person.Id).Count())
				{
					MinMaxForPlaceHolder[0] = Protocols.Where(g => g.FootballerId == person.Id).Count();
				}
				if (MinMaxForPlaceHolder[1] > Protocols.Where(g => g.FootballerId == person.Id).Count())
				{
					MinMaxForPlaceHolder[1] = Protocols.Where(g => g.FootballerId == person.Id).Count();
				}
				//голы
				if (MinMaxForPlaceHolder[2] < Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 1).Count())
				{
					MinMaxForPlaceHolder[2] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 1).Count();
				}
				if (MinMaxForPlaceHolder[3] > Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 1).Count())
				{
					MinMaxForPlaceHolder[3] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 1).Count();
				}
				//голквые передачи
				if (MinMaxForPlaceHolder[4] < Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 2).Count())
				{
					MinMaxForPlaceHolder[4] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 2).Count();
				}
				if (MinMaxForPlaceHolder[5] > Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 2).Count())
				{
					MinMaxForPlaceHolder[5] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 2).Count();
				}
				//желтые карточки
				if (MinMaxForPlaceHolder[6] < Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 3).Count())
				{
					MinMaxForPlaceHolder[6] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 3).Count();
				}
				if (MinMaxForPlaceHolder[7] > Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 3).Count())
				{
					MinMaxForPlaceHolder[7] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 3).Count();
				}
				//красные карточки
				if (MinMaxForPlaceHolder[8] < Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 4).Count())
				{
					MinMaxForPlaceHolder[8] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 4).Count();
				}
				if (MinMaxForPlaceHolder[9] > Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 4).Count())
				{
					MinMaxForPlaceHolder[9] = Actions.Where(g => g.FootballerId == person.Id && g.ActionTypeId == 4).Count();
				}
			}	
		}
	}
}
