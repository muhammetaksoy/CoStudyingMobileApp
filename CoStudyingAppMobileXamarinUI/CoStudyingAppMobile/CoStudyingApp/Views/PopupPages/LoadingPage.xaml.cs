using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YeatyAppMobile.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingPage(bool preventClose = false)
        {
            InitializeComponent();
            if (preventClose)
                this.CloseWhenBackgroundIsClicked = false;
        }
    }
}