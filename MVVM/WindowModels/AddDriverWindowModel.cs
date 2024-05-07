using Microsoft.IdentityModel.Tokens;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.ViewModel
{
    public class AddDriverWindowModel : AddInstanceWindowModelBase<Kierowcy>
    {
        private List<ComboBoxItemRep> addresses;
        private string name;
        private string surname;
        private string pesel;
        private string birthday;
        private ComboBoxItemRep address;
        private string salary;

        public AddDriverWindowModel()
        {
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
            {
                Addresses = new();
                context.Adresies.ToList().ForEach(address => Addresses.Add(new ComboBoxItemRep(address)));
                birthday = "1/1/2000 12:00:00 AM";
            }
        }

        public List<ComboBoxItemRep> Addresses
        {
            get => addresses;
            set
            {
                addresses = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (DataValidator.ValidateName(value))
                    name = value;
                else
                    MessageBox.Show("Wrong name syntax");

                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                if (DataValidator.ValidateName(value))
                    surname = value;
                else
                    MessageBox.Show("Wrong surname syntax");

                OnPropertyChanged();
            }
        }

        public string Pesel
        {
            get => pesel;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidatePesel(value))
                    pesel = value;
                else
                    MessageBox.Show("Wrong pesel syntax");

                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get => birthday;
            set
            {
                if (value.IsNullOrEmpty() || DataValidator.ValidateAge18(value))
                    birthday = value;
                else
                    MessageBox.Show("Driver must have at least 18 years old");

                OnPropertyChanged();
            }
        }

        public ComboBoxItemRep Address
        {
            get => address;
            set
            {
                address = value;
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
                    MessageBox.Show("Wrong salary syntax");

                OnPropertyChanged();
            }
        }

        public override void FillFields(Kierowcy driver)
        {
            UpdatingObject = driver;
            Name = driver.Imie;
            Surname = driver.Nazwisko;
            Pesel = driver.Pesel;
            Address = Addresses.Find(address => address.Id == driver.AdresId);
            Birthday = DataConverter.ConvertDateOnlyToString(driver.DataUrodzenia);
            Salary = driver.StawkaGodzinowaBrutto.ToString();
        }

        protected override bool ValidateRequiredFields() => !(Name.IsNullOrEmpty() || Surname.IsNullOrEmpty() || Salary.IsNullOrEmpty()
            || Birthday == null || Address == null || Address.Id < 1);

        protected override async void SaveChanges()
        {
            var newDriver = UpdateMode ? UpdatingObject : new Kierowcy();
            newDriver.Imie = Name;
            newDriver.Nazwisko = Surname;
            newDriver.Pesel = Pesel.IsNullOrEmpty() ? null : Pesel;
            newDriver.StawkaGodzinowaBrutto = DataConverter.ConvertToDecimal(Salary);
            newDriver.DataUrodzenia = DataConverter.ConvertToDateOnly(Birthday);
            newDriver.AdresId = Address.Id;

            if (!UpdateMode)
                DBManager.AddNewItemToDB(newDriver, CallingVm);
            else
                DBManager.UpdateItemInDB(newDriver, CallingVm);
        }
    }
}
