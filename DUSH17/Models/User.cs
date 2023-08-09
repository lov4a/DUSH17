using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Login { get; set; } = null!;
		public string Password { get; set; } = null!;
		public int? RoleId { get; set; }
		public Role Role { get; set; } = null!;
	}
}
