using HotelManagement.Domain.Entities;
using HotelManagement.Persistence.DataBaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceService(this IServiceCollection services, IConfiguration conn)
        {

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                    conn.GetConnectionString("Conn")));
                    services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}