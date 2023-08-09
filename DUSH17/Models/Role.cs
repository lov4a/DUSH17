using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class Role
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public List<User> Users { get; set; }
		public Role()
		{
			Users = new List<User>();
		}
	}
}
