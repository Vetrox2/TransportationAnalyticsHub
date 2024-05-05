using System;
using System.Collections.Generic;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class TypyTowaru
{
    public string NazwaTypu { get; set; } = null!;

    public virtual ICollection<Przejazdy> Przejazdies { get; set; } = new List<Przejazdy>();

    public virtual ICollection<SamochodyCiezarowe> SamochodyCiezarowes { get; set; } = new List<SamochodyCiezarowe>();
}
