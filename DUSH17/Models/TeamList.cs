namespace DUSH17.Models
{
	public class TeamList
	{
		public int TeamId { get; set; }
		public int CompetitionId { get; set; }
		public int GroupId { get; set; }
		public Team Team { get; set; } = null!;
		public Competition Competition { get; set; } = null!;
	}
}
