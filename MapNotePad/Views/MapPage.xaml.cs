using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace MapNotePad.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }
        public void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            var map = sender as ClusteredMap;
            var position = e.Point;
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMeters(10000));
            map.MoveToRegion(mapSpan);
        }
    }
}