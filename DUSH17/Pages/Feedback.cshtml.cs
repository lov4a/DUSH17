using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Mail;

namespace DUSH17.Pages
{
	public class FeedbackModel : PageModel
    {
		[BindProperty]
		public string customerName { get; set; } = null!;
		[BindProperty]
		public string customerEmail { get; set; } = null!;
		[BindProperty]
		public string customerRequest { get; set; } = null!;
		public string errorMessage { get; set; } = null!;

		public bool debuggingFlag = false;
		[BindProperty]
		public ContactModel contactModel { get; set; } = new();
		public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostSendEmailAsync()
		{
				try
				{
					string emailBody = "����� ��������� �� ����� \n��: " + contactModel.Name + "\nEmail: " + contactModel.Email + "\n���������: " + contactModel.Message;
					MailMessage mailObj = new MailMessage("sh17@1gb.ru", "st-vosxod@mail.ru", "�������� ����� � �����", emailBody);
					mailObj.From = new MailAddress("sh17@1gb.ru", "�������� �����");			
					SmtpClient SMTPServer = new SmtpClient("robots.1gb.ru");
					SMTPServer.Send(mailObj);
					TempData["SuccessMessage"] = "���� ��������� ����������!";
				}
				catch (Exception ex)
				{
					errorMessage = "Error: " + ex.Message;
				}
			System.Diagnostics.Debug.WriteLine(errorMessage);
			return Page();
		}
	}
}
