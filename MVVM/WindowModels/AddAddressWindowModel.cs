using Microsoft.IdentityModel.Tokens;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.WindowModel
{
    class AddAddressWindowModel : AddInstanceWindowModelBase<Adresy>
    {
        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnPropertyChanged();
            }
        }

        private string houseNr;
        public string HouseNr
        {
            get => houseNr;
            set
            {
                houseNr = value;
                OnPropertyChanged();
            }
        }

        private string apartment;
        public string Apartment
        {
            get => apartment;
            set
            {
                apartment = value;
                OnPropertyChanged();
            }
        }

        private string postalCode;
        public string PostalCode
        {
            get => postalCode;
            set
            {
                postalCode = value;
                OnPropertyChanged();
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }

        private string country;
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
            using (var context = new RozliczeniePrzejazdowSamochodowCiezarowychContext())
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

                context.Add<Adresy>(newAddress);
                context.SaveChanges();
                CallingVm.UpdateSource();
            }
        }
    }
}
