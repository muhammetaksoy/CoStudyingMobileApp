//using Entities;
using CoStudyApp.Models.DBModels;
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

namespace YeatyAppMobile.Views.RegisterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterStep3Page : ContentPage
    {
        User user = new User();
        ManageServices service = new ManageServices();
        bool isProceedEnabled = false;
        string status = "";
        bool isCheckedPrivacy = false;
        bool isProceeded = false;
        
        public RegisterStep3Page(User _user, bool _isGoogleLogin = false, bool _isFacebookLogin = false)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            user = _user;
            //user.isGoogleLogin = _isGoogleLogin;
            //user.isFacebookLogin = _isFacebookLogin;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (isProceeded)
            {
                isProceedEnabled = true;
            }
        }


        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public void EntryCheckPoint()
        {
            if (!String.IsNullOrEmpty(entryEmail.Text) && !String.IsNullOrEmpty(entryPassword.Text) && !String.IsNullOrEmpty(entryRePassword.Text) && isCheckedPrivacy)
            {
                isProceedEnabled = true;
            }
            else { isProceedEnabled = false; }               
            ChangeButtonAvailablity();

        }

        public void ChangeButtonAvailablity()
        {
            if (isProceedEnabled)
                frameProceed.Opacity = 1;
            else
                frameProceed.Opacity = 0.3;
        }

        private void entryEmail_Completed(object sender, EventArgs e)
        {
            entryPassword.Focus();
        }

        private void entryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }

        private async void tapProceed_Tapped(object sender, EventArgs e)
        {
            if (!isProceedEnabled)
                return;
            isProceedEnabled = false;
            ValidationModel resp = RegisterValidation.CheckEmail(entryEmail.Text);
            status = resp.Message;
            if (resp.isValid)
            {
                ValidationModel resp2 = RegisterValidation.CheckPassword(entryPassword.Text,entryRePassword.Text);
                status = resp2.Message;
                if( resp2.isValid )
                {
                    await Navigation.PushPopupAsync(new LoadingPage());
                    string response = await service.OnRegisterValidateEmailControl(entryEmail.Text);
                    await Navigation.PopPopupAsync();
                    if (response == "Success")
                    {
                        user.Email = entryEmail.Text;
                        user.Password = entryPassword.Text;
                        await Navigation.PushPopupAsync(new LoadingPage());
                        RegisterResponseModel respp = await service.BasicRegister(user);
                        //RegisterResponseModel respp = new RegisterResponseModel();
                        if ( respp.isSuccess )
                        {
                            LoginStorage.Username = respp.User.Username;
                            LoginStorage.Password = user.Password;
                            var isTokenNotEmpty = await service.GetToken();
                            if (isTokenNotEmpty)
                            {
                                LoginModel loginModel = await service.Login();
                                if (loginModel != null)
                                {

                                    CurrentUserStorage.SetCurrentUser(loginModel.currentUser);
                                    App.Current.MainPage = new NavigationPage(new MainTabbedPage());
                                    //if (!String.IsNullOrEmpty(LoginStorage.isOnBoardingFinished))
                                    //{
                                    //    IsLoading = false;

                                    //    App.NavPage = new NavigationPage(new HomeTabbedPages()) { BarBackgroundColor = Color.White, BarTextColor = Color.Black };
                                    //    Application.Current.MainPage = App.NavPage;
                                    //}
                                    //else
                                    //{
                                    //    IsLoading = false;
                                    //    App.Current.MainPage = new OnBoardingPage();
                                    //}
                                }
                                else
                                {
                                    LoginStorage.ResetAllData();
                                    await Navigation.PopAllPopupAsync();
                                    isProceedEnabled = true;
                                    await DisplayAlert("HATA", "Hata gerçekleşti, Tekrar Deneyiniz.", "TAMAM");
                                }
                            }
                            else
                            {
                                LoginStorage.ResetAllData();
                                await Navigation.PopAllPopupAsync();
                                isProceedEnabled = true;
                                await DisplayAlert("HATA", "Hata gerçekleşti, Tekrar Deneyiniz.", "TAMAM");
                            }
                        }



                    }                 
                    else if (response == "Invalid")
                    {
                        isProceedEnabled = false;
                        ChangeButtonAvailablity();
                        await DisplayAlert("Hata", ValidationMessages.InvalidEmail, "OK");
                    }
                    else if (response == "Taken")
                    {
                        isProceedEnabled = false;
                        ChangeButtonAvailablity();
                        await DisplayAlert("Hata", ValidationMessages.TakenUsername, "OK");
                    }
                    else
                    {
                        isProceedEnabled = false;
                        ChangeButtonAvailablity();
                        await DisplayAlert("Hata", ValidationMessages.DefaultMessage, "OK");
                    }
                }
                else
                {
                    isProceedEnabled = false;
                    ChangeButtonAvailablity();
                    await DisplayAlert("Hata", status, "OK");
                }
                
            }
            else
            {
                isProceedEnabled = false;
                ChangeButtonAvailablity();
                await DisplayAlert("Hata", status, "OK");
            }
        }

        private void spanPrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            if (isCheckedPrivacy)
                isCheckedPrivacy = false;
            else
                isCheckedPrivacy = true;
            ChangeButtonAvailablity();
        }

        private void entryPassword_Completed(object sender, EventArgs e)
        {
            entryRePassword.Focus();
        }

        private void entryPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }

        private void entryRePassword_Completed(object sender, EventArgs e)
        {

        }

        private void entryRePassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }

        private void privacyPolicy_Tapped(object sender, EventArgs e)
        {

        }

        private void checkPrivacy_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            isCheckedPrivacy = checkPrivacy.IsChecked;
            EntryCheckPoint();
        }
    }
}