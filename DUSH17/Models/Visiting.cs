using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Visiting
    {
		public int FootballerId { get; set; }
		public int LessonId { get; set; }
		public Lesson Lesson { get; set; } = null!;
        public Footballer Footballer { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
