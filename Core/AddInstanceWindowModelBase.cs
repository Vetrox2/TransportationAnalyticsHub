﻿using RozliczeniePrzejazdowApp.Core;
using System.Windows;

namespace TransportationAnalyticsHub.Core
{
    public class AddInstanceWindowModelBase<SourceT> : ViewModelBase
    {
        public Window Window;
        public ViewModelBase CallingVm;
        public bool UpdateMode;

        protected const string NameValidationStr = @"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]{0,20}$";
        protected const string IntValidationStr = @"^[0-9]{0,20}$";
        protected const string LatinCharValidationStr = @"^[0-9A-Za-z]{0,20}$";
        protected const string DecimalValidationStr = @"^[0-9]+([,.][0-9]{0,4})?$";
        protected readonly string[] DateFormats = ["MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy"];
        protected int SourceID;

        public RelayCommand CloseCommand => new RelayCommand(_ => Window.Close());

        public RelayCommand SaveCommand => new RelayCommand(_ =>
        {
            if (!ValidateRequiredFields())
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
