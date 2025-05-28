

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using PipeLine.Backend.Assemblers.UserAssemblers;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Repos.ChargingStationRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Backend.Repos.ErrorTicketRepos;
using PipeLine.Backend.Repos.UserRepos;
using PipeLine.Backend.Services.Authentication;
using PipeLine.Backend.Services.ChargingInstanceServices;
using PipeLine.Backend.Services.ChargingStationServices;
using PipeLine.Backend.Services.ChargingStationServices.ChargingPortServices;
using PipeLine.Backend.Services.DeviceServices;
using PipeLine.Backend.Services.ErrorTicketServices;
using PipeLine.Backend.Services.UserServices;
using PipeLine.Backend.WebSockets;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using System.Reflection;

namespace PipeLine.Backend.Extensions
{
    public static class PipeLineBackedExtension
    {
        public static void AddBackend(this IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureInMemoryContext();
            services.ConfigureMySqlContext();
            services.ConfigureRepos();
            services.ConfigureAssemblers();
            services.ConfigureServices();
            services.ConfigureWebSockets();
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(option =>
                option.AddPolicy(name: "PipeLineCors",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:8100", "http://localhost:5173", "https://localhost/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    }
                )
           );
        }

        public static void ConfigureInMemoryContext(this IServiceCollection services)
        {
            /*string dbNamePipeLineContext = "PipeLine" + Guid.NewGuid();
            services.AddDbContext<PipeLineContext>
            (
                 options => options.UseInMemoryDatabase(databaseName: dbNamePipeLineContext),
                 ServiceLifetime.Scoped,
                 ServiceLifetime.Scoped
            );*/

            string dbNameInMemoryContext = "PipeLine" + Guid.NewGuid();
            services.AddDbContext<PipeLineInMemoryContext>
            (
                 options => options.UseInMemoryDatabase(databaseName: dbNameInMemoryContext),
                 ServiceLifetime.Scoped,
                 ServiceLifetime.Scoped
            );

        }

        private static void ConfigureMySqlContext(this IServiceCollection services)
        {
            string connectionString = "server=localhost;userid=root;password=;database=pipeline;port=3306";
            services.AddDbContext<PipeLineMySqlContext>(options => options.UseMySQL(connectionString));
        }

        private static void ConfigureRepos(this IServiceCollection services)
        {
            services.AddScoped<IDeviceRepo, DeviceRepo<PipeLineMySqlContext>>();
            services.AddScoped<IUserRepo, UserRepo<PipeLineMySqlContext>>();
            services.AddScoped<IChargingStationRepo, ChargingStationRepo<PipeLineMySqlContext>>();
            services.AddScoped<IChargingPortRepo, ChargingPortRepo<PipeLineMySqlContext>>();
            services.AddScoped<IErrorTicketRepo, ErrorTicketRepo<PipeLineMySqlContext>>();
            services.AddScoped<IChargingInstanceRepo, ChargingInstanceRepo<PipeLineMySqlContext>>();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IChargingInstanceService, ChargingInstanceService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChargingStationService, ChargingStationService>();
            services.AddScoped<IChargingPortService, ChargingPortService>();
            services.AddScoped<IErrorTicketService, ErrorTicketService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtService, JwtService>();
        }

        private static void ConfigureWebSockets(this IServiceCollection services)
        {
            services.AddScoped<IChargingProgressWebSocketHandler, ChargingProgressWebSocketHandler>();
            
        }

        private static void ConfigureAssemblers(this IServiceCollection services)
        {
            services.AddScoped<IDeviceAssembler, DeviceAssembler>();
            services.AddScoped<IApplicationUserAssembler, ApplicationUserAssembler>();
            services.AddScoped<IChargingStationAssembler, ChargingStationAssembler>();
            services.AddScoped<IErrorTicketAssembler, ErrorTicketAssembler>();
            services.AddScoped<IChargingPortAssembler, ChargingPortAssembler>();
            services.AddScoped<IChargingInstanceAssembler, ChargingInstanceAssembler>();

        }
    }
}
