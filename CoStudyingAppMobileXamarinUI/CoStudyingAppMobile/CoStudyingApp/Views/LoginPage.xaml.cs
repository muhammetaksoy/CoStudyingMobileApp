using Entities;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.BusinessModels.ValidationModels;
using YeatyAppMobile.Models;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.HomeTabbedPages;
using YeatyAppMobile.Views.MainTabbedPages;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        bool isProceedEnabled = false;
        string status = "";
        ManageServices service = new ManageServices();
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }

        protected override void OnAppearing()
        {
           
            base.OnAppearing();
        }

        private void entryUsername_Completed(object sender, EventArgs e)
        {
            entryPassword.Focus();
        }

        private void entryUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }

      
        private void entryPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }

        private async void tapForgotPassword_Tapped(object sender, EventArgs e)
        {

        }

        public void EntryCheckPoint()
        {
            if (!String.IsNullOrEmpty(entryUsername.Text) && !String.IsNullOrEmpty(entryPassword.Text))
            {
                isProceedEnabled = true;
            }
            else
                isProceedEnabled = false;
            ChangeButtonAvailablity();

        }

        public void ChangeButtonAvailablity()
        {
            if (isProceedEnabled)
                frameLogin.Opacity = 1;
            else
                frameLogin.Opacity = 0.3;
        }


        private async void tapProceed_Tapped(object sender, EventArgs e)
        {
            if (!isProceedEnabled)
                return;
            isProceedEnabled = false;
            await Navigation.PushPopupAsync(new LoadingPage());
            LoginStorage.Username = entryUsername.Text;
            LoginStorage.Password = entryPassword.Text;

            bool isCorrect = await service.GetToken();

            if( isCorrect)
            {
                LoginModel loginModel = await service.Login();
                if( loginModel.currentUser != null)
                {
                    App.Current.MainPage = new NavigationPage(new MainTabbedPage());
                    await Navigation.PopPopupAsync();
                }
                else
                {
                    LoginStorage.Username = "";
                    LoginStorage.Password = "";
                    await Navigation.PopPopupAsync();
                    isProceedEnabled = true;
                    await DisplayAlert("Hata", "Lütfen daha sonra tekrar deneyiniz.", "OK");
                }
            }
            else
            {
                LoginStorage.Username = "";
                LoginStorage.Password = "";
                isProceedEnabled = true;
                await Navigation.PopPopupAsync();
                await DisplayAlert("Hata", "Kullanıcı adı veya şifre hatalı", "OK");
            }

           
            

            //ValidationModel resp = RegisterValidation.CheckUsername(entryUsername.Text);
            //status = resp.Message;
            //if (resp.isValid)
            //{
            //    await Navigation.PushPopupAsync(new LoadingPage());
            //    string response = await service.OnRegisterValidateUsernameControl(entryUsername.Text);
            //    await Navigation.PopPopupAsync();
            //    if (response == "Success")
            //    {
            //        user.Username = entryUsername.Text;
            //        isProceeded = true;
            //        await Navigation.PushAsync(new RegisterStep3Page(user), true);
            //    }
            //    else if (response == "Invalid")
            //    {
            //        isProceedEnabled = false;
            //        ChangeButtonAvailablity();
            //        await DisplayAlert("Hata", ValidationMessages.InvalidUsername, "OK");
            //    }
            //    else if (response == "Taken")
            //    {
            //        isProceedEnabled = false;
            //        ChangeButtonAvailablity();
            //        await DisplayAlert("Hata", ValidationMessages.TakenUsername, "OK");
            //    }
            //    else
            //    {
            //        isProceedEnabled = false;
            //        ChangeButtonAvailablity();
            //        await DisplayAlert("Hata", ValidationMessages.DefaultMessage, "OK");
            //    }
            //}
            //else
            //{
            //    isProceedEnabled = false;
            //    ChangeButtonAvailablity();
            //    await DisplayAlert("Hata", status, "OK");
            //}
        }



        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();
        }
    }
}