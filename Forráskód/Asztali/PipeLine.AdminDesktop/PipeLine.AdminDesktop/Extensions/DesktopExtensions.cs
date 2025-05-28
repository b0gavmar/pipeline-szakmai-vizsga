using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using PipeLine.Backend.Assemblers.UserAssemblers;
using PipeLine.HttpService;
using PipeLine.HttpService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.Extensions
{
    public static class DesktopExtensions
    {
        public static void AddDesktop(this IServiceCollection services)
        {
            services.ConfigureHttpClient();
            services.ConfigureHttpServices();
            services.ConfigureAssemblers();
        }

        private static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("PipeLineApi", configureClient =>
            {
                configureClient.BaseAddress = new Uri("https://localhost:7020/");
            });
        }

        private static void ConfigureHttpServices(this IServiceCollection services)
        {
            services.AddScoped<IChargingPortHttpService, ChargingPortHttpService>();
            services.AddScoped<IChargingStationHttpService, ChargingStationHttpService>();
            services.AddScoped<IDeviceHttpService, DeviceHttpService>();
            services.AddScoped<IApplicationUserHttpService, ApplicationUserHttpService>();
            services.AddScoped<IErrorTicketHttpService, ErrorTicketHttpService>();
            services.AddScoped<IChargingInstanceHttpService, ChargingInstanceHttpService>();
            services.AddScoped<LoginHttpService>();
        }

        private static void ConfigureAssemblers(this IServiceCollection services)
        {
            services.AddScoped<IChargingPortAssembler,ChargingPortAssembler>();
            services.AddScoped<IChargingStationAssembler, ChargingStationAssembler>();
            services.AddScoped<IDeviceAssembler,DeviceAssembler>();
            services.AddScoped<IApplicationUserAssembler,ApplicationUserAssembler>();
            services.AddScoped<IErrorTicketAssembler, ErrorTicketAssembler>();
            services.AddScoped<IChargingInstanceAssembler,ChargingInstanceAssembler>();

        }
    }
}
