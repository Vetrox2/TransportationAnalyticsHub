

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using RozliczeniePrzejazdowApp.Core;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.Prefabs;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    public class AddDriverWindowModel : AddInstanceWindowModelBase<Kierowcy>
    {
        private List<ComboBoxItemRep> addresses;
        public List<ComboBoxItemRep> Addresses
        {
            get => addresses;
            set
            {
                addresses = value;
                OnPropertyChanged();
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (Regex.Match(value, NameValidationStr).Success)
                    name = value;
                else
                    MessageBox.Show("Wrong Name");
                OnPropertyChanged();
            }
        }

        private string surname;
        public string Surname
        {
            get => surname;
            set
            {
                if (Regex.Match(value, NameValidationStr).Success)
                    surname = value;
                else
                    MessageBox.Show("Wrong Surname");
                OnPropertyChanged();
            }
        }

        private string pesel;
        public string Pesel
        {
            get => pesel;
            set
            {
                if (value == null || Regex.Match(value, IntValidationStr).Success && value.Length == 11)
                    pesel = value;
                else
                    MessageBox.Show("Wrong pesel syntax");
                OnPropertyChanged();
            }
        }

        private string birthday;
        public string Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
        }

        private ComboBoxItemRep address;
        public ComboBoxItemRep Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private string salary;
        public string Salary
        {
            get => salary;
            set
            {
                if (value == string.Empty || Regex.Match(value, MoneyValidationStr).Success)
                    salary = value;
                else
                    MessageBox.Show("Wrong syntax");
                OnPropertyChanged();
            }
        }

        public AddDriverWindowModel()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                Addresses = new();
                context.Adresies.ToList().ForEach(address => Addresses.Add(new ComboBoxItemRep(address)));
            }
        }

        public override void FillFields(Kierowcy driver)
        {
            DriverId = driver.KierowcaId;
            Name = driver.Imie;
            Surname = driver.Nazwisko;
            Pesel = driver.Pesel;
            Address = Addresses.Find(address => address.Id == driver.AdresId);

            var data = DateTime.ParseExact(driver.DataUrodzenia.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            string formatedDate = data.ToString("M/d/yyyy").Replace('.', '/');
            Birthday = formatedDate += " 12:00:00 AM";

            Salary = driver.StawkaGodzinowaBrutto.ToString();
        }

        protected override bool ValidateRequiredFields() => Name.IsNullOrEmpty() || Surname.IsNullOrEmpty() || Salary.IsNullOrEmpty() || Birthday == null || Address.Id < 1;

        protected override async void SaveChanges()
        {
            DateTime birthdayDT;
            if (!DateTime.TryParseExact(Birthday.Substring(0, Birthday.IndexOf(' ')), DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdayDT))
            {
                MessageBox.Show("Wrong date format");
                return;
            }

            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                var newDriver = UpdateMode ? await context.Kierowcies.FirstAsync(driver => driver.KierowcaId == DriverId) : new Kierowcy();
                newDriver.Imie = Name;
                newDriver.Nazwisko = Surname;
                newDriver.Pesel = Pesel.IsNullOrEmpty() ? null : Pesel;
                newDriver.StawkaGodzinowaBrutto = decimal.Parse(Salary.Replace('.', ','));
                newDriver.DataUrodzenia = DateOnly.FromDateTime(birthdayDT);
                newDriver.AdresId = Address.Id;

                if (UpdateMode)
                    context.Update<Kierowcy>(newDriver);
                else
                    context.Add<Kierowcy>(newDriver);

                context.SaveChanges();
                CallingVm.UpdateSource();
            }
        }
    }
}
