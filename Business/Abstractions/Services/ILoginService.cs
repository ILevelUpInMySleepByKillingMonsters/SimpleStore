using Microsoft.AspNetCore.Http;

namespace Business.Abstractions.Services
{
    public interface ILoginService
    {
        void SignIn(HttpContext httpContext, string name);
        void SignOut(HttpContext httpContext);
        int GetUserId(HttpContext httpContext);
    }
}
