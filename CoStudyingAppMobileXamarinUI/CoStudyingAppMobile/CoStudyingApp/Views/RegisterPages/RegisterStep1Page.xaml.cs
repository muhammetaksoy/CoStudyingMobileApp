
using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YeatyAppMobile.Views.RegisterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterStep1Page : ContentPage
    {
        bool isProceedEnabled = false;
        User user = new User();
        bool isAppeared = false;
        bool isProceeded = false;
        


        public RegisterStep1Page()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if ( isProceeded )
            {
                isProceedEnabled = true;
            }
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void lblName_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }

        private void lblSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryCheckPoint();
        }


        public void EntryCheckPoint()
        {
            if (!String.IsNullOrEmpty(entryName.Text) && !String.IsNullOrEmpty(entrySurname.Text))
                isProceedEnabled = true;
            else
                isProceedEnabled = false;
            ChangeButtonAvailablity();

        }

        public void ChangeButtonAvailablity()
        {
            if(isProceedEnabled)
                frameProceed.Opacity = 1;
            else
                frameProceed.Opacity = 0.3;
        }

        private async void tapProceed_Tapped(object sender, EventArgs e)
        {

            if (isProceedEnabled)
            {
                isProceedEnabled = false;
                user.Name = entryName.Text;
                user.Surname = entrySurname.Text;
                isProceeded = true;
                await Navigation.PushAsync(new RegisterStep2Page(user), true);
            }
                
           
        }

        private void entryName_Completed(object sender, EventArgs e)
        {
            entrySurname.Focus();

        }


        private async void tapLogin_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}