using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotePad.Views
{
    public partial class PinsListPage : ContentPage
    {
        public PinsListPage()
        {

            Prism.Mvvm.ViewModelLocator.SetAutowireViewModel(this, true);
            InitializeComponent();
        }
    }
}