using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
	public class ContactModel
	{
		[Required(ErrorMessage = "Введите ваше имя")]
		public string Name { get; set; } = null!;

		[DataType(DataType.EmailAddress), Required(ErrorMessage = "Пожалуйста, введите ваш email")]
		public string Email { get; set; } = null!;

		[Required]
		[DataType(DataType.MultilineText)]
		public string Message { get; set; } = null!;
	}
}
