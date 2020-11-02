using MapNotePad.Extensions;
using MapNotePad.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace MapNotePad.Controls
{
    public class GeneralMap : CustomMap
    {
        #region --Public properties--

        public static readonly BindableProperty CheckPointProperty =
                                                BindableProperty.Create(
                                                nameof(CheckPoint),
                                                typeof(Pin),
                                                typeof(GeneralMap));

        public Pin CheckPoint
        {
            get => (Pin)GetValue(CheckPointProperty);
            set => SetValue(CheckPointProperty, value);
        }
        #endregion


        public GeneralMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
        }

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
                MapSpan location = MapSpan.FromCenterAndRadius(CheckPoint.Position, Distance.FromKilometers(10));

                  MoveToRegion(location, true); 
            }
        }


        #endregion

        #region --Private helpers--


        private void SetPins()
        {
            Pins.Clear();
            foreach (PinModel p in CollectionOfPins)
            {
                if (p.Name == null)
                {
                    p.Name = "";
                }
                Pins.Add(p.ToPin());
            }
        }

        #endregion
    }
}

