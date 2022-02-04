using JourneyTracker.Models;
using JourneyTracker.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace JourneyTracker.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class TrackingDetailsViewModel : BaseViewModel
    {
        private readonly TrackingRepository _repository;
        private string _itemId;
        private TrackingItem _item;
        private Map _map;
        public TrackingDetailsViewModel()
        {
            Title = "Details";
            _repository = new TrackingRepository();
        }
        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }

        public TrackingItem Item { get => _item; set => SetProperty(ref _item, value); }

        public Map Map { get => _map; set => SetProperty(ref _map, value); }

        private void LoadItemId(string id)
        {
            Item = _repository.GetTrackingItem(id);
            var line = new Polyline()
            {
                StrokeWidth = 8,
                StrokeColor = Color.FromHex("#1BA1E2"),
            };
            var range = new RangeHelper();
            foreach (var point in Item.Points)
            {
                var position = new Position(point.Latitude, point.Longitude);
                range.Add(position);
                line.Geopath.Add(position);
            }
            Map = new Map(range.GetSpan());
            Map.MapElements.Add(line);
        }
    }
}
