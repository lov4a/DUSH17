using DUSH17.Context;
using DUSH17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DUSH17.Pages
{
    public class LoginModel : PageModel
    {
        public string Msg;

        private SecurityManager securityManager = new SecurityManager();
        private readonly ApplicationDbContext context;
        public LoginModel(ApplicationDbContext db)
        {
            context = db;
        }
        private List<User> users = null!;

        public void OnGet()
        {
            users = context.Users.AsNoTracking().ToList();
        }

        public IActionResult OnPost(string username, string password)
        {
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || login(username, password) == null)
            {
                Msg = "Invalid";
                return RedirectToPage("Players");
            }
            else
            {
                securityManager.SignIn(HttpContext, find(username));
                return RedirectToPage("Teams");
            }
        }
        public User find(string username)
        {
            users = context.Users.Include(r => r.Role).AsNoTracking().ToList();
            return users.SingleOrDefault(a => a.Login.Equals(username));
        }

        public User login(string username, string password)
        {
            users = context.Users.Include(r => r.Role).AsNoTracking().ToList();
            return users.SingleOrDefault(a => a.Login.Equals(username) && a.Password.Equals(password));
        }

        public IActionResult OnGetLogout()
        {
            securityManager.SignOut(HttpContext);
            return RedirectToPage("Index");
        }
    }
}
