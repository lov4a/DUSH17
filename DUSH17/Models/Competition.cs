using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Competition
    {
        [Key]
        public int Id { get; set; }
        public string NameOfCompetition { get; set; } = null!;
        public CompetitionLevel CompetitionLevel { get; set; } = null!;
        public int CompetitionLevelId { get; set; }
        public string City { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int CountOfTeams { get; set; }

		public virtual ICollection<TeamList> TeamLists { get; set; }
		public virtual ICollection<OpponentList> OpponentLists { get; set; }
		public Competition()
		{
			TeamLists = new HashSet<TeamList>();
			OpponentLists = new HashSet<OpponentList>();
		}

	}
}
