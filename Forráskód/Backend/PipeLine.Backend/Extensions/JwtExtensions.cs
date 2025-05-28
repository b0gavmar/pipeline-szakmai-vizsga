using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PipeLine.Backend.Extensions
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudiences = configuration.GetSection("JwtSettings:Audience").Get<string[]>(),
                        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("jwt_pipeline"))
                            {
                                context.Token = context.Request.Cookies["jwt_pipeline"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/api/authentication/login"; 
            });

            return services;
        }
    }
}
