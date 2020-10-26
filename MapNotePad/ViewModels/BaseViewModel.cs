using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {

       public readonly INavigationService NavigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
