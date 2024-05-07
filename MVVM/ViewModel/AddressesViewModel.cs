using Microsoft.EntityFrameworkCore;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.WindowModel;
using TransportationAnalyticsHub.MVVM.Windows;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class AddressesViewModel : ShowTableViewModel<Adresy, AddAddressWindow, AddAddressWindowModel>
    {
        public async override void UpdateSource()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                Source = await context.Adresies.ToListAsync();
            }
        }
    }
}
