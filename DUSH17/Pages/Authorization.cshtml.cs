using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace DUSH17.Pages.Shared
{
    public class AuthorizationModel : PageModel
    {
		
		ApplicationDbContext context;
		public AuthorizationModel(ApplicationDbContext db)
		{
			context = db;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await context.Users
					.Include(u => u.Role)
					.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
				if (user != null)
				{
					await Authenticate(user); // аутентификация

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return LocalRedirect("~/Authorization/");
		}

		private async Task Authenticate(User user)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}
		public void OnGet()
        {
        }
    }
}
