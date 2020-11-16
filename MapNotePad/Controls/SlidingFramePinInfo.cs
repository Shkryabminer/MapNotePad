using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MapNotePad.Controls
{
    public class SlidingFramePinInfo : Frame
    {
        public SlidingFramePinInfo()
        {
            this.TranslationY=500;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsVisible))
            {
                if (IsVisible == true)
                {
                    this.TranslateTo(0, -100);
                }
                if (!IsVisible)
                {
                    this.TranslateTo(0, 100);
                }
            }
        }
    }
}
