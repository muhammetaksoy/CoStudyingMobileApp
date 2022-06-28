using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.HomeTabbedPages;
using YeatyAppMobile.Views.MainTabbedPages;
using YeatyAppMobile.Views.RegisterPages;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnBoardingPage : ContentPage
    {
        bool isAppeared = false;
        bool isAutoLogin = false;
        ManageServices service = new ManageServices();
        public OnBoardingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
            {
                isAutoLogin = true;
            }
        }

        protected async override void OnAppearing()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Bağlantı Kurulamadı", "Lütfen internet bağlantınızı kontrol edin", "OK");
                return;
            }
            if ( isAutoLogin )
            {
                rotationGrid.IsVisible = true;
                mainGrid.IsVisible = false;
                bool isCorrect = await service.GetToken();
               
                if (isCorrect)
                {
                    LoginModel loginModel = await service.Login();
                    if (loginModel.currentUser != null)
                    {
                        CurrentUserStorage.SetCurrentUser(loginModel.currentUser);
                        App.Current.MainPage = new NavigationPage(new MainTabbedPage());
                    }
                    else
                    {
                        LoginStorage.Username = "";
                        LoginStorage.Password = "";
                        App.Current.MainPage = new NavigationPage(new OnBoardingPage());
                        //await DisplayAlert("Hata", "Lütfen daha sonra tekrar deneyiniz.", "OK");

                    }
                }
                else
                {
                    LoginStorage.Username = "";
                    LoginStorage.Password = "";
                    App.Current.MainPage = new NavigationPage(new OnBoardingPage());
                    //await DisplayAlert("Hata", "Kullanıcı adı veya şifre hatalı", "OK");
                }
            }
            else
            {
                rotationGrid.IsVisible = false;
                mainGrid.IsVisible = true;

            }
           


            base.OnAppearing();
            //frameRegister.Opacity = 1;
            //Logout();


          

            if (!isAppeared)
            {
                isAppeared = true;
                await Task.Delay(100);
                await lblMainHeader.FadeTo(1, 350);
                await Task.Delay(500);
                await lblProp1.FadeTo(1, 350);
                await Task.Delay(500);
                await lblProp2.FadeTo(1, 350);
                await Task.Delay(500);
                await lblProp3.FadeTo(1, 350);
                await Task.Delay(500);
                await lblProp4.FadeTo(1, 350);
                await Task.Delay(500);
                await lblProp5.FadeTo(1, 350);
                await Task.Delay(500);
                await frameStart.FadeTo(1, 350);
            }
        }

        private async void tapStart_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterStep1Page(), true);
        }
    }
}