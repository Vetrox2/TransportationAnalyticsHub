using System.Windows;

namespace TransportationAnalyticsHub.MVVM.Windows
{
    public partial class AddAddressWindow : System.Windows.Window
    {
        public AddAddressWindow()
        {
            InitializeComponent();
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
    }
}
