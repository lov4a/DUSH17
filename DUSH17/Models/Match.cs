using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public Competition Competition { get; set; } = null!;
        public int CompetitionId { get; set; }
        public DateOnly Date { get; set; }
        public bool Home { get; set; }
        public Opponent Opponent { get; set; } = null!;
        public int OpponentId { get; set; }
        public Team Team { get; set; } = null!;
        public int TeamId { get; set; }
        public int Goals { get; set; }
        public int OpponentGoals { get; set; }
        public int OpponentYear { get; set; }
		public virtual ICollection<Protocol> Protocols { get; set; }
		public Match()
		{
			Protocols = new HashSet<Protocol>();
		}
	}
}
