namespace DUSH17.Models
{
	public class t_TeamList
	{
		public int teamId { get; set; }
		public int competitionId { get; set; }
		public int groupId { get; set; }
		public t_Team team { get; set; } = null!;
		public t_Competition competition { get; set; } = null!;
	}
}
