using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PipeLine.AdminDesktop.Extensions;
using PipeLine.AdminDesktop.Views;
using PipeLine.Desktop.Views;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PipeLine.AdminDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost host;
        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices(static services =>
                {
                    services.ConfigureViewViewModels();
                    services.AddDesktop();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var loginView = host.Services.GetRequiredService<LoginView>();
                loginView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();
            base.OnExit(e);
        }

    }
}