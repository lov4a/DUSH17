using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class Picture
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = null!;

	}
}
