using RozliczeniePrzejazdowApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationAnalyticsHub.Core
{
    public class ViewModelBase : ObservableObject
    {
        public virtual void UpdateSource() { }
    }
}
