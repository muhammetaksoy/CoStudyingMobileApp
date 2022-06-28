using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        bool isAppeared = false;
        int userId = 0;
        bool isProcessEnabled = true;
        ExploreServices exploreServices = new ExploreServices();
        UserProfileResponseModel userProfileResponseModel = new UserProfileResponseModel();
        List<ReviewViewModel> viewModels = new List<ReviewViewModel>();
        public UserProfilePage(int _userId)
        {
            InitializeComponent();
            userId = _userId;
            NavigationPage.SetHasNavigationBar(this, false);
            //lblReviewText1.Text = "Manzarasına denilecek yok kahvaltılık ürünleri de başarılı lakin salonda gözüken garson yok hizmet 10 üzerinden 1 👎 Böyle kaliteli bir mekana olmamış";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if( !isAppeared)
            {
                isAppeared = true;
                stackLoading.IsVisible = true;
                userProfileResponseModel = await exploreServices.GetUserProfile(userId);
                if (userProfileResponseModel.Reviews != null)
                {
                    if (userProfileResponseModel.Reviews.Count > 0)
                    {
                        stackReviews.IsVisible = true;
                        foreach (Review review in userProfileResponseModel.Reviews)
                        {
                            viewModels.Add(new ReviewViewModel() { Review = review });
                        }
                        collectionViewReviews.BindingContext = viewModels;
                        collectionViewReviews.ItemsSource = viewModels;
                    }
                    else
                    {
                        lblNoReviews.IsVisible = true;
                    }
                }
                else
                {
                    lblNoReviews.IsVisible = true;
                }
                stackLoading.IsVisible = false;
                mainScrollView.IsVisible = true;
            }
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void tapVisitRestaurant_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                StackLayout senderstack = sender as StackLayout;
                ReviewViewModel vm = senderstack.BindingContext as ReviewViewModel;
                await Navigation.PushAsync(new RestaurantProfilePage(vm.Review.RestaurantId), true);
                isProcessEnabled = true;
            }
        }
    }
}