using MapNotePad.Extensions;
using MapNotePad.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace MapNotePad.Controls
{
    public class CustomMap : ClusteredMap
    {
        public static readonly BindableProperty CollectionOfPinsProperty =
    BindableProperty.Create(nameof(CollectionOfPins), typeof(List<PinModel>), typeof(CustomMap));

        public List<PinModel> CollectionOfPins
        {
            get { return (List<PinModel>)GetValue(CollectionOfPinsProperty); }
            set { SetValue(CollectionOfPinsProperty, value); }
        }

        public CustomMap()
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
            
        }

        
        #endregion

        #region --Private helpers--

        private static void OnCollectionChanged(BindableObject bindable, object oldValue, object newValue)
        {         
        }

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
