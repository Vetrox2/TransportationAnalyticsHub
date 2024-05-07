using System.Collections;
using System.Windows;
using System.Windows.Controls;
using TransportationAnalyticsHub.Core;

namespace TransportationAnalyticsHub.Prefabs
{
    public partial class ComboBoxField : UserControl
    {
        #region Caption DP

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(ComboBoxField), new PropertyMetadata(null));

        #endregion

        #region Input DP

        public ComboBoxItemRep Input
        {
            get { return (ComboBoxItemRep)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(ComboBoxItemRep), typeof(ComboBoxField), new PropertyMetadata(null));

        #endregion

        #region Source DP

        public ICollection Source
        {
            get { return (ICollection)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ICollection), typeof(ComboBoxField), new PropertyMetadata(null));

        #endregion
        public ComboBoxField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

    }
}
