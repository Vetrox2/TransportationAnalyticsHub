using System.Windows;
using System.Windows.Controls;

namespace TransportationAnalyticsHub.Prefabs
{
    public partial class DateInputField : UserControl
    {
        #region Caption DP

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(DateInputField), new PropertyMetadata(null));

        #endregion

        #region Input DP

        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(string), typeof(DateInputField), new PropertyMetadata(null));

        #endregion
        public DateInputField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

    }
}
