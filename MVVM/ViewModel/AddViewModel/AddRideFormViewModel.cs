using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.ViewModel.AddViewModel
{
    public class AddRideFormViewModel : ViewModelBase
    {
        private Przejazdy ride;
        private List<SamochodyCiezarowe> allCars;
        private ComboBoxItemRep selectedDriver;
        private List<ComboBoxItemRep> drivers;
        private string salary;
        private string beginDate;
        private string endDate;
        private ComboBoxItemRep selectedCar;
        private List<ComboBoxItemRep> cars;
        private string usedFuel;
        private string fuelPrice;
        private string extraCost;
        private ComboBoxItemRep selectedCargoType;
        private List<ComboBoxItemRep> cargoTypes;
        private string cargoWeight;
        private string cargoVolume;

        public AddRideFormViewModel(ref Przejazdy ride)
        {
            this.ride = ride;

            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                CargoTypes = new();
                context.TypyTowarus.ToList().ForEach(cargo => CargoTypes.Add(new ComboBoxItemRep(0, cargo.NazwaTypu)));
                Drivers = new();
                context.Kierowcies.ToList().ForEach(driver => Drivers.Add(new ComboBoxItemRep(driver.KierowcaId, $"{driver.Imie} {driver.Nazwisko}")));

                allCars = context.SamochodyCiezarowes.ToList();
                Cars = new();
                allCars.ForEach(car => Cars.Add(new ComboBoxItemRep(car.SamochodCiezarowyId, car.Rejestracja)));
            }
        }

        public ComboBoxItemRep SelectedDriver
        {
            get => selectedDriver;
            set
            {
                selectedDriver = value;
                OnPropertyChanged();
            }
        }

        public List<ComboBoxItemRep> Drivers
        {
            get => drivers;
            set
            {
                drivers = value;
                OnPropertyChanged();
            }
        }

        public string Salary
        {
            get => salary;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDecimal(value))
                    salary = value;
                else
                    MessageBox.Show("Wrong number syntax");

                OnPropertyChanged();
            }
        }

        public string BeginDate
        {
            get => beginDate;
            set
            {
                var dateTimeRep = DataConverter.ConvertToDateTime(value, false);
                if (value.IsNullOrEmpty() || (DataValidator.ValidateNotFutureDateTime(dateTimeRep) && 
                    (EndDate.IsNullOrEmpty() || dateTimeRep < DataConverter.ConvertToDateTime(EndDate, false))))
                    beginDate = value;
                else
                    MessageBox.Show("Wrong date");

                OnPropertyChanged();
            }
        }

        public string EndDate
        {
            get => endDate;
            set
            {
                var dateTimeRep = DataConverter.ConvertToDateTime(value, false);
                if (value.IsNullOrEmpty() || (DataValidator.ValidateNotFutureDateTime(dateTimeRep) && 
                    (BeginDate.IsNullOrEmpty() || dateTimeRep > DataConverter.ConvertToDateTime(BeginDate, false))))
                    endDate = value;
                else
                    MessageBox.Show("Wrong date");

                OnPropertyChanged();
            }
        }

        public ComboBoxItemRep SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                OnPropertyChanged();
            }
        }

        public List<ComboBoxItemRep> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged();
            }
        }

        public string UsedFuel
        {
            get => usedFuel;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDouble(value))
                    usedFuel = value;
                else
                    MessageBox.Show("Wrong number syntax");

                OnPropertyChanged();
            }
        }

        public string FuelPrice
        {
            get => fuelPrice;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDecimal(value))
                    fuelPrice = value;
                else
                    MessageBox.Show("Wrong number syntax");

                OnPropertyChanged();
            }
        }

        public string ExtraCost
        {
            get => extraCost;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDecimal(value))
                    extraCost = value;
                else
                    MessageBox.Show("Wrong number syntax");

                OnPropertyChanged();
            }
        }

        public ComboBoxItemRep SelectedCargoType
        {
            get => selectedCargoType;
            set
            {
                selectedCargoType = value;
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
        public string CargoWeight
        {
            get => cargoWeight;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDouble(value))
                    cargoWeight = value;
                else
                    MessageBox.Show("Wrong number syntax");

                OnPropertyChanged();
            }
        }
        public string CargoVolume
        {
            get => cargoVolume;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateDouble(value))
                    cargoVolume = value;
                else
                    MessageBox.Show("Wrong number syntax");

                OnPropertyChanged();
            }
        }

        public void FillFields(Przejazdy ride)
        {
            this.ride = ride;
            SelectedDriver = Drivers.Find(driver => driver.Id == ride.KierowcaId);
            Salary = ride.StawkaGodzinowaBruttoKierowcy.ToString();
            BeginDate = ride.DataRozpoczeciaPrzejazdu.ToString("H:mm d.MM.yyyy");
            EndDate = ride.DataZakonczeniaPrzejazdu.ToString("H:mm d.MM.yyyy");
            SelectedCar = Cars.Find(car => car.Id == ride.SamochodCiezarowyId);
            UsedFuel = ride.ZuzytePaliwoL.ToString();
            FuelPrice = ride.CenaPaliwaZlL.ToString();
            ExtraCost = ride.DodatkoweKoszty.ToString();
            SelectedCargoType = CargoTypes.Find(type => type.Text == ride.TypTowaru);
            CargoWeight = (ride.WspolczynnikLadownosci * ride.SamochodCiezarowy.MaksymalnaLadownoscT).ToString();
            cargoVolume = (ride.WspolczynnikObjetosci * ride.SamochodCiezarowy.MaksymalnaObjetoscZaladunkuM3).ToString();
        }

        public bool InsertDataToObject()
        {
            if (SelectedDriver == null || Salary.IsNullOrEmpty() || BeginDate.IsNullOrEmpty() || EndDate.IsNullOrEmpty() || SelectedCar == null || UsedFuel.IsNullOrEmpty() 
                || FuelPrice.IsNullOrEmpty() || SelectedCargoType == null || CargoWeight.IsNullOrEmpty())
                return false;

            ride.KierowcaId = SelectedDriver.Id;
            ride.StawkaGodzinowaBruttoKierowcy = DataConverter.ConvertToDecimal(Salary);
            ride.DataRozpoczeciaPrzejazdu = DataConverter.ConvertToDateTime(BeginDate, false);
            ride.DataZakonczeniaPrzejazdu = DataConverter.ConvertToDateTime(EndDate, false);
            ride.SamochodCiezarowyId = SelectedCar.Id;
            ride.ZuzytePaliwoL = DataConverter.ConvertToDouble(UsedFuel);
            ride.CenaPaliwaZlL = DataConverter.ConvertToDecimal(FuelPrice);
            ride.DodatkoweKoszty = ExtraCost == null? null : DataConverter.ConvertToDecimal(ExtraCost);
            ride.TypTowaru = SelectedCargoType.Text;

            SamochodyCiezarowe car = allCars.Find(car => car.SamochodCiezarowyId == ride.SamochodCiezarowyId);
            ride.WspolczynnikLadownosci = DataConverter.ConvertToDouble(CargoWeight) / car.MaksymalnaLadownoscT;
            ride.WspolczynnikObjetosci = car.MaksymalnaObjetoscZaladunkuM3 == null || cargoVolume.IsNullOrEmpty() ? null : DataConverter.ConvertToDouble(cargoVolume) / car.MaksymalnaObjetoscZaladunkuM3;

            return true;
        }
    }
}
