using Lunatic.Application.Contracts.Identity;
using Lunatic.Domain.Models;
using Lunatic.Identity.Models;
using Lunatic.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lunatic.Identity
{
    public static class InfrastructureIdentityRegistrationDI {
        public static IServiceCollection AddInfrastructureIdentityToDI(
            this IServiceCollection services,
            IConfiguration configuration) {

            services.AddDbContext<ApplicationDbContext>(
               options => options.UseNpgsql(
                   configuration.GetConnectionString("LunaticConnection"),
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                   )
               );

            // For Identity  
            services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();
            // Adding Authentication  
            services.AddAuthentication(
                options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(// Adding Jwt Bearer  
                options => {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                }
            );

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
