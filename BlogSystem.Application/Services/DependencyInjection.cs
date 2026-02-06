using BlogSystem.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;

        }
    }
}
