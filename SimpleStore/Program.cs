using Data;
using Business;
using Endpoint;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureData();
        builder.ConfigureBusiness();
        builder.ConfigureEndpoint();

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Login}/{action=Index}");

        app.Run();
    }
}
