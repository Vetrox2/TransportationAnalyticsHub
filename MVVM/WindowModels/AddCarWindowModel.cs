using Microsoft.IdentityModel.Tokens;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.WindowModels
{
    class AddCarWindowModel : AddInstanceWindowModelBase<SamochodyCiezarowe>
    {
        private string registration;
        private short year;
        private string maxVolume;
        private string maxWeight;
        private ComboBoxItemRep fuelType;
        private ComboBoxItemRep cargoType;
        private List<ComboBoxItemRep> fuelTypes;
        private List<ComboBoxItemRep> cargoTypes;

        public AddCarWindowModel()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                CargoTypes = new();
                context.TypyTowarus.ToList().ForEach(cargo => CargoTypes.Add(new ComboBoxItemRep(0, cargo.NazwaTypu)));
                FuelTypes = new();
                context.RodzajePaliwas.ToList().ForEach(fuel => FuelTypes.Add(new ComboBoxItemRep(0, fuel.NazwaPaliwa)));
            }
        }

        public string Registration
        {
            get => registration;
            set
            {
                if (value == null || DataValidator.ValidateRegistration(value))
                    registration = value;
                else
                    MessageBox.Show("Wrong registration syntax or is too long");

                OnPropertyChanged();
            }
        }

        public short Year
        {
            get => year;
            set
            {
                year = value;
                OnPropertyChanged();
            }
        }

        public string MaxVolume
        {
            get => maxVolume;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDecimal(value))
                    maxVolume = value;
                else
                    MessageBox.Show("Wrong syntax");

                OnPropertyChanged();
            }
        }

        public string MaxWeight
        {
            get => maxWeight;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDecimal(value))
                    maxWeight = value;
                else
                    MessageBox.Show("Wrong syntax");

                OnPropertyChanged();
            }
        }

        public ComboBoxItemRep FuelType
        {
            get => fuelType;
            set
            {
                fuelType = value;
                OnPropertyChanged();
            }
        }

        public ComboBoxItemRep CargoType
        {
            get => cargoType;
            set
            {
                cargoType = value;
                OnPropertyChanged();
            }
        }

        public List<ComboBoxItemRep> FuelTypes
        {
            get => fuelTypes;
            set
            {
                fuelTypes = value;
                OnPropertyChanged();
            }
        }

        public List<ComboBoxItemRep> CargoTypes
        {
            get => cargoTypes;
            set
            {
                cargoTypes = value;
                OnPropertyChanged();
            }
        }

        public override void FillFields(SamochodyCiezarowe car)
        {
            UpdatingObject = car;
            Registration = car.Rejestracja;
            Year = car.RokProdukcji;
            MaxVolume = car.MaksymalnaObjetoscZaladunkuM3.ToString();
            MaxWeight = car.MaksymalnaLadownoscT.ToString();
            CargoType = CargoTypes.Find(cargo => cargo.Text == car.TypTowaru);
            FuelType = FuelTypes.Find(fuel => fuel.Text == car.RodzajPaliwa);
        }

        protected override bool ValidateRequiredFields() => !(Registration.IsNullOrEmpty() || Year < 1950 || Year > DateTime.Now.Year || MaxWeight.IsNullOrEmpty() || FuelType == null || FuelType.Text.IsNullOrEmpty());

        protected override async void SaveChanges()
        {
            var newCar = UpdateMode ? UpdatingObject : new SamochodyCiezarowe();
            newCar.Rejestracja = Registration.ToUpper();
            newCar.RokProdukcji = Year;
            newCar.MaksymalnaObjetoscZaladunkuM3 = MaxVolume.IsNullOrEmpty() ? null : DataConverter.ConvertToDouble(MaxVolume);
            newCar.MaksymalnaLadownoscT = DataConverter.ConvertToDouble(MaxWeight);
            newCar.RodzajPaliwa = FuelType.Text;
            newCar.TypTowaru = CargoType == null || CargoType.Text.IsNullOrEmpty() ? null : CargoType.Text;

            if (!UpdateMode)
                DBManager.AddNewItemToDB(newCar, CallingVm);
            else
                DBManager.UpdateItemInDB(newCar, CallingVm);
        }
    }
}
