using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapNotePad.Views
{
   public class BaseContentPage:ContentPage
    {
        public BaseContentPage()
        {
           
                Prism.Mvvm.ViewModelLocator.SetAutowireViewModel(this, true);
           
        }
    }
}
