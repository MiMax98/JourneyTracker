using JourneyTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace JourneyTracker.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}