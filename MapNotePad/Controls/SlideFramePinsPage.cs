using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MapNotePad.Controls
{
    public class SlideFramePinsPage : Frame
    {
        public static BindableProperty IsActiveProperty =
            BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(SlideFramePinsPage));

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public SlideFramePinsPage()
        { }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsActive))
            {
                if (IsActive)
                {
                    this.TranslateTo(-100, 0);
                }
                else
                {
                    this.TranslateTo(100, 0);
                }
            }
        }
    }
}
