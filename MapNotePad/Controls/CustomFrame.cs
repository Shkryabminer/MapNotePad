using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapNotePad.Controls
{
   public class CustomFrame : Frame
    {
        public CustomFrame() //: base ()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
            Padding= new Thickness(0, 1);
            }
            else if(Device.RuntimePlatform==Device.Android)
            {
                Padding = new Thickness(5, -5);
            }
        }
    }
}
