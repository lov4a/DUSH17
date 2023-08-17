namespace DUSH17.Pages
{
	public class ChampionshipPage
	{
		private readonly ApplicationDbContext context;
		public ChampionshipPageModel(ApplicationDbContext db)
		{
			context = db;
		}
		public Competition Competition { get; set; }
	}
}
