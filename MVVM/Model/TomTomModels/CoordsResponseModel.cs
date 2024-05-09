using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationAnalyticsHub.MVVM.Model.TomTomModels
{
    public class CoordsResponseModel
    {
        public Summary Summary { get; set; }
        public List<Result> Results { get; set; }
    }

    public class AddressRanges
    {
        public string rangeLeft { get; set; }
        public string rangeRight { get; set; }
        public From from { get; set; }
        public To to { get; set; }
    }

    public class BtmRightPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class DataSources
    {
        public Geometry geometry { get; set; }
    }

    public class EntryPoint
    {
        public string type { get; set; }
        public Position position { get; set; }
    }

    public class From
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Geometry
    {
        public string id { get; set; }
    }

    public class Mapcode
    {
        public string type { get; set; }
        public string fullMapcode { get; set; }
        public string territory { get; set; }
        public string code { get; set; }
    }

    public class MatchConfidence
    {
        public double score { get; set; }
    }

    public class Position
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Root
    {
        public string type { get; set; }
        public string id { get; set; }
        public double score { get; set; }
        public MatchConfidence matchConfidence { get; set; }
        public double dist { get; set; }
        public Address address { get; set; }
        public Position position { get; set; }
        public List<Mapcode> mapcodes { get; set; }
        public Viewport viewport { get; set; }
        public List<EntryPoint> entryPoints { get; set; }
        public AddressRanges addressRanges { get; set; }
        public DataSources dataSources { get; set; }
    }

    public class To
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class TopLeftPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Viewport
    {
        public TopLeftPoint topLeftPoint { get; set; }
        public BtmRightPoint btmRightPoint { get; set; }
    }


    public class Result
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public double Score { get; set; }
        public MatchConfidence MatchConfidence { get; set; }
        public Address Address { get; set; }
        public Position Position { get; set; }
        public Viewport Viewport { get; set; }
        public List<EntryPoint> EntryPoints { get; set; }
    }
    public class Summary
    {
        public string Query { get; set; }
        public string QueryType { get; set; }
        public int QueryTime { get; set; }
        public int NumResults { get; set; }
        public int Offset { get; set; }
        public int TotalResults { get; set; }
        public int FuzzyLevel { get; set; }
    }

    public class Address
    {
        public string streetNumber { get; set; }
        public string streetName { get; set; }
        public string municipalitySubdivision { get; set; }
        public string municipality { get; set; }
        public string countrySecondarySubdivision { get; set; }
        public string countryTertiarySubdivision { get; set; }
        public string countrySubdivision { get; set; }
        public string postalCode { get; set; }
        public string extendedPostalCode { get; set; }
        public string countryCode { get; set; }
        public string country { get; set; }
        public string countryCodeISO3 { get; set; }
        public string freeformAddress { get; set; }
        public string countrySubdivisionName { get; set; }
        public string localName { get; set; }
    }
}
