using RozliczeniePrzejazdowApp.Core;
using System.Windows;

namespace TransportationAnalyticsHub.Core
{
    public class AddInstanceWindowModelBase<SourceT> : ViewModelBase
    {
        public Window Window;
        public ViewModelBase CallingVm;
        public bool UpdateMode;

        protected SourceT UpdatingObject;

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
