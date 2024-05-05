using Microsoft.EntityFrameworkCore;
using RozliczeniePrzejazdowApp.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.View;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class DriverViewModel : ShowTableViewModel<Kierowcy,AddDriverWindow,AddDriverWindowModel>
    {

        private List<Adresy> addresses;
        public List<Adresy> Addresses 
        { 
            get => addresses; 
            set 
            {
                addresses = value; 
                OnPropertyChanged(); 
            } 
        }

        public async override void UpdateSource()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                Addresses = await context.Adresies.ToListAsync();
                Source = await context.Kierowcies.ToListAsync();
            }
        }

    }

}
