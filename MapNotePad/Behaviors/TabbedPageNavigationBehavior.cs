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
            var newpage = this.AssociatedObject.CurrentPage;

            if (this.CurrentPage != null)
            {
                var parameters = new NavigationParameters();
                PageUtilities.OnNavigatedFrom(this.CurrentPage, parameters);
                PageUtilities.OnNavigatedTo(newpage, parameters);
            }

            this.CurrentPage = newpage;
        }
        #endregion


    }
}
