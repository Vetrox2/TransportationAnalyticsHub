using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.ViewModel;
using TransportationAnalyticsHub.MVVM.View;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub
{
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;
        public App()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>(),
            });
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<DriverView>(provider => new DriverView()
            {
                DataContext = provider.GetRequiredService<DriverViewModel>(),
            });
            services.AddSingleton<DriverViewModel>();

            services.AddSingleton<INavigationService, Navigation>();

            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => 
            viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            var mainWindowVM = serviceProvider.GetService<MainWindowViewModel>();
            mainWindowVM.Window = mainWindow;
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
