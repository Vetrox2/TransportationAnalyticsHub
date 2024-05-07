namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class CzasPracyKierowcow
{
    public int KierowcaId { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public double? CzasPrzejazduH { get; set; }

    public string Miesiac { get; set; } = null!;
}
