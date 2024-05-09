using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.ViewModel.AddViewModel;

namespace TransportationAnalyticsHub.MVVM.WindowModels
{
    class AddRideWindowModel : AddInstanceWindowModelBase<Przejazdy>
    {
        private Przejazdy ride;
        private List<PunktyTrasy> ridePoints;
        private ViewModelBase currentView;
        private AddRideFormViewModel formView;
        private AddRideAddressesViewModel addressView;
        private string saveButtonText;
        private string closeButtonText;

        public AddRideWindowModel()
        {
            ride = new Przejazdy();
            ridePoints = new();
            FormView = new AddRideFormViewModel(ref ride);
            AddressView = new AddRideAddressesViewModel(ref ride, ridePoints);
            GoToFirstView();
        }

        public ViewModelBase CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }
        public AddRideFormViewModel FormView
        {
            get => formView;
            set
            {
                formView = value;
                OnPropertyChanged();
            }
        }
        public AddRideAddressesViewModel AddressView
        {
            get => addressView;
            set
            {
                addressView = value;
                OnPropertyChanged();
            }
        }

        public string SaveButtonText
        {
            get => saveButtonText;
            set
            {
                saveButtonText = value;
                OnPropertyChanged();
            }
        }

        public string CloseButtonText
        {
            get => closeButtonText;
            set
            {
                closeButtonText = value;
                OnPropertyChanged();
            }
        }

        public override void FillFields(Przejazdy ride)
        {
            this.ride = ride;
            ridePoints = ride.PunktyTrasies.ToList();
            FormView.FillFields(this.ride);
            AddressView.FillFields(this.ride, ridePoints);
        }

        protected override async void SaveChanges() 
        {
            var addresses = new List<string>();
            ridePoints.ForEach(point => addresses.Add(DataConverter.ConvertAddressToString(point.Adres)));
            ride.LacznaOdlegloscPrzejazduKm = await TomtomManager.GetDistance(addresses);

            if(UpdateMode)
                DBManager.UpdateItemInDB(ride, CallingVm);
            else
                DBManager.AddNewItemToDB(ride, CallingVm);

            DBManager.ReplaceRidePointsInDB(ridePoints, CallingVm);
        }

        protected override bool ValidateRequiredFields() => AddressView.InsertDataToObject();

        private void GoToFirstView()
        {
            CurrentView = FormView;

            CloseButtonText = "Cancel";
            CloseCommand.SwapCommand(_ => CloseWindow());

            SaveButtonText = "Next";
            SaveCommand.SwapCommand(_ => GoToSecondView());
        }

        private void GoToSecondView()
        {
            if (!FormView.InsertDataToObject())
            {
                MessageBox.Show("Fill required fields");
                return;
            }


            CurrentView = AddressView;
            AddressView.UpdateSource();

            CloseButtonText = "Back";
            CloseCommand.SwapCommand(_ => GoToFirstView());

            SaveButtonText = "Add";
            SaveCommand.SwapCommand(_ => Save());
        }
    }
}
