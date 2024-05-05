using System.Windows;

namespace TransportationAnalyticsHub.MVVM.Windows
{
    public partial class AddDriverWindow : System.Windows.Window
    {
        public AddDriverWindow()
        {
            InitializeComponent();
        }
        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
    }
}
