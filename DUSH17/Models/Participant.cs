namespace DUSH17.Models
{
	public class Participant
	{
		public int Id { get; set; }
		public string Name { get; set; } =  null!;
		public Picture Picture { get; set; } = null!;
		public int wins { get; set; }
		public int draws { get; set; }
		public int loses { get; set; }
		public int goalsScored { get; set; }
		public int goalsConceded { get; set; }
		public int type { get; set; }
	}
}
