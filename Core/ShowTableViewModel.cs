using System.Windows;
using TransportationAnalyticsHub.MVVM.Model;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.Core
{
    public class ShowTableViewModel<SourceT, WindowT, WindowVmT> : ViewModelBase
        where WindowT : Window, new()
        where WindowVmT : AddInstanceWindowModelBase<SourceT>, new()
        where SourceT : class
    {

        private object selectedItem;
        private List<SourceT> source;

        public RelayCommand Add => new RelayCommand(_ =>
        {
            var addWindow = new WindowT();
            addWindow.DataContext = new WindowVmT() { Window = addWindow, CallingVm = this };
            addWindow.ShowDialog();
        });

        public RelayCommand Update => new RelayCommand(_ =>
        {
            if (selectedItem == null)
                return;

            var addWindow = new WindowT();
            var vm = new WindowVmT() { Window = addWindow, CallingVm = this, UpdateMode = true };
            vm.FillFields((SourceT)selectedItem);
            addWindow.DataContext = vm;
            addWindow.ShowDialog();
        });

        public RelayCommand Delete => new RelayCommand(_ =>
        {
            if (selectedItem != null && MessageBox.Show("Are you sure you want to delete this row?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                DBManager.DeleteItemFromDB((SourceT)selectedItem, this);
        });

        public List<SourceT> Source
        {
            get => source;
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }

        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }
    }
}
