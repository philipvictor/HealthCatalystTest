
using HealthCatalystUserSearchAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCatalystUserSearchAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("myconn")));
        }
    }


}
