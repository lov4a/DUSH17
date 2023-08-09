namespace DUSH17.Models
{
	public class Protocol
	{
		public int FootballerId { get; set; }
		public int MatchId { get; set; }
		public Footballer Footballer { get; set; } = null!;
		public Match Match { get; set; } = null!;
	}
}
