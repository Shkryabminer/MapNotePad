using MapNotePad.Extensions;
using MapNotePad.ViewModels;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace MapNotePad.Controls
{
    public class CustomMap : ClusteredMap
    {
        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
        }

        #region --public properties--

        public static readonly BindableProperty CollectionOfPinsProperty =
            BindableProperty.Create(nameof(CollectionOfPins), typeof(List<PinModelViewModel>), typeof(CustomMap));

        public List<PinModelViewModel> CollectionOfPins
        {
            get => (List<PinModelViewModel>)GetValue(CollectionOfPinsProperty);
            set => SetValue(CollectionOfPinsProperty, value);
        }
        #endregion

        public static readonly BindableProperty MapStartCameraPositionProperty =
           BindableProperty.Create(nameof(MapStartCameraPosition), typeof(CameraPosition), typeof(CustomMap), propertyChanged: OnStartPositionChanged);

        public CameraPosition MapStartCameraPosition
        {
            get => (CameraPosition)GetValue(MapStartCameraPositionProperty);
            set => SetValue(MapStartCameraPositionProperty, value);
        }

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

        protected virtual void SetPins()
        {
            Pins.Clear();

            foreach (PinModelViewModel p in CollectionOfPins)
            {
                if (p.Name == null)
                {
                    p.Name = "";
                }

                Pins.Add(p.ToPin());
            }
        }

        private static void OnStartPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition((CameraPosition)newValue);

            var customlMap = (CustomMap)bindable;

            customlMap.InitialCameraUpdate = cameraUpdate;

            customlMap.MoveCamera(cameraUpdate);
        }

        #endregion
    }

}
