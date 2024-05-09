using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationAnalyticsHub.MVVM.Model.TomTomModels
{
    public class DistanceResponseModel
    {
        public string formatVersion { get; set; }
        public List<Route> routes { get; set; }

        public class Leg
        {
            public Summary summary { get; set; }
            public List<Point> points { get; set; }
        }

        public class Point
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Route
        {
            public Summary summary { get; set; }
            public List<Leg> legs { get; set; }
        }

        public class Summary
        {
            public int lengthInMeters { get; set; }
            public int travelTimeInSeconds { get; set; }
            public int trafficDelayInSeconds { get; set; }
            public int trafficLengthInMeters { get; set; }
            public DateTime departureTime { get; set; }
            public DateTime arrivalTime { get; set; }
        }
    }
}
