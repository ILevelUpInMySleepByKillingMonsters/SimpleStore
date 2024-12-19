using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Endpoint
{
    public static class Configuration
    {
        public static void ConfigureEndpoint(this IHostApplicationBuilder builder)
        {
            ConfigureAuthentication(builder.Services, builder.Configuration);

            builder.Services.AddControllersWithViews();
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                });
        }
    }
}
