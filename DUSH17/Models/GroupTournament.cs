using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DUSH17.Models
{
	public class GroupTournament
	{
		[Key]
		public int CompetitionId { get; set; }
		[ForeignKey("CompetitionId")]
		public Competition Competition { get; set; } = null!;
		public int GroupsCount { get; set; }
		public int Round { get; set; }
		public int Winers { get; set; }
	}
}
