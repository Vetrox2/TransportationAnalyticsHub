using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.Core
{
    public class ComboBoxItemRep
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public ComboBoxItemRep(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public ComboBoxItemRep(Adresy address)
        {
            Id = address.AdresId;
            Text = $"{address.Ulica} {address.NumerBudynku} {address.NumerLokalu}, " +
                    $"{address.KodPocztowy} {address.Miejscowosc}, {address.Kraj}";
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
