namespace DUSH17.Models
{
	public class Stat
	{
		public int Id { get; set; }
		public Footballer footballer { get; set; } = null!;
		public int games { get; set; }
		public int goals { get; set; }
		public int assists { get; set; }
		public int YK { get; set; }
		public int RK { get; set; }
		public double minutes { get; set; }
		public double g90 { get; set; }
		public double a90 { get; set; }
		public int gPlusA { get; set; }
	}
}
