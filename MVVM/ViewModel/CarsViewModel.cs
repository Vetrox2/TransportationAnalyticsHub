using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.WindowModel;
using TransportationAnalyticsHub.MVVM.Windows;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class CarsViewModel : ShowTableViewModel<SamochodyCiezarowe, AddCarWindow, AddCarWindowModel>
    {
        public async override void UpdateSource()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                Source = await context.SamochodyCiezarowes.ToListAsync();
            }
        }
    }
}
