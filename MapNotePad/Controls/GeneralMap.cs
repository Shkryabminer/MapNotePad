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
           BindableProperty.Create(propertyName: nameof(CheckPoint), typeof(Pin), typeof(GeneralMap));

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
        }

        #endregion        
    }
}

