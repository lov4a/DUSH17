namespace DUSH17.Models
{
	public class OpponentsMatch
	{
		public int Id { get; set; }
		public Opponent Opponent1 { get; set; } = null!;
		public Opponent Opponent2 { get; set; } = null!;
		public int Opponent1Id { get; set; }
		public int Opponent2Id { get; set; }
		public int Goals1 { get; set; }
		public int Goals2 { get; set;}
		public Competition Competition { get; set; } = null!;
		public int CompetitionId { get; set; }
		public int Stage { get; set; }
		public DateOnly Date {	get; set; }
	}
}
