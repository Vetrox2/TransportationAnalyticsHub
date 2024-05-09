using System;
using System.Collections.Generic;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class SamochodyCiezarowe
{
    public int SamochodCiezarowyId { get; set; }

    public string? TypTowaru { get; set; }

    public string RodzajPaliwa { get; set; } = null!;

    public string Rejestracja { get; set; } = null!;

    public double? MaksymalnaObjetoscZaladunkuM3 { get; set; }

    public double MaksymalnaLadownoscT { get; set; }

    public short RokProdukcji { get; set; }

    public virtual ICollection<Przejazdy> Przejazdies { get; set; } = new List<Przejazdy>();

    public virtual RodzajePaliwa RodzajPaliwaNavigation { get; set; } = null!;

    public virtual TypyTowaru? TypTowaruNavigation { get; set; }
}
