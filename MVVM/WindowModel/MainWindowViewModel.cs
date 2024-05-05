using Microsoft.EntityFrameworkCore.Metadata;
using RozliczeniePrzejazdowApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TransportationAnalyticsHub.Core;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    class MainWindowViewModel : Core.ViewModelBase
    {
        public MainWindow Window;

        public RelayCommand MinimizeWindow => new(_ => Window.WindowState = WindowState.Minimized);

        public RelayCommand MaximizeWindow => new(_ =>
        {
            Window.WindowState = (Window.WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal;
            Window.BorderThickness = (Window.WindowState != WindowState.Maximized) ? new Thickness(0) : new Thickness(8);
        });

        public RelayCommand CloseWindow => new(_ => Window.Close());

        public HomeViewModel MainVW;

        private INavigationService navigation;
        public INavigationService Navigation
        {
            get => navigation;
            set
            {
                navigation = value;
                OnPropertyChanged();
            }
        }
        private string txt;
        public string Txt
        {
            get => txt;
            set
            {
                txt = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand NavigateToHome { get; set; }
        public RelayCommand NavigateToDriver { get; set; }
        public RelayCommand NavigateToRides { get; set; }
        public RelayCommand NavigateToCars { get; set; }
        public RelayCommand NavigateToAddresses { get; set; }
        public RelayCommand NavigateToFuelTypes { get; set; }
        public RelayCommand NavigateToCargoTypes { get; set; }
        public RelayCommand NavigateToConfiguration { get; set; }
        public MainWindowViewModel(INavigationService navigation)
        {
            Txt = "eoeoeoeoeooe";
            Navigation = navigation;
            NavigateToHome = new(_ => { Navigation.NavigateTo<HomeViewModel>(); });
            NavigateToDriver = new(_ => { Navigation.NavigateTo<DriverViewModel>(); });
            NavigateToRides = new(_ => { Navigation.NavigateTo<RidesViewModel>(); });
            NavigateToCars = new(_ => { Navigation.NavigateTo<CarsViewModel>(); });
            NavigateToAddresses = new(_ => { Navigation.NavigateTo<AddressesViewModel>(); });
            NavigateToFuelTypes = new(_ => { Navigation.NavigateTo<FuelTypesViewModel>(); });
            NavigateToCargoTypes = new(_ => { Navigation.NavigateTo<CargoTypesViewModel>(); });
            NavigateToConfiguration = new(_ => { Navigation.NavigateTo<ConfigurationViewModel>(); });
        }
    }
}
