using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace MapNotePad.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible, IInitialize, IInitializeAsync
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

        #endregion
    }
}
