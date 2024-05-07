using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.Core
{
    public class RaportViewModelBase<SourceT> : ViewModelBase
    {
        private List<SourceT> source;

        public List<SourceT> Source
        {
            get => source;
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }
    }
}
