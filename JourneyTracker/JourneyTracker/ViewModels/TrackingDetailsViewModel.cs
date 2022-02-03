using JourneyTracker.Models;
using JourneyTracker.Services;
using Xamarin.Forms;

namespace JourneyTracker.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class TrackingDetailsViewModel : BaseViewModel
    {
        private readonly TrackingRepository _repository;
        private string _itemId;
        private TrackingItem _item;
        public TrackingDetailsViewModel()
        {
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

        private void LoadItemId(string id)
        {
            Item = _repository.GetTrackingItem(id);
        }
    }
}
