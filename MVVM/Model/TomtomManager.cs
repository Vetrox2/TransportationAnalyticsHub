using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace TransportationAnalyticsHub.MVVM.Model
{
    public class TomtomManager
    {
        static string Your_API_Key = "";

        public static async Task<float> GetDistance(List<string> addresses)
        {
            if (Your_API_Key.IsNullOrEmpty())
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "TomTomApiKey.txt");
                if (File.Exists(path))
                    Your_API_Key = File.ReadAllText(path);
                else
                    return 1;
            }

            List<string> Coordinates = new();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    addresses.ForEach(async address =>
                    {
                        var cords = await GetCoordinates(address, client);
                        if (cords != null)
                            Coordinates.Add(cords);
                    });

                    if (Coordinates.Count != addresses.Count)
                        return 1;

                    string coordinatesStr = string.Join(':', Coordinates);
                    string query = $"https://api.tomtom.com/routing/1/calculateRoute/{coordinatesStr}/json?key={Your_API_Key}";
                    var response = await client.GetAsync(query).Result.Content.ReadAsStringAsync();

                    Console.WriteLine(response);
                }
                catch
                {
                    Console.WriteLine("error");
                }
            }

            return 1;
        }

        private static async Task<string?> GetCoordinates(string address, HttpClient client)
        {
            address = Uri.EscapeDataString(address);
            string query = $"https://api.tomtom.com/search/2/geocode/{address}.json?key={Your_API_Key}";

            var responsObject = await GetDeserializedResponse<ResponseModel>(query, client);
            if (responsObject == null)
                return null;

            double maxScore = responsObject.Results.Max(result => result.MatchConfidence.score);
            return responsObject.Results.First(result => result.MatchConfidence.score == maxScore).Position.lat.ToString().Replace(',', '.') + ","
                + responsObject.Results.First(result => result.MatchConfidence.score == maxScore).Position.lon.ToString().Replace(',', '.');
        }

        private static async Task<T?> GetDeserializedResponse<T>(string query, HttpClient client)
        {
            try
            {
                var response = await client.GetAsync(query).Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch(Exception ex)
            {
                return default;
            }
        }
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

    public class ResponseModel
    {
        public Summary Summary { get; set; }
        public List<Result> Results { get; set; }
    }

}

