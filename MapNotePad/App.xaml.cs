using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Ioc;
using Prism;

namespace MapNotePad
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
                      
        }
        public App(IPlatformInitializer platformInitializer =null) : base(platformInitializer)
        { 
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }

        protected override void OnInitialized()
        {
            throw new NotImplementedException();
        }
    }
}
