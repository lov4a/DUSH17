using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public Team Team { get; set; } = null!;
        public int TeamId { get; set; }
        public DateOnly Date { get; set; }

		public virtual ICollection<Visiting> Visits { get; set; }
		public Lesson()
		{
			Visits = new HashSet<Visiting>();
		}

	}
}
