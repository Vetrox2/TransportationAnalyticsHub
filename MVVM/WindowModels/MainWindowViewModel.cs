using RozliczeniePrzejazdowApp.Core;
using System.Windows;
using TransportationAnalyticsHub.Core;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    class MainWindowViewModel : Core.ViewModelBase
    {
        public MainWindow Window;

        private INavigationService navigation;

        public MainWindowViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            NavigateToHome = new(_ => { Navigation.NavigateTo<HomeViewModel>(); });
            NavigateToDriver = new(_ => { Navigation.NavigateTo<DriverViewModel>(); });
            NavigateToRides = new(_ => { Navigation.NavigateTo<RidesViewModel>(); });
            NavigateToCars = new(_ => { Navigation.NavigateTo<CarsViewModel>(); });
            NavigateToAddresses = new(_ => { Navigation.NavigateTo<AddressesViewModel>(); });
            NavigateToConfiguration = new(_ => { Navigation.NavigateTo<ConfigurationViewModel>(); });
        }

        public RelayCommand MinimizeWindow => new(_ => Window.WindowState = WindowState.Minimized);
        public RelayCommand MaximizeWindow => new(_ =>
        {
            Window.WindowState = (Window.WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal;
            Window.BorderThickness = (Window.WindowState != WindowState.Maximized) ? new Thickness(0) : new Thickness(8);
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
