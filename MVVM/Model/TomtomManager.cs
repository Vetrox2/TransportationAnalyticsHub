using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using TransportationAnalyticsHub.MVVM.Model.TomTomModels;

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
                    Your_API_Key = File.ReadAllLines(path)[0];
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
                    var response = await GetDeserializedResponse<DistanceResponseModel>(query, client);

                    if (response != null)
                        return response.routes.Min(x => x.summary.lengthInMeters)/1000;
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

            var responsObject = await GetDeserializedResponse<CoordsResponseModel>(query, client);
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
}

