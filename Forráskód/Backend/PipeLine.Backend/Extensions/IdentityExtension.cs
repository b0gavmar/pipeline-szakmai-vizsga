using Microsoft.AspNetCore.Identity;
using PipeLine.Backend.Context;
using PipeLine.Shared.Models.UserModels;
using System.Configuration;

namespace PipeLine.Backend.Extensions
{
    public static class IdentityExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<PipeLineMySqlContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });

            /*services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(30);
            });*/
        }
    }
}
