using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DUSH17.Pages
{
    public class SetActionsModel : PageModel
    {
		ApplicationDbContext context;
		public SetActionsModel(ApplicationDbContext db)
		{
			context = db;
		}
		[BindProperty]
		public Actionn act { get; set; } = new();
		public Match? match { get; set; } = new();
		public List<Footballer> Footballers { get; private set; } = new();
		public List<Protocol> Protocols { get; private set; } = new();
		public List<Actionn> Actions { get; private set; } = new();
		public List<Replace> rep { get; private set; } = new();
		public SelectList SquadG { get; set; } = null!;
		public SelectList SquadA { get; set; } = null!;
		public SelectList CardOwners { get; set; } = null!;
		public SelectList CardTypes { get; set; } = null!;
		public int IdOfMatch;
		public async Task<IActionResult> OnPostAsync(List<int>? PlayersChecked, int[][]? PlayersCards, int[][]? replace, int[]? GoalMin, int[]? GoalId, int[]? AsistId, int[]? CardMin, int[]? CardId, int[]? TypeId,int[]? repMin, int[]? FootballerIn, int[]? FootballerOut, int mID)
		{
			match = context.Matches.Find(mID);
			Footballers = context.Footballers.Include(g => g.Team).Include(p => p.Position).AsNoTracking().ToList();
			foreach (var player in PlayersChecked)
			{
				Actions.Add(new Actionn { FootballerId = player, MatchId = match.Id, ActionTypeId = 6, Time = 0 });
			}
			

			for (int i = 0; i < match.Goals; i++)
			{
				if (GoalId[i] != 0)
				{
					Actions.Add(new Actionn { FootballerId = GoalId[i], MatchId = match.Id, ActionTypeId = 1, Time = GoalMin[i]});
				}
				if (AsistId[i] != 0)
				{
					Actions.Add(new Actionn { FootballerId = AsistId[i], MatchId = match.Id, ActionTypeId = 2, Time = GoalMin[i] });
				}
			}
			for (int i = 0; i < CardMin.Length; i++)
			{
				if (CardMin != null && CardId[i] > 0 && TypeId[i] > 0)
				{
					Actions.Add(new Actionn { FootballerId = CardId[i], MatchId = match.Id, ActionTypeId = TypeId[i], Time = CardMin[i] });
				}
			}
			

			for (int i = 0; i < repMin.Length; i++)
			{
				if (repMin != null && FootballerIn[i] > 0 && FootballerOut[i] > 0)
				{
					rep.Add(new Replace { FootballerInId = FootballerIn[i], FootballerOutId = FootballerOut[i], MatchID = match.Id, Time = repMin[i] });
					Actions.Add(new Actionn { FootballerId = FootballerIn[i], MatchId = match.Id, ActionTypeId = 6, Time = repMin[i] });
				}
			}
			context.Actions.AddRange(Actions);
			context.Replaces.AddRange(rep);
			await context.SaveChangesAsync();


			return LocalRedirect("~/TeamPage/" + match.TeamId);
		}
		public void OnGet(int mId)
        {
			IdOfMatch = mId;
			IQueryable<Footballer> footballersIQ = from s in context.Footballers
												   select s;
			IQueryable<Footballer> footballersIQ2 = footballersIQ.Where(s => s.PositionId == 100);
			match = context.Matches.Include(i=>i.Opponent).First(i=>i.Id == mId);
			if (match != null)
			{
				Protocols = context.Protocols.Where(i => i.MatchId == mId).AsNoTracking().ToList();

				foreach (var foot in Protocols)
				{
					footballersIQ2 = footballersIQ2.Concat(footballersIQ.Where(i => i.Id == foot.FootballerId));
				}
				Footballers = footballersIQ2.Include(p => p.Position).AsNoTracking().ToList();
			}
			else
			{
				Footballers = footballersIQ.Include(p=>p.Position).AsNoTracking().ToList();
			}
			List<Footballer> GoalAuthors = Footballers.ToList();
			List<Footballer> AsistAuthors = Footballers.ToList();
			List<Footballer> Cards = Footballers.ToList();
			
			Cards.Insert(0, new Footballer { Id = 0, Name = "Выберите футболиста", Surname = "" });
			GoalAuthors.Insert(0, new Footballer { Id = 0, Name = "Автогол", Surname = "" });
			AsistAuthors.Insert(0, new Footballer { Id = 0, Name = "Нет", Surname = "" });

			SquadG = new SelectList((from s in GoalAuthors
								select new
								{
									ID = s.Id,
									FullName = s.Surname + " " + s.Name
								}), "ID","FullName", null);
			SquadA = new SelectList((from s in AsistAuthors
									 select new
									 {
										 ID = s.Id,
										 FullName = s.Surname + " " + s.Name
									 }), "ID", "FullName", null);
			CardOwners = new SelectList((from s in Cards
									 select new
									 {
										 ID = s.Id,
										 FullName = s.Surname + " " + s.Name
									 }), "ID", "FullName", null);
			
			CardTypes = new SelectList(context.ActionTypes.Where(i=>i.Id == 3 || i.Id == 4), nameof(ActionType.Id), nameof(ActionType.Name));

		}
	}
}
