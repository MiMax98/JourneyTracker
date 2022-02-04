using System;
using Xamarin.Forms.Maps;

namespace JourneyTracker.Services
{
    public class RangeHelper
    {
        private double _maxLatitude = double.MinValue;
        private double _minLatitude = double.MaxValue;
        private double _minLongitude = double.MaxValue;
        private double _maxLongitude = double.MinValue;

        public void Add(Position position)
        {
            if (position.Latitude > _maxLatitude)
            {
                _maxLatitude = position.Latitude;
            }
            if (position.Latitude < _minLatitude)
            {
                _minLatitude = position.Latitude;
            }
            if (position.Longitude > _maxLongitude)
            {
                _maxLongitude = position.Longitude;
            }
            if (position.Longitude < _minLongitude)
            {
                _minLongitude = position.Longitude;
            }
        }

        public Position Center => new Position((_minLatitude + _maxLatitude) / 2, (_minLongitude + _maxLongitude) / 2);

        public MapSpan GetSpan()
        {
            return new MapSpan(Center, Math.Abs(_maxLatitude - _minLatitude) * 1.2, Math.Abs(_maxLongitude - _minLongitude) * 1.2);
        }
    }
}
