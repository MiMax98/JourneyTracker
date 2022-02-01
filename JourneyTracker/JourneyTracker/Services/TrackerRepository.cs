using System;
using System.IO;
using System.Linq;
using Xamarin.Essentials;

namespace JourneyTracker.Services
{
    public class TrackerRepository
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
