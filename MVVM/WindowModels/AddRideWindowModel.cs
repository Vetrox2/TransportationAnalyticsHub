using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TransportationAnalyticsHub.Core;
using TransportationAnalyticsHub.MVVM.Model.DBModels;
using TransportationAnalyticsHub.MVVM.ViewModel.AddViewModel;

namespace TransportationAnalyticsHub.MVVM.WindowModels
{
    class AddRideWindowModel : AddInstanceWindowModelBase<Przejazdy>
    {
        private Przejazdy ride;
        private ViewModelBase currentView;
        private AddRideFormViewModel formView;
        private AddRideAddressesViewModel addressView;
        private string saveButtonText;
        private string closeButtonText;
        public AddRideWindowModel()
        {
            ride = new Przejazdy();
            FormView = new AddRideFormViewModel(ref ride);
            AddressView = new AddRideAddressesViewModel();//ride);
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
            FormView.FillFields(this.ride);
            //AddressView.Ride = ride;
        }

        protected override async void SaveChanges() { }

        protected override bool ValidateRequiredFields() => true;

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
            FormView.InsertDataToObject();
            CurrentView = AddressView;

            CloseButtonText = "Back";
            CloseCommand.SwapCommand(_ => GoToFirstView());

            SaveButtonText = "Add";
            SaveCommand.SwapCommand(_ => Save());
        }
    }
}
