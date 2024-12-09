namespace SimpleStore
{
    public interface ILoginService
    {
        void SignIn(HttpContext httpContext, string name);
        void SignOut(HttpContext httpContext);
    }
}
