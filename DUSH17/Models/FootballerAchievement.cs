using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class FootballerAchievement
	{

		public int FootballerId { get; set; }
		public int AchievementId { get; set; }
		public Footballer Footballer { get; set; } = null!;
		public Achievement Achievement { get; set; } = null!;
	}
}
