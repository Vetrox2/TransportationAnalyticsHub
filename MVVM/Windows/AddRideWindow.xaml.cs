using System.Windows;

namespace TransportationAnalyticsHub.MVVM.Windows
{
    public partial class AddRideWindow : System.Windows.Window
    {
        public AddRideWindow()
        {
            InitializeComponent();
        }
        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
    }
}
