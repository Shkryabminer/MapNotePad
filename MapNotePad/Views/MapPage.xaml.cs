using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

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
    }
}