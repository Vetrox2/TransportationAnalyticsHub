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
        public RelayCommand NavigateToHomeCommand { get; set; }
        public RelayCommand NavigateToSearchKierowcyCommand { get; set; }
        public MainWindowViewModel(INavigationService navigation)
        {
            Txt = "eoeoeoeoeooe";
            Navigation = navigation;
            NavigateToHomeCommand = new(_ => { Navigation.NavigateTo<HomeViewModel>(); });
            NavigateToSearchKierowcyCommand = new(_ => { Navigation.NavigateTo<DriverViewModel>(); });
        }
    }
}
