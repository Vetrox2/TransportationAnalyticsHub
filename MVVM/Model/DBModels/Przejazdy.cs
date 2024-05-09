using System;
using System.Collections.Generic;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class Przejazdy
{
    public int PrzejazdId { get; set; }

    public int KierowcaId { get; set; }

    public int SamochodCiezarowyId { get; set; }

    public string TypTowaru { get; set; } = null!;

    public double LacznaOdlegloscPrzejazduKm { get; set; }

    public double ZuzytePaliwoL { get; set; }

    public decimal CenaPaliwaZlL { get; set; }

    public decimal? DodatkoweKoszty { get; set; }

    public double SrednieSpalanieNa100km { get; set; }

    public DateTime DataRozpoczeciaPrzejazdu { get; set; }

    public DateTime DataZakonczeniaPrzejazdu { get; set; }

    public decimal StawkaGodzinowaBruttoKierowcy { get; set; }

    public double? WspolczynnikObjetosci { get; set; }

    public double WspolczynnikLadownosci { get; set; }

    public virtual Kierowcy Kierowca { get; set; } = null!;

    public virtual ICollection<PunktyTrasy> PunktyTrasies { get; set; } = new List<PunktyTrasy>();

    public virtual SamochodyCiezarowe SamochodCiezarowy { get; set; } = null!;

    public virtual TypyTowaru TypTowaruNavigation { get; set; } = null!;
}
