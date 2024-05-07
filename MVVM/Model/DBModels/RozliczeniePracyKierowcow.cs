namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class RozliczeniePracyKierowcow
{
    public int KierowcaId { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public double? WynagrodzenieZl { get; set; }

    public string Miesiac { get; set; } = null!;
}
