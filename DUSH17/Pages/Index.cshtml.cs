using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DUSH17.Pages
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext context;
        public IndexModel(ApplicationDbContext db)
        {
            context = db;
        }
        public List<Team> Teams { get; private set; } = new();
        public Team bestTeam { get; private set; } = new();
        public List<Match> Matches { get; private set; } = new();
        public List<Match> LastMatches { get; private set; } = new();
        public List<Footballer> Footballers { get; private set; } = new();
        public List<Replace> Replaces { get; private set; } = new();
        public List<Actionn> Actions { get; private set; } = new();
        public List<Footballer> Birthday { get; private set; } = new();
        public Footballer bestFootballer { get; private set; } = new();

        public int goalsPlusA = 0;

        public void OnGet()
        {
            int draws = 0;
            int wins = 0;
            int points = 0;
            int games = 0;

            Teams = context.Groups.Include(c => c.Coach).Include(p => p.Picture).AsNoTracking().ToList();
            Matches = context.Matches.Where(i => i.Date > DateOnly.FromDateTime(DateTime.Now.AddDays(-30))).Include(i => i.Protocols).AsNoTracking().ToList();
            LastMatches = context.Matches.Include(i => i.Competition).Include(i => i.Team).Include(i=>i.Opponent).OrderByDescending(i => i.Date).Take(3).AsNoTracking().ToList();
            Footballers = context.Footballers.Include(i => i.Picture).AsNoTracking().ToList();
            Replaces = context.Replaces.AsNoTracking().ToList();
            Actions = context.Actions.Include(a => a.ActionType).Include(i=>i.Match).Where(i=>i.Match.Date > DateOnly.FromDateTime(DateTime.Now.AddDays(-30))).AsNoTracking().ToList();
            Birthday = context.Footballers.Where(i => i.DateOfBirthday.DayOfYear - DateTime.Now.DayOfYear >= 0 && i.DateOfBirthday.DayOfYear - DateTime.Now.DayOfYear <= 14)
                .OrderBy(i=>i.DateOfBirthday.DayOfYear).Include(i=>i.Picture).AsNoTracking().ToList();
            foreach (var team in Teams)
            {
                wins = Matches.Where(i => i.TeamId == team.Id && i.Goals > i.OpponentGoals).Count();
                draws = Matches.Where(i => i.TeamId == team.Id && i.Goals == i.OpponentGoals).Count();
                if (points < wins * 3 + draws)
                {
                    bestTeam = Teams.Where(i => i.Id == team.Id).First();
                    points = wins * 3 + draws;
                    games = Matches.Where(i => i.TeamId == team.Id).Count();
                }
                else if (points == wins * 3 + draws)
                {
                    if (games < Matches.Where(i => i.TeamId == team.Id).Count())
                    {
                        bestTeam = Teams.Where(i => i.Id == team.Id).First();
                        games = Matches.Where(i => i.TeamId == team.Id).Count();
                    }
                }
            }
            int minMinutes = 0;
            foreach (var foot in Footballers)
            {
                int totalMin = 0;
                foreach (var match in Matches.Where(i=>i.TeamId == foot.Id))
                {

                    if (match.Protocols.Where(pr => pr.FootballerId == foot.Id).Count() > 0)
                    {

                        int inMin = 0;
                        int outMin = 90;
                        foreach (var rep in Replaces.Where(fb => fb.MatchID == match.Id && (fb.FootballerInId == foot.Id || fb.FootballerOutId == foot.Id)))
                        {
                            if (rep.FootballerOutId == foot.Id)
                            {
                                outMin = rep.Time;
                            }
                            if (rep.FootballerInId == foot.Id)
                            {
                                inMin = rep.Time;
                            }
                        }
                        totalMin += outMin - inMin;
                    }
                }

                if (Actions.Where(x => x.FootballerId == foot.Id && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).Count() > goalsPlusA)
                {
                    bestFootballer = Footballers.Where(i => i.Id == foot.Id).First();
                    goalsPlusA = Actions.Where(x => x.FootballerId == foot.Id && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).Count();
                    minMinutes = totalMin;
                }
                else if (Actions.Where(x => x.FootballerId == foot.Id && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).Count() == goalsPlusA)
				{
                    if (totalMin < minMinutes)
                    {
                        bestFootballer = Footballers.Where(i => i.Id == foot.Id).First();
						goalsPlusA = Actions.Where(x => x.FootballerId == foot.Id && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).Count();
						minMinutes = totalMin;
                    }
                }
            }

        }
    }
}