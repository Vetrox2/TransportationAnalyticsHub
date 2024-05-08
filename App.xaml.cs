using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.View;
using TransportationAnalyticsHub.MVVM.View.AddViews;
using TransportationAnalyticsHub.MVVM.ViewModel;
using TransportationAnalyticsHub.MVVM.ViewModel.AddViewModel;
using TransportationAnalyticsHub.MVVM.WindowModels;
using TransportationAnalyticsHub.MVVM.Windows;

namespace TransportationAnalyticsHub
{
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;
        public App()
        {
            var services = new ServiceCollection();

            //Main Window Views
            services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainWindowModel>(),
            });
            services.AddSingleton<MainWindowModel>();

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

            //Add Ride Views
            services.AddSingleton<AddRideWindow>(provider => new AddRideWindow()
            {
                DataContext = provider.GetRequiredService<AddRideWindowModel>(),
            });
            services.AddSingleton<AddRideWindowModel>();

            services.AddSingleton<AddRideFormView>(provider => new AddRideFormView()
            {
                DataContext = provider.GetRequiredService<AddRideFormViewModel>(),
            });
            services.AddSingleton<AddRideFormViewModel>();

            services.AddSingleton<AddRideAddressesView>(provider => new AddRideAddressesView()
            {
                DataContext = provider.GetRequiredService<AddRideAddressesViewModel>(),
            });
            services.AddSingleton<AddRideAddressesViewModel>();

            //Raports
            services.AddSingleton<RaportDriversWorkTimeView>(provider => new RaportDriversWorkTimeView()
            {
                DataContext = provider.GetRequiredService<RaportDriversWorkTimeViewModel>(),
            });
            services.AddSingleton<RaportDriversWorkTimeViewModel>();

            services.AddSingleton<RaportCarsPayoffView>(provider => new RaportCarsPayoffView()
            {
                DataContext = provider.GetRequiredService<RaportCarsPayoffViewModel>(),
            });
            services.AddSingleton<RaportCarsPayoffViewModel>();

            services.AddSingleton<RaportDriversPayoffView>(provider => new RaportDriversPayoffView()
            {
                DataContext = provider.GetRequiredService<RaportDriversPayoffViewModel>(),
            });
            services.AddSingleton<RaportDriversPayoffViewModel>();

            services.AddSingleton<RaportRidesCostView>(provider => new RaportRidesCostView()
            {
                DataContext = provider.GetRequiredService<RaportRidesCostViewModel>(),
            });
            services.AddSingleton<RaportRidesCostViewModel>();

            //Navigation
            services.AddSingleton<INavigationService, Navigation>();

            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider =>
            viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            var mainWindowVM = serviceProvider.GetService<MainWindowModel>();
            mainWindowVM.Window = mainWindow;
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
