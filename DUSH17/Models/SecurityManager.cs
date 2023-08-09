using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DUSH17.Models
{
    public class SecurityManager
    {
        public async void SignIn(HttpContext httpContext, User account)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Login));
            identity.AddClaim(new Claim(ClaimTypes.Name, account.Login));
            identity.AddClaims(getUserClaims(account));
            var principal = new ClaimsPrincipal(identity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> getUserClaims(User account)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, account.Login));
            claims.Add(new Claim(ClaimTypes.Role, account.Role.Name));
            
            return claims;
        }
    }
}
