namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class Kierowcy
{
    public int KierowcaId { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string? Pesel { get; set; }

    public decimal StawkaGodzinowaBrutto { get; set; }

    public DateOnly DataUrodzenia { get; set; }

    public int AdresId { get; set; }

    public virtual Adresy Adres { get; set; } = null!;

    public virtual ICollection<Przejazdy> Przejazdies { get; set; } = new List<Przejazdy>();
}
