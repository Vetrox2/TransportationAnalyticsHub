﻿using System;
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
using System.Windows.Shapes;
using TransportationAnalyticsHub.MVVM.ViewModel;

namespace TransportationAnalyticsHub.MVVM.View
{
    public partial class AddDriverWindow : Window
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
