using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.View;
using TransportationAnalyticsHub.MVVM.ViewModel;
using TransportationAnalyticsHub.MVVM.WindowModels;

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

            services.AddSingleton<AddressesView>(provider => new AddressesView()
            {
                DataContext = provider.GetRequiredService<AddressesViewModel>(),
            });
            services.AddSingleton<AddressesViewModel>();

            services.AddSingleton<CarsView>(provider => new CarsView()
            {
                DataContext = provider.GetRequiredService<CarsViewModel>(),
            });
            services.AddSingleton<CarsViewModel>();

            services.AddSingleton<ConfigurationView>(provider => new ConfigurationView()
            {
                DataContext = provider.GetRequiredService<ConfigurationViewModel>(),
            });
            services.AddSingleton<ConfigurationViewModel>();

            services.AddSingleton<DriverView>(provider => new DriverView()
            {
                DataContext = provider.GetRequiredService<DriverViewModel>(),
            });
            services.AddSingleton<DriverViewModel>();

            services.AddSingleton<RidesView>(provider => new RidesView()
            {
                DataContext = provider.GetRequiredService<RidesViewModel>(),
            });
            services.AddSingleton<RidesViewModel>();

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
