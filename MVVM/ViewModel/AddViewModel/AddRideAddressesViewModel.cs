using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.WindowModels;
using TransportationAnalyticsHub.MVVM.Windows;

namespace TransportationAnalyticsHub.MVVM.ViewModel.AddViewModel
{
    internal class AddRideAddressesViewModel : ViewModelBase
    {
        private Przejazdy ride;
        private List<PunktyTrasy> ridePoints;
        private List<Adresy> chosenAddresses;
        private List<ComboBoxItemRep> comboAddresses;
        private List<Adresy> addresses;
        private Adresy selected;

        public AddRideAddressesViewModel(ref Przejazdy ride, List<PunktyTrasy> ridePoints)
        {
            this.ride = ride;
            this.ridePoints = ridePoints;
            ComboAddresses = new List<ComboBoxItemRep>();
            ChosenAddresses = new List<Adresy>();
        }

        public RelayCommand MoveUp => new RelayCommand(_ =>
        {
            if (Selected == null) return;

            var index = ChosenAddresses.FindIndex(item => item.AdresId == Selected.AdresId);
            if (index == -1 || index == 0)
                return;

            ChosenAddresses.RemoveAt(index);
            ChosenAddresses.Insert(index - 1, Selected);
            UpdateChosenList();
        });

        public RelayCommand MoveDown => new RelayCommand(_ =>
        {
            if (Selected == null) return;

            var index = ChosenAddresses.FindIndex(item => item.AdresId == Selected.AdresId);
            if (index == -1 || index == ChosenAddresses.Count - 1)
                return;

            ChosenAddresses.RemoveAt(index);
            ChosenAddresses.Insert(index + 1, Selected);
            UpdateChosenList();
        });

        public RelayCommand Delete => new RelayCommand(_ =>
        {
            if (Selected == null) return;

            var index = ChosenAddresses.FindIndex(item => item.AdresId == Selected.AdresId);
            if (index == -1)
                return;

            ChosenAddresses.RemoveAt(index);
            UpdateChosenList();
        });

        public List<Adresy> ChosenAddresses
        {
            get => chosenAddresses;
            set
            {
                chosenAddresses = value;
                OnPropertyChanged();
            }
        }

        public List<ComboBoxItemRep> ComboAddresses
        {
            get => comboAddresses;
            set
            {
                comboAddresses = value;
                OnPropertyChanged();
            }
        }

        public Adresy Selected
        {
            get => selected;
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        public ComboBoxItemRep AddressToAdd
        {
            get => null;
            set
            {
                ChosenAddresses.Add(addresses.Find(address => address.AdresId == value.Id));
                UpdateChosenList();
                OnPropertyChanged();
            }
        }

        public void FillFields(Przejazdy ride, List<PunktyTrasy> ridePoints)
        {
            ChosenAddresses = new();
            this.ride = ride;
            this.ridePoints = ridePoints;
            ridePoints.ForEach(point => ChosenAddresses.Add(point.Adres));
        }

        public async override void UpdateSource()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                addresses = await context.Adresies.ToListAsync();
                addresses.ForEach(address => ComboAddresses.Add(new ComboBoxItemRep(address)));
            }
        }

        public bool InsertDataToObject()
        {
            if (ChosenAddresses.Count < 2) return false;

            ridePoints.Clear();
            ChosenAddresses.ForEach(address => ridePoints.Add(new PunktyTrasy() { Adres= address, Przejazd = ride}));
            return true;
        }

        private void UpdateChosenList() => ChosenAddresses = new List<Adresy>(ChosenAddresses);
    }
}
