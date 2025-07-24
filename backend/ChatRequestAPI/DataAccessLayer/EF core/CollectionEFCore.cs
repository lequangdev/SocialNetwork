using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataAccessLayer.EF_core
{
    public static class CollectionEFCore 
    {
        // Entity Frame Work core
        public static IServiceCollection AddEntityFrameWorkCore(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = configuration.GetSection(nameof(EntityFrameWorkConfiguration)).Get<EntityFrameWorkConfiguration>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    jwtConfiguration!.DefaultConnection,
                    new MySqlServerVersion(new Version(8, 0, 29))
                )
            );

            return services;
        }
    }
}
