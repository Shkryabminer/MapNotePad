using Prism.Behaviors;
using Prism.Common;
using System;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotePad.Behaviors
{
    public class TabbedPageNavigationBehavior : BehaviorBase<TabbedPage>

    {
        private Page CurrentPage { get; set; }

        #region --Overrides--

        protected override void OnAttachedTo(TabbedPage bindable)
        {
            bindable.CurrentPageChanged += this.OnCurrentPageChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            bindable.CurrentPageChanged -= this.OnCurrentPageChanged;
            base.OnDetachingFrom(bindable);
        }
        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            var newpage = AssociatedObject.CurrentPage;

            if (CurrentPage != null)
            {
                var parameters = new NavigationParameters();
                PageUtilities.OnNavigatedFrom(CurrentPage, parameters);
                PageUtilities.OnNavigatedTo(newpage, parameters);
            }

            CurrentPage = newpage;
        }
        #endregion


    }
}
