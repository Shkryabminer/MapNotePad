using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MapNotePad.Views
{
    
    public class BaseContentPage:ContentPage
    {
        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Prism.Mvvm.ViewModelLocator.SetAutowireViewModel(this, true);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IDestructible bindable)
            {
               // bindable.Destroy();
            }
        }
    }
}
