using Microsoft.EntityFrameworkCore;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.WindowModels;
using TransportationAnalyticsHub.MVVM.Windows;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class DriverViewModel : ShowTableViewModel<Kierowcy, AddDriverWindow, AddDriverWindowModel>
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
