using Azure.Core.GeoJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using RozliczeniePrzejazdowApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        protected const string DecimalValidationStr = @"^[0-9]+([,.][0-9]{0,4})?$";

        public RelayCommand AddFuel => new RelayCommand(_ =>
        {
            if (Input.IsNullOrEmpty()) return;

            AddNewItemToDB(new RodzajePaliwa() { NazwaPaliwa = Input });
        });

        public RelayCommand AddCargo => new RelayCommand(_ =>
        {
            if (Input.IsNullOrEmpty()) return;

            AddNewItemToDB(new TypyTowaru() { NazwaTypu = Input });
        });

        public RelayCommand DeleteFuel => new RelayCommand(_ =>
        {
            if (SelectedFuel == null) return;

            DeleteItemFromDB((RodzajePaliwa)SelectedFuel);
        });

        public RelayCommand DeleteCargo => new RelayCommand(_ =>
        {
            if (SelectedCargo == null) return;

            DeleteItemFromDB((TypyTowaru)SelectedCargo);
        });
        
        public RelayCommand UpdateSalary => new RelayCommand(_ =>
        {
            if (Input.IsNullOrEmpty() || !Regex.Match(Input, DecimalValidationStr).Success) 
                return;

            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                    Salary[0].StawkaMinimalnaBrutto = decimal.Parse(Input.Replace('.',','));
                context.Update<Konfiguracja>(Salary[0]);
                context.SaveChanges();
            }
            UpdateSource();
        });

        private string input;
        public string Input
        {
            get => input;
            set
            {
                input = value;
                OnPropertyChanged();
            }
        }

        private object selectedFuel;
        public object SelectedFuel
        {
            get => selectedFuel;
            set
            {
                selectedFuel = value;
                OnPropertyChanged();
            }
        }

        private object selectedCargo;
        public object SelectedCargo
        {
            get => selectedCargo;
            set
            {
                selectedCargo = value;
                OnPropertyChanged();
            }
        }

        private List<Konfiguracja> salary;
        public List<Konfiguracja> Salary
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChanged();
            }
        }

        private List<RodzajePaliwa> fuelTypes;
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

        private List<TypyTowaru> cargoTypes;
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

        private void AddNewItemToDB<T>(T newObject) where T : class
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                context.Add(newObject);
                context.SaveChanges();
            }
            UpdateSource();
        }

        private void DeleteItemFromDB<T>(T item) where T : class
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                context.Remove(item);
                try 
                {
                    context.SaveChanges();
                }
                catch (Exception e) { }
            }
            UpdateSource();
        }
    }
}
