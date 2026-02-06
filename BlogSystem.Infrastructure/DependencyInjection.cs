using BlogSystem.Domain.Interfaces;
using BlogSystem.Infrastructure.Persistence.Context;
using BlogSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                IConfiguration configuration)
        {

            // Register infrastructure services here
            services.AddDbContext<ApplicationDbContext>(options=>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServer=>sqlServer.CommandTimeout(180)
                    );
            });
            services.AddScoped<IUserRepository,UserRepository>();
            return services;
        }
    }
}
