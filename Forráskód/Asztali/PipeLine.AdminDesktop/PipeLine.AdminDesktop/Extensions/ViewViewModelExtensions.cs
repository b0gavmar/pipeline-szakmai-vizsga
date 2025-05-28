using Microsoft.Extensions.DependencyInjection;
using PipeLine.AdminDesktop.ViewModels;
using PipeLine.AdminDesktop.ViewModels.ChargingInstances;
using PipeLine.AdminDesktop.ViewModels.ChargingStations;
using PipeLine.AdminDesktop.ViewModels.ControlPanel;
using PipeLine.AdminDesktop.ViewModels.Devices;
using PipeLine.AdminDesktop.ViewModels.ErrorTickets2;
using PipeLine.AdminDesktop.ViewModels.Users;
using PipeLine.AdminDesktop.Views;
using PipeLine.AdminDesktop.Views.ChargingInstances;
using PipeLine.AdminDesktop.Views.ChargingStations;
using PipeLine.AdminDesktop.Views.ControlPanel;
using PipeLine.AdminDesktop.Views.Devices;
using PipeLine.AdminDesktop.Views.ErrorTickets2;
using PipeLine.AdminDesktop.Views.Users;
using PipeLine.Desktop.ViewModels;
using PipeLine.Desktop.Views;

namespace PipeLine.AdminDesktop.Extensions
{
    public static class ViewViewModelsExtensions
    {
        public static void ConfigureViewViewModels(this IServiceCollection services)
        {
            // MainView
            services.AddSingleton<MainViewModel>();
            services.AddSingleton(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            // ControlPanel
            services.AddSingleton<ControlPanelViewModel>();
            services.AddSingleton(s => new ControlPanelView()
            {
                DataContext = s.GetRequiredService<ControlPanelViewModel>()
            });

            services.AddSingleton<DevicesViewModel>();
            services.AddSingleton(s => new DevicesView()
            {
                DataContext = s.GetRequiredService<DevicesViewModel>()
            });


            services.AddSingleton<ChargingStationsViewModel>();
            services.AddSingleton(s => new ChargingStationsView()
            {
                DataContext = s.GetRequiredService<ChargingStationsViewModel>()
            });

            services.AddSingleton<ChargingStationsListViewModel>();
            services.AddSingleton(s => new ChargingStationListView()
            {
                DataContext = s.GetRequiredService<ChargingStationsListViewModel>()
            });

            services.AddSingleton<ChargingPortsListViewModel>();
            services.AddSingleton(s => new ChargingPortsListView()
            {
                DataContext = s.GetRequiredService<ChargingPortsListViewModel>()
            });

            services.AddSingleton<ChargingInstancesListViewModel>();
            services.AddSingleton(s => new ChargingInstancesListView()
            {
                DataContext = s.GetRequiredService<ChargingInstancesListViewModel>()
            });

            services.AddSingleton<UnifiedChargingStationViewModel>();
            services.AddSingleton(s => new UnifiedChargingStationView()
            {
                DataContext = s.GetRequiredService<UnifiedChargingStationViewModel>()
            });

            services.AddSingleton<ChargingInstancesViewModel>();
            services.AddSingleton(s => new ChargingInstancesView()
            {
                DataContext = s.GetRequiredService<ChargingInstancesViewModel>()
            });

            services.AddSingleton<AddNewChargingStationViewModel>();
            services.AddSingleton(s => new AddNewChargingStationView()
            {
                DataContext = s.GetRequiredService<AddNewChargingStationViewModel>()
            });

            services.AddSingleton<AddNewChargingPortViewModel>();
            services.AddSingleton(s => new AddNewChargingPortView()
            {
                DataContext = s.GetRequiredService<AddNewChargingPortViewModel>()
            });




            services.AddSingleton<UnifiedDeviceViewModel>();
            services.AddSingleton(s => new UnifiedDeviceView()
            {
                DataContext = s.GetRequiredService<UnifiedDeviceViewModel>()
            });

            services.AddSingleton<UsersViewModel>();
            services.AddSingleton(s => new UsersView()
            {
                DataContext = s.GetRequiredService<UsersViewModel>()
            });

            services.AddSingleton<ApplicationUserViewModel>();
            services.AddSingleton(s => new ApplicationUserView()
            {
                DataContext = s.GetService<ApplicationUserViewModel>()
            });

            services.AddSingleton<UnifiedUserViewModel>();
            services.AddSingleton(s => new UnifiedUserView()
            {
                DataContext = s.GetRequiredService<UnifiedUserViewModel>()
            });

            services.AddSingleton<AddNewUserViewModel>();
            services.AddSingleton(s => new AddNewUserView()
            {
                DataContext = s.GetRequiredService<AddNewUserViewModel>()
            });

            services.AddSingleton<AddNewDeviceViewModel>();
            services.AddSingleton(s => new AddNewDeviceView()
            {
                DataContext = s.GetRequiredService<AddNewDeviceViewModel>()
            });


            services.AddSingleton<LoginViewModel>();
            services.AddSingleton(s => new LoginView()
            {
                DataContext = s.GetRequiredService<LoginViewModel>()
            });

            services.AddSingleton<ErrorTicketsListViewModel2>();
            services.AddSingleton(s => new Views.ErrorTickets2.ErrorTicketsListView()
            {
                DataContext = s.GetRequiredService<ErrorTicketsListViewModel2>()
            });
            services.AddSingleton<ErrorTicketsViewModel2>();
            services.AddSingleton(s => new Views.ErrorTickets2.ErrorTicketsView()
            {
                DataContext = s.GetRequiredService<ErrorTicketsViewModel2>()
            });
            services.AddSingleton<AddNewErrorTicketsViewModel>();
            services.AddSingleton(s => new AddNewErrorTicketsView()
            {
                DataContext = s.GetRequiredService<AddNewErrorTicketsViewModel>()
            });
        }
    }
}