namespace TransportationAnalyticsHub.Core
{
    public interface INavigationService
    {
        public ViewModelBase CurrentView { get; }

        public void NavigateTo<T>() where T : ViewModelBase;
    }
}
