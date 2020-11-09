using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;
using Prism.Navigation;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using MapNotePad.ViewModels.Interfaces;

namespace MapNotePad.Views
{
     public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is ICameraSave bindable)
            {
                 bindable.SaveCameraPosition();
            }
        }


    }
}