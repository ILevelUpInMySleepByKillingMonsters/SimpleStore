using Business.Abstractions.Services;
using Data;
using Data.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Business.Services
{
    public class SimpleLoginService : ILoginService
    {
        public void SignIn(HttpContext httpContext, string id)
        {
            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier, id) 
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public void SignOut(HttpContext httpContext)
        {
            httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public int GetUserId(HttpContext httpContext)
        {
            var userId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }
    }
}
