using System;
using System.Collections.Generic;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class RaportPracySamochodow
{
    public int SamochodCiezarowyId { get; set; }

    public string Rejestracja { get; set; } = null!;

    public double? CzasPrzejazduH { get; set; }

    public double? SrednieSpalanie100km { get; set; }

    public double? SredniZaladunek { get; set; }

    public double? LacznaOdleglosc { get; set; }

    public string Miesiac { get; set; } = null!;
}
