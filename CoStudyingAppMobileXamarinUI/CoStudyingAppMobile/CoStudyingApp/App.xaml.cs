using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Views;
using YeatyAppMobile.Views.HomeTabbedPages;
using YeatyAppMobile.Views.MainTabbedPages;

namespace YeatyAppMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new OnBoardingPage());
            //MainPage = new NavigationPage(new MainTabbedPage());
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
    }
}
