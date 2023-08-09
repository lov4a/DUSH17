using DUSH17.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace DUSH17.Models
{
    public class AccountModel
    {

        private List<User> users;

        public User find(string username)
        {
            return users.SingleOrDefault(a => a.Login.Equals(username));
        }

        public User login(string username, string password)
        {
            return users.SingleOrDefault(a => a.Login.Equals(username) && a.Password.Equals(password));
        }
    }
}
