using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoriteRestaurantsPage : ContentPage
	{

        List<RestaurantCardModelBasic> cardModels = new List<RestaurantCardModelBasic>();
        List<Restaurant> restaurants = new List<Restaurant>();
        ExploreServices exploreServices = new ExploreServices();
        bool isRefreshRequired = false;

        bool isProcessEnabled = true;
        bool isAppeared = false;
		public FavoriteRestaurantsPage ()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!isAppeared)
            {
                isAppeared = true;
                stackLoading.IsVisible = true;
                collectionViewRestaurants.Opacity = 0;
                bool rests = await FillRestaurants();
                if (rests)
                {
                    stackLoading.IsVisible = false;
                    collectionViewRestaurants.IsVisible = true;
                    await collectionViewRestaurants.FadeTo(1);
                }
                else
                {
                    stackLoading.IsVisible = false;
                    lblNoFavs.IsVisible = true;
                }
            }
            else
            {
                if ( FavoritesStorage.isReloadRequiredRestaurant())
                {
                    lblNoFavs.IsVisible = false;
                    collectionViewRestaurants.IsVisible = false;
                    collectionViewRestaurants.Opacity = 0;
                    stackLoading.IsVisible = true;
                    bool rests = await FillRestaurants();
                    if (rests)
                    {
                        stackLoading.IsVisible = false;
                        collectionViewRestaurants.IsVisible = true;
                        await collectionViewRestaurants.FadeTo(1);
                    }
                    else
                    {
                        stackLoading.IsVisible = false;
                        lblNoFavs.IsVisible = true;
                    }
                    FavoritesStorage.FavoriteRestaurantReloaded();
                }
            }
        }

        private async Task<bool> FillRestaurants()
        {
            collectionViewRestaurants.ItemsSource = null;
            collectionViewRestaurants.BindingContext = null;
            if ( cardModels != null)
            {
                if (cardModels.Count > 0)
                    cardModels.Clear();
            }
            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants = await exploreServices.GetFavoriteRestaurants();
            if ( restaurants != null)
            {
                if ( restaurants.Count > 0)
                {
                    foreach( Restaurant rest in restaurants)
                    {
                        RestaurantCardModelBasic card = new RestaurantCardModelBasic() { Restaurant = rest };
                        cardModels.Add(card);
                    }
                    collectionViewRestaurants.BindingContext = cardModels;
                    collectionViewRestaurants.ItemsSource = cardModels;
                    return true;
                }
            }
            return false;
        }

        private async void tapSelectRestaurant_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                RestaurantCardModelBasic vm = senderGrid.BindingContext as RestaurantCardModelBasic;
                await Navigation.PushAsync(new RestaurantProfilePage(vm.RestaurantId), true);
                isProcessEnabled = true;
            }
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled )
            {
                isProcessEnabled = false;
                await Navigation.PopAsync();
            }
        }
    }
}