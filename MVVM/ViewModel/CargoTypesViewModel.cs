using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.View;
using TransportationAnalyticsHub.MVVM.Windows;
using TransportationAnalyticsHub.MVVM.WindowModel;
using Microsoft.EntityFrameworkCore;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class CargoTypesViewModel : ShowTableViewModel<TypyTowaru, AddCargoTypeWindow, AddCargoTypeWindowModel>
    {
        public async override void UpdateSource()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                Source = await context.TypyTowarus.ToListAsync();
            }
        }
    }
}
