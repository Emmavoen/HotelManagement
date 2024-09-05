using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureService(this IServiceCollection services, IConfiguration conn)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddHttpClient();
            return services;
        }

    }
}