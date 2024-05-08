using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TransportationAnalyticsHub.Prefabs
{
    public partial class DateTimeInputField : UserControl
    {
        #region Caption DTP

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(DateTimeInputField), new PropertyMetadata(null));

        #endregion

        #region Input DTP

        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(string), typeof(DateTimeInputField), new PropertyMetadata(null));

        #endregion

        public DateTimeInputField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
