using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Controls
{
    public class GeneralMap : CustomMap
    {
        public GeneralMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
        }

        #region --Public properties--

        public static readonly BindableProperty CheckPointProperty =
                                                BindableProperty.Create(
                                                nameof(CheckPoint),
                                                typeof(Pin),
                                                typeof(GeneralMap));



        public static readonly BindableProperty MapStartCameraPositionProperty =
                                                    BindableProperty.Create(
                                                    nameof(MapStartCameraPosition),
                                                    typeof(CameraPosition),
                                                    typeof(GeneralMap),
                                                    propertyChanged: OnStartPositionChanged);

        public CameraPosition MapStartCameraPosition
        {
            get => (CameraPosition)GetValue(MapStartCameraPositionProperty);
            set => SetValue(MapStartCameraPositionProperty, value);
        }

        public Pin CheckPoint
        {
            get => (Pin)GetValue(CheckPointProperty);
            set => SetValue(CheckPointProperty, value);
        }
        #endregion       

        #region --Overrides--
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(CollectionOfPins))
            {
                SetPins();
            }
            if (propertyName == nameof(CheckPoint))
            {
                MapSpan location = MapSpan.FromCenterAndRadius(CheckPoint.Position,Xamarin.Forms.GoogleMaps.Distance.FromKilometers(50));

                MoveToRegion(location, true);
            }
        }


        #endregion

        #region --Private helpers--

        private CameraUpdate GetCameraUpdate()
        {
            return CameraUpdateFactory.NewPositionZoom
                    (MapStartCameraPosition.Target,
                    MapStartCameraPosition.Zoom);
        }

        private static void OnStartPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition((CameraPosition)newValue);

            var generalMap = (GeneralMap)bindable;

            generalMap.InitialCameraUpdate = cameraUpdate;

            generalMap.MoveCamera(cameraUpdate);
        }

        #endregion
    }
}

