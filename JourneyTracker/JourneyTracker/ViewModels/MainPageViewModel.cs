using JourneyTracker.Models;
using JourneyTracker.Services;
using JourneyTracker.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace JourneyTracker.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Title = "Tracking";
            State = TrackerState.Stopped;
            ButtonCommand = new Command(ButtonClicked);
            SetupMap();
            _repository = new TrackingRepository();
        }
        private Stopwatch _stopwatch;
        private Location _lastLocation;
        private Map _map;
        private string _fileName;
        
        public Map Map { get => _map; set => SetProperty(ref _map, value); }
        public string ButtonLabel
        {
            get => _state == TrackerState.Stopped ? "Start" : "Stop";
        }
        public string ButtonColor 
        {
            get => _state == TrackerState.Stopped ? "#23B428" : "#ED3131";
        }

        private TrackerState _state;
        private readonly TrackingRepository _repository;

        public TrackerState State
        {
            get => _state; set
            {
                _state = value;
                OnPropertyChanged(nameof(ButtonLabel));
                OnPropertyChanged(nameof(ButtonColor));
            }
        }

        public ICommand ButtonCommand { get; }

        private void ButtonClicked()
        {
            if (State == TrackerState.Stopped)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private async void SetupMap()
        {
            var location = await GetCurrentLocation();
            var span = new MapSpan(new Position(location.Latitude, location.Longitude), 0.01, 0.01);
            Map = new Map(span)
            {
                MapType = MapType.Street,
                IsShowingUser = true,
            };
        }

        private async Task<Location> GetCurrentLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            return await Geolocation.GetLocationAsync(request);
        }

        private void Start()
        {
            State = TrackerState.Started;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _fileName = _repository.CreateFile();
            Device.StartTimer(new System.TimeSpan(0, 0, 5), () =>
              {
                  PushLocation();
                  return State == TrackerState.Started;
              });
        }

        private async void PushLocation()
        {
            var location = await GetCurrentLocation();
            var distance = Location.CalculateDistance(_lastLocation ?? location, location, DistanceUnits.Kilometers) * 1000;
            _lastLocation = location;
            _repository.Append(_fileName, _stopwatch.Elapsed, location, distance);
        }

        private async void Stop()
        {
            State = TrackerState.Stopped;
            _stopwatch.Stop();
            _lastLocation = null;
            await Shell.Current.GoToAsync($"{nameof(TrackingDetails)}?{nameof(TrackingDetailsViewModel.ItemId)}={_fileName}");
        }
    }
}
