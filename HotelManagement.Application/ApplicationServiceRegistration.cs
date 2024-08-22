using System.Text;
using HotelManagement.Application.Contracts.Services;
using HotelManagement.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HotelManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection RegisterApplicationService(this IServiceCollection services, IConfiguration conn)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = conn["JWT:ValidAudience"],
                    ValidIssuer = conn["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conn["JWT:Secret"]))
                };
            });
            
            return services;
        }
    }
}