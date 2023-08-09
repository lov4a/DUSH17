using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public Coach Coach { get; set; } = null!;
        public int CoachId { get; set; }
        public int Year { get; set; }
        public Picture Picture { get; set; } = null!;
        public int PictureId { get; set; }
		public virtual ICollection<TeamList> TeamLists { get; set; }
		public Team()
		{
			TeamLists = new HashSet<TeamList>();
		}
	}
}
