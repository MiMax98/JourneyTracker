using JourneyTracker.ViewModels;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JourneyTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackingHistory : ContentPage
    {
        TrackingHistoryViewModel _viewModel;
        public ObservableCollection<string> Items { get; set; }

        public TrackingHistory()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TrackingHistoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
