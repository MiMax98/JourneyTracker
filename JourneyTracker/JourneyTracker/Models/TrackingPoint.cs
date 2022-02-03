using System;

namespace JourneyTracker.Models
{
    public class TrackingPoint
    {
        public TimeSpan Elapsed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public static TrackingPoint FromLine(string line)
        {
            var elements = line.Split(' ');
            return new TrackingPoint
            {
                Elapsed = TimeSpan.FromTicks(long.Parse(elements[0])),
                Latitude = double.Parse(elements[1]),
                Longitude = double.Parse(elements[2]),
                Distance = double.Parse(elements[3])
            };
        }
    }
}
