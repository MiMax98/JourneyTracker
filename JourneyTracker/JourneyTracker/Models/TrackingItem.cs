using System;
using System.Collections.Generic;

namespace JourneyTracker.Models
{
    public class TrackingItem
    {
        public DateTime Date { get; set; }
        public double DistanceMeters { get; set; }
        public TimeSpan Time { get; set; }
        public double SpeedMetersPerSecond => DistanceMeters / Time.TotalSeconds;
        public double SpeedKilometersPerHour => DistanceMeters / 1000 / Time.TotalHours;
        public int Steps => (int)(DistanceMeters / 0.762);
        public int BurntKcal => (int)(Steps * 0.04); 
        public List<TrackingPoint> Points { get; set; }
    }
}
