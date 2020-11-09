using MapNotePad.ViewModels.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible, IInitialize, IInitializeAsync, ICameraSave
    {

       public readonly INavigationService NavigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region --IDestrucible implement--
        public virtual void Destroy()
        {            
        }
        #endregion

        #region --IInitialize implement--
        public virtual void Initialize(INavigationParameters parameters)
        {           
        }
        #endregion

        #region IInitializeAsync implement--
        public virtual async Task InitializeAsync(INavigationParameters parameters)
        {        
            
        }
        #endregion


        #region INavigationAware implementation--
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        public virtual void SaveCameraPosition()
        {
            
        }
        #endregion
    }
}
