using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.StorageModels;

namespace YeatyAppMobile.Views.HomeTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        bool isProcessEnabled = true;
        public ProfilePage()
        {
            InitializeComponent();
            this.Title = "Profil";
            imgProfilePicture.Source = "randomPerson.jpg";
            lblName.Text = "Ulaş";
            lblShowProfile.Text = "Profili gör";
        }

        private async void tapSignOut_Tapped(object sender, EventArgs e)
        {
            CurrentUserStorage.ClearStorage();
            FavoritesStorage.ClearStorage();
            LoginStorage.ResetAllData();
            App.Current.MainPage = new NavigationPage(new OnBoardingPage());
        }

        private async void tapGoProife_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfilePage(CurrentUserStorage.GetCurrentUser().Id), true);
        }

        private async void tapFavmenuItems_Tapped(object sender, EventArgs e)
        {
            if(isProcessEnabled)
            {
                isProcessEnabled = false;
                await Navigation.PushAsync(new FavoriteMenuItemsPage(), true);
                isProcessEnabled = true;
            }
        }

        private async void tapFavRestaurants_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                await Navigation.PushAsync(new FavoriteRestaurantsPage(), true);
                isProcessEnabled = true;
            }
        }
    }
}