using RozliczeniePrzejazdowApp.Core;

namespace TransportationAnalyticsHub.Core
{

    public class Navigation : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModelBase> viewModelFactory;
        private ViewModelBase currentView;
        public ViewModelBase CurrentView
        {
            get => currentView;
            private set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        public Navigation(Func<Type, ViewModelBase> viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
            CurrentView.UpdateSource();
        }
    }
}
