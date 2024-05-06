using System.Windows;

namespace TransportationAnalyticsHub.MVVM.Windows
{
    public partial class AddCarWindow : System.Windows.Window
    {
        public AddCarWindow()
        {
            InitializeComponent();
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
    }
}
