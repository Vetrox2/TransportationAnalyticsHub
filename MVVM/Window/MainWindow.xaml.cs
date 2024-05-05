using System.Windows;
using TransportationAnalyticsHub.MVVM.ViewModel;

namespace TransportationAnalyticsHub
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void MoveWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}