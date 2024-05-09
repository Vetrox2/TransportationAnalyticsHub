using System;
using System.Collections.Generic;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class PunktyTrasy
{
    public int PunktTrasyId { get; set; }

    public int PrzejazdId { get; set; }

    public int AdresId { get; set; }

    public virtual Adresy Adres { get; set; } = null!;

    public virtual Przejazdy Przejazd { get; set; } = null!;
}
