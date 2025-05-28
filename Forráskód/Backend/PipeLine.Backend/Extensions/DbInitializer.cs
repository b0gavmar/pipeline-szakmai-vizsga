using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PipeLine.Backend.Context;

namespace PipeLine.Backend.Extensions
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PipeLineMySqlContext>();

                context.Database.Migrate();
            }
        }
    }
}