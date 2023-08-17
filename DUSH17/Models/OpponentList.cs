namespace DUSH17.Models
{
	public class OpponentList
	{
		public int OpponentId { get; set; }
		public int CompetitionId { get; set; }
		public Opponent Opponent { get; set; } = null!;
		public Competition Competition { get; set; } = null!;
	}
}
