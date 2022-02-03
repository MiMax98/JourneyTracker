using JourneyTracker.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JourneyTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackingDetails : ContentPage
    {
        public TrackingDetails()
        {
            InitializeComponent();
            BindingContext = new TrackingDetailsViewModel();
        }
    }
}