using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.View;
using TransportationAnalyticsHub.MVVM.WindowModel;
using TransportationAnalyticsHub.MVVM.Windows;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class RidesViewModel : ShowTableViewModel<Przejazdy, AddRideWindow, AddRideWindowModel>
    {
    }
}
