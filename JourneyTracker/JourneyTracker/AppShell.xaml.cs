using JourneyTracker.Views;
using Xamarin.Forms;

namespace JourneyTracker
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TrackingDetails), typeof(TrackingDetails));
        }

    }
}
