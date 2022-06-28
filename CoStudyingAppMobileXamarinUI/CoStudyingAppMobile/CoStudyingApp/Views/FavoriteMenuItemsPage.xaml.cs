using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteMenuItemsPage : ContentPage
    {
        bool isProcessEnabled = true;
        bool isAppeared = false;
        ExploreServices exploreServices = new ExploreServices();
        IList<RestaurantCardModelBasic> restaurantCardModels = new ObservableCollection<RestaurantCardModelBasic>();
        IList<ProductCardModelIndex> productModels = new ObservableCollection<ProductCardModelIndex>();
        IncreaseServices increaseServices = new IncreaseServices();
        DecreaseServices decreaseServices = new DecreaseServices();
        public FavoriteMenuItemsPage()
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
                List<FavoriteMenuItemResponseModel> responses = await exploreServices.GetFavoriteMenuItems();
                if ( responses != null)
                {
                    if (responses.Count > 0)
                    {
                        foreach( FavoriteMenuItemResponseModel resp in responses)
                        {
                            RestaurantMenuItemResponseModel resp2 = new RestaurantMenuItemResponseModel() { MenuItem = resp.MenuItem, Restaurant = resp.Restaurant };
                            productModels.Add(new ProductCardModelIndex(true)
                            {
                                RootItem = resp2
                            });
                        }
                        collectionViewFavorites.ItemsSource = productModels;
                        collectionViewFavorites.BindingContext = productModels;
                        stackLoading.IsVisible = false;
                        collectionViewFavorites.IsVisible = true;
                        await collectionViewFavorites.FadeTo(1);
                    }
                    else
                    {
                        stackLoading.IsVisible = false;
                        lblNoFavs.IsVisible = true;
                    }
                }
                else
                {
                    stackLoading.IsVisible = false;
                    lblNoFavs.IsVisible = true;
                }
            }
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            isProcessEnabled = false;
            await Navigation.PopAsync();
        }

        private async void tapSelectMenuItem_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                ProductCardModelIndex cardModelIndex = senderGrid.BindingContext as ProductCardModelIndex;
                SelectedMenuItemStorage.AddNewItem(cardModelIndex.MenuItem, cardModelIndex.Restaurant.Id);
                await Navigation.PushAsync(new RestaurantProfilePage(cardModelIndex.Restaurant.Id));
                isProcessEnabled = true;
            }
        }

        private void tapFavv_Tapped(object sender, EventArgs e)
        {

        }

        private async void tapFavorite_Tapped(object sender, EventArgs e)
        {
            if (await DisplayAlert("Kaldır", "Seçtiğiniz ürünü favorilerden kaldırmak istiyor musunuz?", "Evet", "Hayır"))
            {
                Frame lbl = sender as Frame;
                ProductCardModelIndex vm = lbl.BindingContext as ProductCardModelIndex;
                vm.LikeChanged(false);
                FavoritesStorage.RemoveFavoriteMenuItem(vm.MenuItem.Id);
                await Task.Delay(500);
                productModels.Remove(vm);
                await decreaseServices.RemoveFavoriteMenuItem(vm.MenuItem.Id);
            }
            
        }
    }
}