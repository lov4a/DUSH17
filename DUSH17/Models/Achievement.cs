using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        public int Place { get; set; } //Место, которое спортсмен занял на соревновании
        public Competition Competition { get; set; } = null!;
        public int CompetitionId { get; set; }
        public Team Team { get; set; } = null!;
        public int TeamId { get; set; }
		public virtual ICollection<FootballerAchievement> FootballerAchievements { get; set; }
		public Achievement()
		{
			FootballerAchievements = new HashSet<FootballerAchievement>();
		}



	}
}
