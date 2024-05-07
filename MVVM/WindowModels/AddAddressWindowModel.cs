using Microsoft.IdentityModel.Tokens;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.WindowModels
{
    class AddAddressWindowModel : AddInstanceWindowModelBase<Adresy>
    {
        private string street;
        private string houseNr;
        private string apartment;
        private string postalCode;
        private string city;
        private string country;


        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnPropertyChanged();
            }
        }

        public string HouseNr
        {
            get => houseNr;
            set
            {
                houseNr = value;
                OnPropertyChanged();
            }
        }

        public string Apartment
        {
            get => apartment;
            set
            {
                apartment = value;
                OnPropertyChanged();
            }
        }

        public string PostalCode
        {
            get => postalCode;
            set
            {
                postalCode = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged();
            }
        }

        public override void FillFields(Adresy address)
        {
            Street = address.Ulica;
            HouseNr = address.NumerBudynku;
            Apartment = address.NumerLokalu;
            PostalCode = address.KodPocztowy;
            City = address.Miejscowosc;
            Country = address.Kraj;
        }

        protected override bool ValidateRequiredFields() => !(Street.IsNullOrEmpty() || HouseNr.IsNullOrEmpty() || City.IsNullOrEmpty() || Country.IsNullOrEmpty());

        protected override async void SaveChanges()
        {
            var newAddress = new Adresy()
            {
                Kraj = Country,
                Miejscowosc = City,
                KodPocztowy = PostalCode,
                Ulica = Street,
                NumerBudynku = HouseNr,
                NumerLokalu = Apartment
            };

            DBManager.AddNewItemToDB(newAddress, CallingVm);
        }
    }
}
