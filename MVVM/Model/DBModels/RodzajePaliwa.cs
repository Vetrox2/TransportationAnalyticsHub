using System;
using System.Collections.Generic;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class RodzajePaliwa
{
    public string NazwaPaliwa { get; set; } = null!;

    public virtual ICollection<SamochodyCiezarowe> SamochodyCiezarowes { get; set; } = new List<SamochodyCiezarowe>();
}
