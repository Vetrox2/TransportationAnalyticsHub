namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class KosztyPrzejazdow
{
    public int PrzejazdId { get; set; }

    public double? KosztPaliwa { get; set; }

    public double? PensjaKierowcy { get; set; }

    public decimal? DodatkoweKoszty { get; set; }

    public double? LacznyKoszt { get; set; }
}
