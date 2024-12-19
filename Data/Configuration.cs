using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Data
{
    public static class Configuration
    {
        public static void ConfigureData(this IHostApplicationBuilder builder)
        {
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(connection)
            );
        }
    }
}
