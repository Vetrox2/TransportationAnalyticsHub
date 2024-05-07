using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private string input;
        private object selectedFuel;
        private object selectedCargo;
        private List<Konfiguracja> salary;
        private List<RodzajePaliwa> fuelTypes;
        private List<TypyTowaru> cargoTypes;

        public RelayCommand AddFuel => new RelayCommand(_ =>
        {
            if (Input.IsNullOrEmpty()) return;

            DBManager.AddNewItemToDB(new RodzajePaliwa() { NazwaPaliwa = Input }, this);
        });

        public RelayCommand AddCargo => new RelayCommand(_ =>
        {
            if (Input.IsNullOrEmpty()) return;

            DBManager.AddNewItemToDB(new TypyTowaru() { NazwaTypu = Input }, this);
        });

        public RelayCommand DeleteFuel => new RelayCommand(_ =>
        {
            if (SelectedFuel == null) return;

            DBManager.DeleteItemFromDB((RodzajePaliwa)SelectedFuel, this);
        });

        public RelayCommand DeleteCargo => new RelayCommand(_ =>
        {
            if (SelectedCargo == null) return;

            DBManager.DeleteItemFromDB((TypyTowaru)SelectedCargo, this);
        });

        public RelayCommand UpdateSalary => new RelayCommand(_ =>
        {
            if (Input.IsNullOrEmpty() || !DataValidator.ValidateDecimal(Input))
                return;

            Salary[0].StawkaMinimalnaBrutto = DataConverter.ConvertToDecimal(Input);
            DBManager.UpdateItemInDB(Salary[0], this);
        });

        public string Input
        {
            get => input;
            set
            {
                input = value;
                OnPropertyChanged();
            }
        }

        public object SelectedFuel
        {
            get => selectedFuel;
            set
            {
                selectedFuel = value;
                OnPropertyChanged();
            }
        }

        public object SelectedCargo
        {
            get => selectedCargo;
            set
            {
                selectedCargo = value;
                OnPropertyChanged();
            }
        }

        public List<Konfiguracja> Salary
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChanged();
            }
        }

        public List<RodzajePaliwa> FuelTypes
        {
            get => fuelTypes;
            set
            {
                if (!value.IsNullOrEmpty())
                    fuelTypes = value;
                OnPropertyChanged();
            }
        }

        public List<TypyTowaru> CargoTypes
        {
            get => cargoTypes;
            set
            {
                if (!value.IsNullOrEmpty())
                    cargoTypes = value;
                OnPropertyChanged();
            }
        }

        public async override void UpdateSource()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                CargoTypes = await context.TypyTowarus.ToListAsync();
                FuelTypes = await context.RodzajePaliwas.ToListAsync();
                Salary = await context.Konfiguracjas.ToListAsync();
            }
        }
    }
}
