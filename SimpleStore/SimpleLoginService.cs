using EmptyStore.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace SimpleStore
{
    public class SimpleLoginService : ILoginService
    {
        public void SignIn(HttpContext httpContext, string name)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public void SignOut(HttpContext httpContext)
        {
            httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
