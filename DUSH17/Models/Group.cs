using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class Group
	{
		[Key]
		public int Id { get; set; }
		public Competition Competition { get; set; } = null!;
		public int CompetitionId { get; set; }
		public string Name = null!;
	}
}
