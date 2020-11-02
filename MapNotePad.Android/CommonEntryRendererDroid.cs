using Android.Content;
using Android.Graphics.Drawables;
using MapNotePad.Controls;
using MapNotePad.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CommonEntry), typeof(CommonEntryRendererDroid))]
namespace MapNotePad.Droid
{
    public class CommonEntryRendererDroid : EntryRenderer
    {
        public CommonEntryRendererDroid(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {               
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(60f);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.Blue);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight,
                    Control.PaddingBottom);
            }
        }
    }
}