using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.WindowModel
{
    class AddCarWindowModel : AddInstanceWindowModelBase<SamochodyCiezarowe>
    {
        private string registration;
        public string Registration
        {
            get => registration;
            set
            {
                if (value == null || Regex.Match(value, LatinCharValidationStr).Success && value.Length <= 8)
                    registration = value;
                else
                    MessageBox.Show("Wrong registration syntax");
                OnPropertyChanged();
            }
        }

        private short year;
        public short Year
        {
            get => year;
            set
            {
                year = value;
                OnPropertyChanged();
            }
        }

        private string maxVolume;
        public string MaxVolume
        {
            get => maxVolume;
            set
            {
                if (value == string.Empty || Regex.Match(value, DecimalValidationStr).Success)
                    maxVolume = value;
                else
                    MessageBox.Show("Wrong syntax");
                OnPropertyChanged();
            }
        }

        private string maxWeight;
        public string MaxWeight
        {
            get => maxWeight;
            set
            {
                if (value == string.Empty || Regex.Match(value, DecimalValidationStr).Success)
                    maxWeight = value;
                else
                    MessageBox.Show("Wrong syntax");
                OnPropertyChanged();
            }
        }

        private ComboBoxItemRep fuelType;
        public ComboBoxItemRep FuelType
        {
            get => fuelType;
            set
            {
                fuelType = value;
                OnPropertyChanged();
            }
        }

        private ComboBoxItemRep cargoType;
        public ComboBoxItemRep CargoType
        {
            get => cargoType;
            set
            {
                cargoType = value;
                OnPropertyChanged();
            }
        }

        private List<ComboBoxItemRep> fuelTypes;
        public List<ComboBoxItemRep> FuelTypes
        {
            get => fuelTypes;
            set
            {
                fuelTypes = value;
                OnPropertyChanged();
            }
        }

        private List<ComboBoxItemRep> cargoTypes;
        public List<ComboBoxItemRep> CargoTypes
        {
            get => cargoTypes;
            set
            {
                cargoTypes = value;
                OnPropertyChanged();
            }
        }

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

        public override void FillFields(SamochodyCiezarowe car)
        {
            SourceID = car.SamochodCiezarowyId;
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
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                var newCar = UpdateMode ? await context.SamochodyCiezarowes.FirstAsync(car => car.SamochodCiezarowyId == SourceID) : new SamochodyCiezarowe();
                newCar.Rejestracja = Registration.ToUpper();
                newCar.RokProdukcji = Year;
                newCar.MaksymalnaObjetoscZaladunkuM3 = MaxVolume.IsNullOrEmpty() ? null : double.Parse(MaxVolume.Replace('.', ','));
                newCar.MaksymalnaLadownoscT = double.Parse(MaxWeight.Replace('.', ','));
                newCar.RodzajPaliwa = FuelType.Text;
                newCar.TypTowaru = (CargoType == null || CargoType.Text.IsNullOrEmpty()) ? null : CargoType.Text;

                if (UpdateMode)
                    context.Update<SamochodyCiezarowe>(newCar);
                else
                    context.Add<SamochodyCiezarowe>(newCar);

                context.SaveChanges();
                CallingVm.UpdateSource();
            }
        }
    }
}
