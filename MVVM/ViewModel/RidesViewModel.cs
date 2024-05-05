using Microsoft.EntityFrameworkCore;
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
        private List<Kierowcy> drivers;
        public List<Kierowcy> Drivers
        {
            get => drivers;
            set
            {
                drivers = value;
                OnPropertyChanged();
            }
        }

        private List<SamochodyCiezarowe> cars;
        public List<SamochodyCiezarowe> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged();
            }
        }

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
                Drivers = await context.Kierowcies.ToListAsync();
                Cars = await context.SamochodyCiezarowes.ToListAsync();
                Source = await context.Przejazdies.Include(ad => ad.Adres).ToListAsync();
            }
        }
    }
}
