using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class LoginModel
	{

		[Required(ErrorMessage = "Не указан Email")]
		public string Login { get; set; } = null!;

		[Required(ErrorMessage = "Не указан пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
	}
}
