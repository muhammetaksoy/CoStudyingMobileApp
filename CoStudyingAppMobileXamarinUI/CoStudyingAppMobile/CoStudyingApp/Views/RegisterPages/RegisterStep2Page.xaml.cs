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
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views.RegisterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterStep2Page : ContentPage
    {
        List<University> universityList = new List<University>();
        User user = new User();
        ManageServices service = new ManageServices();
        ExploreServices exploreServices = new ExploreServices();
        bool isProceedEnabled = false;
        string status = "";
        bool isProceeded = false;
        public RegisterStep2Page(User _user)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            user = _user;
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
            if (!String.IsNullOrEmpty(entryUsername.Text))
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
                frameProceed.Opacity = 1;
            else
                frameProceed.Opacity = 0.3;
        }

        private async void tapProceed_Tapped(object sender, EventArgs e)
        {
            if (!isProceedEnabled)
                return;
            isProceedEnabled = false;
            ValidationModel resp = RegisterValidation.CheckUsername(entryUsername.Text);
            status = resp.Message;
            if ( resp.isValid )
            {
                await Navigation.PushPopupAsync(new LoadingPage());
                string response = await service.OnRegisterValidateUsernameControl(entryUsername.Text);
                List<University> _universityList = await exploreServices.GetUniversities();
                if (_universityList.Count > 0)
                    universityList = _universityList;
                await Navigation.PopPopupAsync();
                if ( response == "Success")
                {
                    user.Username = entryUsername.Text;
                    isProceeded = true;
                    await Navigation.PushAsync(new RegisterStep2_B(user, universityList), true);
                }
                else if( response == "Invalid")
                {
                    isProceedEnabled = false;
                    ChangeButtonAvailablity();
                    await DisplayAlert("Hata", ValidationMessages.InvalidUsername,"OK");
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

        private void entryUsername_Completed(object sender, EventArgs e)
        {

        }

        private void entryUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }

        
    }
}