using JourneyTracker.Models;
using JourneyTracker.Services;
using JourneyTracker.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace JourneyTracker.ViewModels
{
    public class TrackingHistoryViewModel : BaseViewModel
    {
        public ObservableCollection<TrackingItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<TrackingItem> ItemTapped { get; }

        private TrackingRepository _repository;

        public TrackingHistoryViewModel()
        {
            Title = "History";
            Items = new ObservableCollection<TrackingItem>();
            _repository = new TrackingRepository();
            LoadItemsCommand = new Command(LoadTrackingData);
            ItemTapped = new Command<TrackingItem>(OnItemSelected);
        }

        private async void OnItemSelected(TrackingItem item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TrackingDetails)}?{nameof(TrackingDetailsViewModel.ItemId)}={item.Date.Ticks}");
        }

        private void LoadTrackingData()
        {
            IsBusy = true;
            Items.Clear();
            foreach (var date in _repository.GetTrackingItems())
            {
                Items.Add(date);
            }
            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
