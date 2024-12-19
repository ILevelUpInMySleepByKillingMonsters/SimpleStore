using Business.Abstractions.Services;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Business
{
    public static class Configuration
    {
        public static void ConfigureBusiness(this IHostApplicationBuilder builder)
        {
            ConfigureServices(builder.Services);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILoginService, SimpleLoginService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserManager, UserManager>();
        }
    }
}
