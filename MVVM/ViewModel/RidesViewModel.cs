using Microsoft.EntityFrameworkCore;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.WindowModels;
using TransportationAnalyticsHub.MVVM.Windows;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class RidesViewModel : ShowTableViewModel<Przejazdy, AddRideWindow, AddRideWindowModel>
    {
        private List<Kierowcy> drivers;
        private List<SamochodyCiezarowe> cars;
        private List<Adresy> addresses;

        public List<Kierowcy> Drivers
        {
            get => drivers;
            set
            {
                drivers = value;
                OnPropertyChanged();
            }
        }

        public List<SamochodyCiezarowe> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged();
            }
        }

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
                Source = await context.Przejazdies.Include(ad => ad.PunktyTrasies).ToListAsync();
            }
        }
    }
}
