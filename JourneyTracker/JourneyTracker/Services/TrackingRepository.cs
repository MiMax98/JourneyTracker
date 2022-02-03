using JourneyTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;

namespace JourneyTracker.Services
{
    public class TrackingRepository
    {
        private readonly string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TrackingData");

        public string CreateFile()
        {
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
            var now = DateTime.Now;
            var name = $"{now.Ticks}.txt";
            var path = Path.Combine(Folder, name);
            File.WriteAllText(path, string.Empty);
            return name;
        }

        public void Append(string name, TimeSpan elapsed, Location location, double distance)
        {
            var path = Path.Combine(Folder, name);
            File.AppendAllLines(path, new string[] { $"{elapsed.Ticks} {location.Latitude} {location.Longitude} {distance:N2}" });
        }

        public List<TrackingItem> GetTrackingItems()
        {
            var files = Directory.GetFiles(Folder, "*.txt");
            return files
                .Select(Path.GetFileNameWithoutExtension)
                .Where(f => f.All(Char.IsDigit))
                .Select(long.Parse)
                .Select(f => new DateTime(f))
                .OrderByDescending(f => f)
                .Select(f => new TrackingItem { Date = f })
                .ToList();
        }

        public TrackingItem GetTrackingItem(string id)
        {
            var path = Path.Combine(Folder, $"{id}.txt");
            var points = File.ReadAllLines(path)
                .Select(TrackingPoint.FromLine)
                .ToList();
            var distance = points.Sum(p => p.Distance);
            return new TrackingItem
            {
                Points = points,
                Date = new DateTime(long.Parse(id)),
                DistanceMeters = distance,
                Time = points.Last().Elapsed
            };
        }

        public string GetLastFile()
        {
            var files = Directory.GetFiles(Folder);
            var name = files.OrderByDescending(f => f).FirstOrDefault();
            if (name == null) return String.Empty;
            var path = Path.Combine(Folder, name);
            return File.ReadAllText(path);
        }
    }
}
