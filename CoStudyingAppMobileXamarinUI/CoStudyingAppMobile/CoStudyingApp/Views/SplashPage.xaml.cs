using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Views.HomeTabbedPages;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Fill();   
        }

        public async void Fill()
        {
            await Task.Delay(1000);
            await Navigation.PushAsync(new HomeTabbedPage());
        }

        public async Task GoOtherPage()
        {

        }
    }
}