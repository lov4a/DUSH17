using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class Championship
	{
		[Key]
		public int Id { get; set; }
		public Competition Competition { get; set; } = null!;
		public int CompetitionId { get; set; }
		public int Rounds { get; set; }

	}
}
