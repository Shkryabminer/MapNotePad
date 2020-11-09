using CoreGraphics;
using MapNotePad.Controls;
using MapNotePad.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CommonEntry), typeof(CommonEntryRendererIOS))]
namespace MapNotePad.iOS
{
    class CommonEntryRendererIOS : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Layer.CornerRadius = 0;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.BorderColor = Color.Transparent.ToCGColor();
                Control.Layer.BackgroundColor = Color.Transparent.ToCGColor();
                // Control.RemoveBackgroundLayer();
                //Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));

                //Control.LeftViewMode = UITextFieldViewMode.Always;
            }
        }

    }
}