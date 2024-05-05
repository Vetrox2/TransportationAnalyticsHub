using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationAnalyticsHub.Core
{
    public interface INavigationService
    {
        public ViewModelBase CurrentView { get; }

        public void NavigateTo<T>() where T : ViewModelBase;
    }
}
