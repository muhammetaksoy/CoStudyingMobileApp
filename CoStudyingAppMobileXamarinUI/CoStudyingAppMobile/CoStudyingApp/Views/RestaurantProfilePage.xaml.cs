using Entities;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.BusinessModels;
using YeatyAppMobile.Models;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestaurantProfilePage : ContentPage
    {

        int slideItemCount = 0;
        int activeItemIndex = 0;
        bool isAppeared = false;
        int restaurantId = 0;
        bool isProcessEnabled = true;
        SelectedMenuItemViewModel selectedMenuItemViewModel = new SelectedMenuItemViewModel();
        List<SlideClass> slidePhotos = new List<SlideClass>();
        LatLongInfoModel latLongInfoModel = new LatLongInfoModel() { Lat =0, Long = 0 };
        ExploreServices exploreServices = new ExploreServices();
        List<CampaignRestaurantProfileResponseModel> campaignList = new List<CampaignRestaurantProfileResponseModel>();
        IList<ProductCardModelIndex> productModels = new ObservableCollection<ProductCardModelIndex>();
        IList<CampaignCardModelRestaurant> campaignModels = new ObservableCollection<CampaignCardModelRestaurant>();
        Restaurant restaurant = new Restaurant();
        bool isFavorite = false;
        IncreaseServices increaseServices = new IncreaseServices();
        DecreaseServices decreaseServices = new DecreaseServices();
        UserProfileResponseModel userProfileResponseModel = new UserProfileResponseModel();
        List<ReviewViewModel> viewModels = new List<ReviewViewModel>();

        class SlideClass
        {
            public string PhotoPath
            {
                get
                {
                    return SliderPhoto.PhotoPath;
                }
            }

            public int slideNumber { get; set; }

            public int itemId { get; set; }

            //public string PhotoPathTitle
            //{
            //    get
            //    {
            //        if (langCode == "tr")
            //            return SliderPhoto.PhotoPathTitleTR;
            //        return SliderPhoto.PhotoPathTitleEN;
            //    }
            //}

            public RestaurantPhoto SliderPhoto { get; set; }


        }


        public RestaurantProfilePage(int _restaurantId)
        {
            InitializeComponent();
            restaurantId = _restaurantId;
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Subscribe<PurchaseMenuItemPopupPage>(this, "PurchaseComplete", (sender) => {
                CancelItem();
            });
        }

        protected async override void OnAppearing()
        {
            
            base.OnAppearing();
            if (!isAppeared)
            {
                isAppeared = true;
                await LoadingState.StartLoading(mainGrid, stackLoading, lottieLoading);

                //favorite control
                if ( FavoritesStorage.isFavoriteRestaurant(restaurantId) )
                {
                    isFavorite = true;
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        lblHeart.FontFamily = "FontAwesome5Free-Solid";
                    }
                    else
                    {
                        lblHeart.FontFamily = "FontAwesome5Solid.otf#Regular";
                    }
                }

                if (AppPropertyStorage.isUserAllowedLocation())
                {
                    latLongInfoModel = await LocationRequest.GetCurrentLocation();
                    if (latLongInfoModel.Long != 0 && latLongInfoModel.Long != 0)
                    {
                        AppPropertyStorage.UpdateLocation(latLongInfoModel);
                    }
                }
                else
                {

                }

                latLongInfoModel = AppPropertyStorage.GetUserLastLocation();
                await Fill();
                await LoadingState.StopLoading(mainGrid, stackLoading, lottieLoading);  
                if ( SelectedMenuItemStorage.GetSelectedMenuItem(restaurantId) != null)
                {
                    selectedMenuItemViewModel.MenuItem = SelectedMenuItemStorage.GetSelectedMenuItem(restaurantId);                    
                    imgSelectedMenuItemImage.Source = selectedMenuItemViewModel.MenuItemImageUrl;
                    lblSelectedMenuItemNameAndPrice.Text = selectedMenuItemViewModel.MenuItemNameAndPrice;
                    frameSelectedItem.IsVisible = true;
                    await frameSelectedItem.FadeTo(1);
                }
                await FillItems();
                await FillCampaigns();
                await FillProps();
                await FillReviews();
            }
        }

        private async Task FillReviews()
        {
            List<Review> reviews = new List<Review>();
            reviews = await exploreServices.GetRestaurantReviews(restaurantId);
            if (reviews != null)
            {
                if (reviews.Count > 0)
                {
                    stackReviews.IsVisible = true;
                    foreach (Review review in reviews)
                    {
                        viewModels.Add(new ReviewViewModel() { Review = review });
                    }
                    collectionViewReviews.BindingContext = viewModels;
                    collectionViewReviews.ItemsSource = viewModels;
                }               
            }            
        }

        private async Task FillCampaigns()
        {
            
            campaignList = await exploreServices.GetRestaurantCampaigns(restaurantId);
            if ( campaignList == null || campaignList.Count <= 0 )
            {
                return;
            }
            else
            {
                foreach(CampaignRestaurantProfileResponseModel resp in campaignList)
                {
                    CampaignCardModelRestaurant node = new CampaignCardModelRestaurant()
                    {
                        CampaignText = resp.Campaign.SubText,
                        CampaignId = resp.Campaign.Id

                    };
                    if (restaurant.isTypeCoffee)
                        node.CardPhotoUrl = "campaignBgCafe.jpg";
                    else
                        node.CardPhotoUrl = "campaignBgRestaurant.jpg";
                    campaignModels.Add(node);
                }
            }
            collectionViewCampaigns.BindingContext = campaignModels;
            collectionViewCampaigns.ItemsSource = campaignModels;
            stackYeatyCampaigns.IsVisible = true;
            await stackYeatyCampaigns.FadeTo(1);
        }

        private async Task FillProps()
        {
            RestaurantPropertyList propList = await exploreServices.GetRestaurantProperties(restaurantId);
            if (propList == null)
                return;
            List<string> props = ReturnPropertiesTR(propList);
            int counter = 0;
            foreach( string prop in props )
            {
                StackLayout stackProp = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0 };
                Label lblCheck = new Label()
                {
                    FontFamily = GetFontFamilyIcons.FontAwesomeLight,
                    Text = "check-circle",
                    FontSize = 12.5,
                    TextColor = Color.FromHex("#006301"),
                };
                Label lblProp = new Label()
                {
                    FontFamily = "CustomMedium",
                    Text = prop,
                    FontSize = 12,
                    TextColor = CustomColors.defaultFoggyColor
                };
                stackProp.Children.Add(lblCheck);
                stackProp.Children.Add(lblProp);
                if ( counter%2 == 0 )
                {
                    stackPropertiesList1.Children.Add(stackProp);
                }
                else
                {
                    stackPropertiesList2.Children.Add(stackProp);
                }
                counter++;
            }
            stackProperties.IsVisible = true;
            await stackProperties.FadeTo(1);
        }

        private async Task Fill()
        {
            RestaurantProfileResponseModel resp = await exploreServices.GetRestaurantProfile(latLongInfoModel, restaurantId);
            if( resp == null || resp.Restaurant == null || String.IsNullOrEmpty(resp.Restaurant.RestaurantName))
            {
                await DisplayAlert("Hata", "Bir hata oluştu. Daha sonra tekrar deneyin", "OK");
                return;
            }
            lblRestaurantName.Text = resp.Restaurant.RestaurantName;
            restaurant = resp.Restaurant;
            lblAverageRate.Text = Math.Round(resp.Restaurant.Rating, 2).ToString();
            lblReviewsCount.Text = "(" + resp.Restaurant.ReviewCount.ToString() + " reviews)";
            lblKitchenType.Text = resp.Restaurant.RestaurantType;
            lblCityDistrict.Text = resp.Restaurant.City + ", " + resp.Restaurant.District;
            lblAverageCost.Text = "₺" + resp.Restaurant.AverageWageForTwoPeople + " iki kişi için (ortalama)";


            int counter = 0;
            foreach( RestaurantPhoto photo in resp.Restaurant.RestaurantPhotos)
            {
                counter++;
                slidePhotos.Add(new SlideClass()
                {
                    itemId = photo.Id,
                    SliderPhoto = photo,
                    slideNumber = counter
                });
            }
            slideItemCount = slidePhotos.Count;
            lblSlideIndicator.Text = "1/" + slideItemCount.ToString();

            carouselViewSlide.ItemsSource = slidePhotos;
        }

        private async Task FillItems()
        {
            List<Entities.MenuItem> menuItems = new List<Entities.MenuItem>();
            menuItems = await exploreServices.GetRestaurantMenuItems(restaurantId);
            if( menuItems==null || menuItems.Count ==0)
            {
                return;
            }
            foreach(Entities.MenuItem item in menuItems)
            {
                productModels.Add(new ProductCardModelIndex(FavoritesStorage.isFavoriteMenuItem(item.Id))
                {
                    RootItem = new RestaurantMenuItemResponseModel() { MenuItem = item },

                });
            }
            collectionView2.BindingContext = productModels;
            collectionView2.ItemsSource = productModels;
            stackSectionProducts.IsVisible = true;
            await stackSectionProducts.FadeTo(1);



        }

        private async void tapPrevious_Tapped(object sender, EventArgs e)
        {
            //if ( activeItemIndex > 0)
            //{
            //    int oldIndex = activeItemIndex;
            //    activeItemIndex--;
            //    carouselViewSlide.CurrentItem = dummySlides[oldIndex - 1];
            //    //ChangeIndicator(oldIndex, activeItemIndex);
            //}
            if (activeItemIndex > 0)
            {
                int oldIndex = activeItemIndex;
                activeItemIndex--;
                carouselViewSlide.CurrentItem = slidePhotos[oldIndex - 1];
                lblSlideIndicator.Text = (activeItemIndex + 1).ToString() + "/" + slideItemCount.ToString();
            }
        }

        private async void tapNext_Tapped(object sender, EventArgs e)
        {
            //if (activeItemIndex < slideItemCount-1)
            //{
            //    int oldIndex = activeItemIndex;
            //    activeItemIndex++;
            //    carouselViewSlide.CurrentItem = dummySlides[oldIndex + 1];
            //}
            if (activeItemIndex < slideItemCount - 1)
            {
                int oldIndex = activeItemIndex;
                activeItemIndex++;
                carouselViewSlide.CurrentItem = slidePhotos[oldIndex + 1];
                lblSlideIndicator.Text = (activeItemIndex + 1).ToString() + "/" + slideItemCount.ToString();
            }
        }

        private async void carouselViewSlide_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            //DummySlideClass dumm = carouselViewSlide.CurrentItem as DummySlideClass;
            //double heignt1 = carouselViewSlide.Height;
            //double widd = carouselViewSlide.Width;
            //if ( dumm.slideNumber -1 != activeItemIndex)
            //{
            //    int oldIndex = activeItemIndex;
            //    int newIndex = dumm.slideNumber - 1;
            //    activeItemIndex = newIndex;
            //    //ChangeIndicator(oldIndex, newIndex);
            //}
            SlideClass dumm = carouselViewSlide.CurrentItem as SlideClass;
            if (dumm.slideNumber - 1 != activeItemIndex)
            {
                int oldIndex = activeItemIndex;
                int newIndex = dumm.slideNumber - 1;
                activeItemIndex = newIndex;
                lblSlideIndicator.Text = dumm.slideNumber.ToString() + "/" + slideItemCount.ToString();
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void tapFavv_Tapped(object sender, EventArgs e)
        {
            Frame lbl = sender as Frame;
            ProductCardModelIndex vm = lbl.BindingContext as ProductCardModelIndex;
            if (vm.isLiked)
            {
                vm.LikeChanged(false);
            }
            else
            {
                vm.LikeChanged(true);
            }
        }


        private List<string> ReturnPropertiesTR(RestaurantPropertyList prop)
        {
            List<string> propList = new List<string>();

            if (prop.haveGarden)
                propList.Add("Açık alan var");
            if (prop.haveVegan)
                propList.Add("Vegan seçenekler mevcut");
            if (prop.haveFreeAnimal)
                propList.Add("Evcil hayvan girebilir");
            if (prop.haveWifi)
                propList.Add("Wifi mevcut");
            if (prop.haveParkingSpace)
                propList.Add("Otopark mevcut");
            if (prop.haveSmokingZone)
                propList.Add("Sigara alanı var");
            if (prop.haveSisha)
                propList.Add("Nargile var");
            if (prop.haveTerrace)
                propList.Add("Teras var");
            if (!String.IsNullOrEmpty(prop.ExtraProperty1))
                propList.Add(prop.ExtraProperty1);
            if (!String.IsNullOrEmpty(prop.ExtraProperty2))
                propList.Add(prop.ExtraProperty2);
            if (!String.IsNullOrEmpty(prop.ExtraProperty3))
                propList.Add(prop.ExtraProperty3);
            if (!String.IsNullOrEmpty(prop.ExtraProperty4))
                propList.Add(prop.ExtraProperty4);
            if (!String.IsNullOrEmpty(prop.ExtraProperty5))
                propList.Add(prop.ExtraProperty5);
            if (!String.IsNullOrEmpty(prop.ExtraProperty6))
                propList.Add(prop.ExtraProperty6);
            if (!String.IsNullOrEmpty(prop.ExtraProperty7))
                propList.Add(prop.ExtraProperty7);
            if (!String.IsNullOrEmpty(prop.ExtraProperty8))
                propList.Add(prop.ExtraProperty8);
            return propList;
        }

        private async void tapCancelItem_Tapped(object sender, EventArgs e)
        {            
            await frameSelectedItem.FadeTo(0);
            frameSelectedItem.IsVisible = false;
            SelectedMenuItemStorage.RemoveItemFromModel(selectedMenuItemViewModel.MenuItem.Id);
            
        }

        private async void CancelItem()
        {
            await frameSelectedItem.FadeTo(0);
            frameSelectedItem.IsVisible = false;
            SelectedMenuItemStorage.RemoveItemFromModel(selectedMenuItemViewModel.MenuItem.Id);
        }

        private async void tapOpenMenuItem_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled )
            {
                isProcessEnabled = false;
                await Navigation.PushPopupAsync(new PurchaseMenuItemPopupPage(selectedMenuItemViewModel.MenuItem, restaurant));
                isProcessEnabled = true;
            }
        }

        private async void tapSelectItem_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled )
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                ProductCardModelIndex cardModelIndex = senderGrid.BindingContext as ProductCardModelIndex;
                SelectedMenuItemStorage.AddNewItem(cardModelIndex.MenuItem, restaurantId);

                if (frameSelectedItem.IsVisible)
                {
                    await frameSelectedItem.FadeTo(0);
                    frameSelectedItem.IsVisible = false;
                    selectedMenuItemViewModel.MenuItem = cardModelIndex.MenuItem;
                    imgSelectedMenuItemImage.Source = selectedMenuItemViewModel.MenuItemImageUrl;
                    lblSelectedMenuItemNameAndPrice.Text = selectedMenuItemViewModel.MenuItemNameAndPrice;
                    frameSelectedItem.IsVisible = true;
                    await frameSelectedItem.FadeTo(1);

                }
                else
                {
                    selectedMenuItemViewModel.MenuItem = cardModelIndex.MenuItem;
                    imgSelectedMenuItemImage.Source = selectedMenuItemViewModel.MenuItemImageUrl;
                    lblSelectedMenuItemNameAndPrice.Text = selectedMenuItemViewModel.MenuItemNameAndPrice;
                    frameSelectedItem.IsVisible = true;
                    await frameSelectedItem.FadeTo(1);
                }
                isProcessEnabled = true;
            }
        }

        private async void tapBack_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void tapCampaignSelected_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled)
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                CampaignCardModelRestaurant cardModelIndex = senderGrid.BindingContext as CampaignCardModelRestaurant;
                await Navigation.PushPopupAsync(new PurchaseCampaignPopupPage(cardModelIndex.CampaignId));
                isProcessEnabled = true;
            }
        }

        private async void tapFavorite_Tapped(object sender, EventArgs e)
        {
            if ( isFavorite)
            {
                isFavorite = false;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    lblHeart.FontFamily = "FontAwesome5Free-Regular";
                }
                else
                {
                    lblHeart.FontFamily = "FontAwesome5Regular.otf#Regular";
                }
                FavoritesStorage.RemoveFavoriteRestaurant(restaurantId);
                bool res = await decreaseServices.RemoveFavoriteRestaurant(restaurantId);
               
            }
            else
            {
                isFavorite = true;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    lblHeart.FontFamily = "FontAwesome5Free-Solid";
                }
                else
                {
                    lblHeart.FontFamily = "FontAwesome5Solid.otf#Regular";
                }
                FavoritesStorage.AddFavoriteRestaurant(restaurantId);
                ///bool res = await increaseServices.AddFavoriteRestaurant(restaurantId);
                //return true;
            }
        }

        private async void tapVisitUser_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                StackLayout senderstack = sender as StackLayout;
                ReviewViewModel vm = senderstack.BindingContext as ReviewViewModel;
                await Navigation.PushAsync(new UserProfilePage(vm.Review.UserId), true);
                isProcessEnabled = true;
            }
        }
    }
}