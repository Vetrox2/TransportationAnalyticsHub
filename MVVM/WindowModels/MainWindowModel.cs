using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.ViewModel;

namespace TransportationAnalyticsHub.MVVM.WindowModels
{
    class MainWindowModel : ViewModelBase
    {
        public MainWindow Window;

        private INavigationService navigation;

        public MainWindowModel(INavigationService navigation)
        {
            Navigation = navigation;
            NavigateToHome = new(_ => { Navigation.NavigateTo<HomeViewModel>(); });
            NavigateToDriver = new(_ => { Navigation.NavigateTo<DriverViewModel>(); });
            NavigateToRides = new(_ => { Navigation.NavigateTo<RidesViewModel>(); });
            NavigateToCars = new(_ => { Navigation.NavigateTo<CarsViewModel>(); });
            NavigateToAddresses = new(_ => { Navigation.NavigateTo<AddressesViewModel>(); });
            NavigateToConfiguration = new(_ => { Navigation.NavigateTo<ConfigurationViewModel>(); });
            NavigateToRaportDriversWorkTime = new(_ => { Navigation.NavigateTo<RaportDriversWorkTimeViewModel>(); });
            NavigateToRaportDriversPayoff = new(_ => { Navigation.NavigateTo<RaportDriversPayoffViewModel>(); });
            NavigateToRaportCarsPayoff = new(_ => { Navigation.NavigateTo<RaportCarsPayoffViewModel>(); });
            NavigateToRaportRidesCost = new(_ => { Navigation.NavigateTo<RaportRidesCostViewModel>(); });
        }

        public RelayCommand MinimizeWindow => new(_ => Window.WindowState = WindowState.Minimized);
        public RelayCommand MaximizeWindow => new(_ =>
        {
            Window.WindowState = Window.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
            Window.BorderThickness = Window.WindowState != WindowState.Maximized ? new Thickness(0) : new Thickness(8);
        });
        public RelayCommand CloseWindow => new(_ => Window.Close());

        public RelayCommand NavigateToHome { get; set; }
        public RelayCommand NavigateToDriver { get; set; }
        public RelayCommand NavigateToRides { get; set; }
        public RelayCommand NavigateToCars { get; set; }
        public RelayCommand NavigateToAddresses { get; set; }
        public RelayCommand NavigateToFuelTypes { get; set; }
        public RelayCommand NavigateToCargoTypes { get; set; }
        public RelayCommand NavigateToConfiguration { get; set; }
        public RelayCommand NavigateToRaportDriversWorkTime { get; set; }
        public RelayCommand NavigateToRaportDriversPayoff { get; set; }
        public RelayCommand NavigateToRaportCarsPayoff { get; set; }
        public RelayCommand NavigateToRaportRidesCost { get; set; }

        public INavigationService Navigation
        {
            get => navigation;
            set
            {
                navigation = value;
                OnPropertyChanged();
            }
        }
    }
}
