using RozliczeniePrzejazdowApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TransportationAnalyticsHub.MVVM.ViewModel;

namespace TransportationAnalyticsHub.Core
{
    public class AddInstanceWindowModelBase<SourceT> : ViewModelBase
    {
        public Window Window;
        public ViewModelBase CallingVm;
        public bool UpdateMode;

        protected const string NameValidationStr = @"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]{0,20}$";
        protected const string IntValidationStr = @"^[0-9]{0,20}$";
        protected const string MoneyValidationStr = @"^[0-9]+([,.][0-9]{0,4})?$";
        protected readonly string[] DateFormats = ["MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy"];
        protected int DriverId;

        public RelayCommand CloseCommand => new RelayCommand(_ => Window.Close());

        public RelayCommand SaveCommand => new RelayCommand(_ =>
        {
            if (ValidateRequiredFields())
            {
                MessageBox.Show("The required data must not be empty!");
                return;
            }

            SaveChanges();
            Window.Close();
        });

        public virtual void FillFields(SourceT source) { }

        protected virtual async void SaveChanges() { }

        protected virtual bool ValidateRequiredFields() => true;
    }
}
