using Microsoft.EntityFrameworkCore.Metadata;
using System.Windows;

namespace TransportationAnalyticsHub.Core
{
    public class AddInstanceWindowModelBase<SourceT> : ViewModelBase
    {
        public Window Window;
        public ViewModelBase CallingVm;
        public bool UpdateMode;

        protected SourceT UpdatingObject;

        public AddInstanceWindowModelBase()
        {
            CloseCommand = new RelayCommand(_ => CloseWindow());
            SaveCommand = new RelayCommand(_ => Save());
        }

        public RelayCommand CloseCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }

        public virtual void FillFields(SourceT source) { }

        protected virtual async void SaveChanges() { }

        protected virtual bool ValidateRequiredFields() => true;

        protected void CloseWindow() => Window.Close();
        protected void Save()
        {
            if (!ValidateRequiredFields())
            {
                MessageBox.Show("The required data must not be empty!");
                return;
            }

            SaveChanges();
            Window.Close();
        }
    }
}
