using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DUSH17.Models
{
	public class Footballer
	{
		[Key]
		public int Id { get; set; }
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Patronymic { get; set; } = null!;

		public DateOnly DateOfBirthday { get; set; }
		public DateOnly DateOfStart { get; set; }
		public Team Team { get; set; } = null!;
		public int TeamId { get; set; }
		public int Height { get; set; }
		public int Weight { get; set; }
		public Position Position { get; set; } = null!;
		public int PositionId { get; set; }
		public string PhoneNumber { get; set; } = null!;
		public Picture Picture { get; set; } = null!;
		public int PictureId { get; set; }
		public virtual ICollection<FootballerAchievement> FootballerAchievements { get; set; }
		public virtual ICollection<Visiting> Visits { get; set; }
		public Footballer()
		{
			FootballerAchievements = new HashSet<FootballerAchievement>();
			Protocols = new HashSet<Protocol>();
			Visits = new HashSet<Visiting>();
		}
		public virtual ICollection<Protocol> Protocols { get; set; }
	}
}
