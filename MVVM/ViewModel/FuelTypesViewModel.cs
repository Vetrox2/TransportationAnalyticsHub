using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.Window;
using TransportationAnalyticsHub.MVVM.WindowModel;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class FuelTypesViewModel : ShowTableViewModel<RodzajePaliwa, AddFuelTypeWindow, AddFuelTypeWindowModel>
    {
    }
}
