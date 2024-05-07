using System.Windows;

namespace TransportationAnalyticsHub.Prefabs
{
    public partial class TextInputField : System.Windows.Controls.UserControl
    {
        #region Caption DP

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(TextInputField), new PropertyMetadata(null));

        #endregion

        #region Input DP

        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(string), typeof(TextInputField), new PropertyMetadata(null));

        #endregion
        public TextInputField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

    }
}
