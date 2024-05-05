using System;
using System.Collections.Generic;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class Adresy
{
    public int AdresId { get; set; }

    public string Kraj { get; set; } = null!;

    public string Miejscowosc { get; set; } = null!;

    public string Ulica { get; set; } = null!;

    public string NumerBudynku { get; set; } = null!;

    public string? NumerLokalu { get; set; }

    public string? KodPocztowy { get; set; }

    public virtual ICollection<Kierowcy> Kierowcies { get; set; } = new List<Kierowcy>();

    public virtual ICollection<Przejazdy> Przejazds { get; set; } = new List<Przejazdy>();
}
