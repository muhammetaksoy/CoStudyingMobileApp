using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.StorageModels;

namespace YeatyAppMobile.Views.MainTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            this.Title = "Profilim";
        }

        private async void tapSignOut_Tapped(object sender, EventArgs e)
        {
            //CurrentUserStorage.ClearStorage();
            //FavoritesStorage.ClearStorage();
            LoginStorage.ResetAllData();
            App.Current.MainPage = new NavigationPage(new OnBoardingPage());
        }
    }
}