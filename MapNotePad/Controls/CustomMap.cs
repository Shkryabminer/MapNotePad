using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
                               BindableProperty.Create(nameof(CollectionOfPins),
                                                       typeof(List<PinModelViewModel>), 
                                                       typeof(CustomMap));
              

        public List<PinModelViewModel> CollectionOfPins
        {
            get => (List<PinModelViewModel>)GetValue(CollectionOfPinsProperty);
            set => SetValue(CollectionOfPinsProperty, value);
        }
        #endregion

       
        #region --Overrides--
       
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CollectionOfPins))
            {
                SetPins();
             //   RaisePropertyChanged(nameof(Pins));
            }
        }
        protected override void OnParentSet()
        {
            base.OnParentSet();
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

        private MapSpan GetLocation()
        {
            var center = new Position(CollectionOfPins.First().Latitude, CollectionOfPins.First().Longtitude);

            return MapSpan.FromCenterAndRadius(center, Distance.FromKilometers(10));
        }      

        #endregion
    }

}
