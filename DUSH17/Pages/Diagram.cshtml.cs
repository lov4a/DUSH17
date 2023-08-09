using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Web;



namespace DUSH17.Pages
{
    public class DiagramModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public DiagramModel(ApplicationDbContext db)
        {
            context = db;
        }
        public List<Footballer> Footballers { get; set; } = null!;
        public List<Match> Matches { get; set; } = null!;
        public List<Actionn> Actions { get; private set; } = new();
        public List<Replace> Replaces { get; set; } = null!;
        public List<Protocol> Protocols { get; set; } = null!;
        public Team? team = new Team();
        public int amount = 10;
        public int Act = 0;
        public string? str = "������ �� ����";
		public SelectList Type { get; set; } = null!;
		public void OnGet(int? id, int am, string? fDate, string? sDate, int ActId)
        {
            team = context.Groups.Find(id);
            Footballers = context.Footballers.Where(i=>i.TeamId == id).Include(g => g.Team).Include(p => p.Position).OrderBy(p => p.PositionId).ThenBy(s => s.Surname).AsNoTracking().ToList();
            amount = am;
            Act = ActId;
            if (ActId == 6)
            {
				str = "������ �� ����";
			}
            else if(ActId == 0) {
				str = "������";
			}
			else if (ActId == 1){
				str = "����";
			}
			else if (ActId == 2){
				str = "������� �������";
			}
			else if (ActId == 3){
				str = "������ ��������";
			}
			else if (ActId == 4){
				str = "������� ��������";
			}
			else if (ActId == 7){
				str = "���� �� 90 �����";
			}
			else if (ActId == 8){
				str = "������� �������� �� 90 �����";
			}
			Matches = context.Matches.Where(i => i.TeamId == id).Include(c => c.Competition).ThenInclude(l => l.CompetitionLevel).Include(t => t.Team)
.Include(p => p.Protocols).ThenInclude(f => f.Footballer).OrderByDescending(i=>i.Date).Take(amount).AsNoTracking().ToList();
            if (fDate != null)
            {
                Matches = Matches.Where(i=>i.Date >= DateOnly.Parse(fDate)).ToList();
			}
            if (sDate != null)
            {
				Matches = Matches.Where(i => i.Date <= DateOnly.Parse(sDate)).ToList();
			}
            Replaces = context.Replaces.AsNoTracking().ToList();
            Protocols = context.Protocols.AsNoTracking().ToList();
            Actions = context.Actions.Include(a => a.ActionType).AsNoTracking().ToList();
			List<ActionType> Category = new List<ActionType>();

			Category.Insert(0, new ActionType { Id = 6, Name="������ �� ����"});
			Category.Insert(1, new ActionType { Id = 0, Name="������"});
			Category.Insert(2, new ActionType { Id = 1, Name="����"});
			Category.Insert(3, new ActionType { Id = 2, Name="������� ��������"});
			Category.Insert(4, new ActionType { Id = 3, Name="������ ��������"});
			Category.Insert(5, new ActionType { Id = 4, Name="������� ��������"});
			Category.Insert(6, new ActionType { Id = 7, Name="���� �� 90 �����"});
			Category.Insert(7, new ActionType { Id = 8, Name="������� �������� �� 90 �����"});

			Type = new SelectList((from s in Category
									select new
									{
										ID = s.Id,
										FName = s.Name
									}), "ID", "FName", null);
		}
	}
}
