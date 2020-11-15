using System;
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
                                                propertyName: nameof(CheckPoint),
                                                typeof(Pin),
                                                typeof(GeneralMap));
                                               

        public Pin CheckPoint
        {
            get => (Pin)GetValue(CheckPointProperty);
            set => SetValue(CheckPointProperty, value);
        }

        //private static void OnSelectedPinChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    if (newValue != null && bindable is GeneralMap map)
        //    {
        //        MapSpan location = MapSpan.FromCenterAndRadius(map.CheckPoint.Position, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(50));

        //        map.MoveToRegion(location, true);
        //    }
        //}

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
        #endregion       

        #region --Overrides--
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(CollectionOfPins))
            {
                SetPins();
            }           
        }

        #endregion

        #region --Private helpers--
               

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

