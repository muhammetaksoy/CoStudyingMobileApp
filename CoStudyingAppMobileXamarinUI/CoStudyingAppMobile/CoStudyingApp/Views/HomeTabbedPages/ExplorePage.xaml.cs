using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.BusinessModels;
using YeatyAppMobile.Models;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.AppTexts;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views.HomeTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorePage : ContentPage
    {
        bool isWaitingState = false;
        int activeSectionNumber = 1;
        bool isAppeared = false;
        Location currentLocation = null;
        bool isProcessEnabled = true;
        bool isLocationPermitted = true;
        List<int> currentRestaurantIds = new List<int>();
        ExploreServices exploreServices = new ExploreServices();
        IList<RestaurantCardModelBasic> restaurantCardModels = new ObservableCollection<RestaurantCardModelBasic>();
        IList<ProductCardModelIndex> productModels = new ObservableCollection<ProductCardModelIndex>();
        IncreaseServices increaseServices = new IncreaseServices();
        DecreaseServices decreaseServices = new DecreaseServices();
        public ExplorePage()
        {
            InitializeComponent();
            this.Title = "Keşfet";
            
            lblHeaderSection1.Text = ExplorePageTexts.Header1ForYouText;
            lblSubTitleSection1.Text = ExplorePageTexts.SubTitleForYouText;
        }

        

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if ( !isAppeared )
            {
                isAppeared = true;
                await LoadingState.StartLoading(mainScrollView, stackLoading, lottieLoading);
                await FillFirstList(1);
                await LoadingState.StopLoading(mainScrollView, stackLoading, lottieLoading);
                await CheckCampaign();
                await FillSecondList(1);  //MenuItems
            }
            
        }

        private async Task FillFirstList(int type)
        {
            collectionView1.BindingContext = null;
            collectionView1.ItemsSource = null;
            restaurantCardModels.Clear();
            GeolocationRequest locationRequest;
            if( isLocationPermitted)
            {
                try
                {
                    locationRequest = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    currentLocation = await Geolocation.GetLocationAsync(locationRequest);
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await DisplayAlert("HATA", "Cihazınınız konum servislerini desteklemiyor", "TAMAM");
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                    var response = await DisplayAlert("UYARI", "Cihazınızın Konum Servisleri açık değil", "Ayarlara Git", "Hayır");
                    if (response)
                        Xamarin.Essentials.AppInfo.ShowSettingsUI();
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                    var response = await DisplayAlert("UYARI", "Cihazınızın Konum Servis izinlerini düzenleyin", "Ayarlara Git", "Hayır");
                    if (response)
                        Xamarin.Essentials.AppInfo.ShowSettingsUI();
                }
                catch (Exception ex)
                {
                    // Unable to get location
                    await DisplayAlert("HATA", "Error: " + ex, "TAMAM");
                }
            }

            LatLongInfoModel latLongModel = new LatLongInfoModel()
            {
                Lat = 0,
                Long = 0
            };

            if (currentLocation!= null)
            {
                latLongModel.Lat = currentLocation.Latitude;
                latLongModel.Long = currentLocation.Longitude;
                isLocationPermitted = true;
                AppPropertyStorage.SetUserAllowsLocation(true, latLongModel);
            }
            else
            {
                isLocationPermitted = false;
                AppPropertyStorage.SetUserAllowsLocation(false, latLongModel);
            }
            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants = await exploreServices.GetExploreResultNearbyRestaurants(latLongModel, type);
            if( restaurants.Count > 0)
            {
                foreach(Restaurant rest in restaurants)
                {
                    restaurantCardModels.Add(new RestaurantCardModelBasic()
                    {
                        Restaurant = rest
                    });
                    currentRestaurantIds.Add(rest.Id);
                }

                collectionView1.BindingContext = restaurantCardModels;
                collectionView1.ItemsSource = restaurantCardModels;
                stackSection1.IsVisible = true;
            }
        }

        private async Task FillSecondList(int type)
        {
            collectionView2.BindingContext = null;
            collectionView2.ItemsSource = null;
            stackSection3.IsVisible = false;
            productModels.Clear();
            LatLongInfoModel latLongModel = new LatLongInfoModel()
            {
                Lat = 0,
                Long = 0
            };

            if (currentLocation != null)
            {
                latLongModel.Lat = currentLocation.Latitude;
                latLongModel.Long = currentLocation.Longitude;
            }
            List<RestaurantMenuItemResponseModel> rootItems = new List<RestaurantMenuItemResponseModel>();
            rootItems = await exploreServices.GetExploreResultMenuItems(currentRestaurantIds, type);
            if (rootItems.Count > 0)
            {
                foreach (RestaurantMenuItemResponseModel rootItem in rootItems)
                {
                    productModels.Add(new ProductCardModelIndex(FavoritesStorage.isFavoriteMenuItem(rootItem.MenuItem.Id))
                    {
                        RootItem = rootItem
                    });
                }

                collectionView2.BindingContext = productModels;
                collectionView2.ItemsSource = productModels;
                stackSection3.IsVisible = true;

            }
        }

        private async Task CheckCampaign()
        {
            Campaign campaign = new Campaign();
            campaign = await exploreServices.GetRegularCampaign();
            if (campaign == null || campaign.Id == 0)
            {
                return;
            }
            else
            {
                imgGeneralCampaign.Source = campaign.BackgroundImageUrl;
                lblGeneralCampaignTitle.Text = campaign.Text;
                lblGeneralCampaignSubTitle.Text = campaign.SubText;
                frameGeneralCampaign.IsVisible = true;
                frameGeneralCampaign.FadeTo(1);
            }
        }

        //private async void swipeRecognizer_Swiped_Left(object sender, SwipedEventArgs e)
        //{
        //    if (!isWaitingState)
        //    {
        //        isWaitingState = true;
        //        if (activeSectionNumber == 1)
        //            return;
        //        if (activeSectionNumber == 5)
        //        {
        //            sectionText5.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText4.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText5.FontFamily = "CustomMedium";
        //            sectionText4.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine5.FadeTo(0, 100),
        //                SectionLine4.FadeTo(1, 100),
        //                sectionText5.FadeTo(0.8, 100),
        //                sectionText4.FadeTo(1, 100)
        //                );

        //        }
        //        else if (activeSectionNumber == 4)
        //        {
        //            sectionText4.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText3.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText4.FontFamily = "CustomMedium";
        //            sectionText3.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine4.FadeTo(0, 100),
        //                SectionLine3.FadeTo(1, 100),
        //                sectionText4.FadeTo(0.8, 100),
        //                sectionText3.FadeTo(1, 100)
        //                );

        //        }
        //        else if (activeSectionNumber == 3)
        //        {
        //            sectionText3.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText2.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText3.FontFamily = "CustomMedium";
        //            sectionText2.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine3.FadeTo(0, 100),
        //                SectionLine2.FadeTo(1, 100),
        //                sectionText3.FadeTo(0.8, 100),
        //                sectionText2.FadeTo(1, 100)
        //                );

        //        }
        //        else if (activeSectionNumber == 2)
        //        {
        //            sectionText2.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText1.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText2.FontFamily = "CustomMedium";
        //            sectionText1.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine2.FadeTo(0, 100),
        //                SectionLine1.FadeTo(1, 100),
        //                sectionText2.FadeTo(0.8, 100),
        //                sectionText1.FadeTo(1, 100)
        //                );

        //        }
        //        activeSectionNumber--;
        //        isWaitingState = false;
        //    }

        //}

        //private async void swipeRecognizer_Swiped_Right(object sender, SwipedEventArgs e)
        //{

        //    if (!isWaitingState)
        //    {
        //        isWaitingState = true;
        //        if (activeSectionNumber == 5)
        //            return;
        //        if (activeSectionNumber == 1)
        //        {
        //            sectionText1.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText2.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText1.FontFamily = "CustomMedium";
        //            sectionText2.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine1.FadeTo(0, 100),
        //                SectionLine2.FadeTo(1, 100),
        //                sectionText1.FadeTo(0.8, 100),
        //                sectionText2.FadeTo(1, 100)
        //                );

        //        }
        //        else if (activeSectionNumber == 2)
        //        {
        //            sectionText2.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText3.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText2.FontFamily = "CustomMedium";
        //            sectionText3.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine2.FadeTo(0, 100),
        //                SectionLine3.FadeTo(1, 100),
        //                sectionText2.FadeTo(0.8, 100),
        //                sectionText3.FadeTo(1, 100)
        //                );

        //        }
        //        else if (activeSectionNumber == 3)
        //        {
        //            sectionText3.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText4.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText3.FontFamily = "CustomMedium";
        //            sectionText4.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine3.FadeTo(0, 100),
        //                SectionLine4.FadeTo(1, 100),
        //                sectionText3.FadeTo(0.8, 100),
        //                sectionText4.FadeTo(1, 100)
        //                );

        //        }
        //        else if (activeSectionNumber == 4)
        //        {
        //            sectionText4.TextColor = CustomColors.defaultShadedWhiteColor;
        //            sectionText5.TextColor = CustomColors.defaultWhiteColor;
        //            sectionText4.FontFamily = "CustomMedium";
        //            sectionText5.FontFamily = "CustomBold";
        //            await Task.WhenAny(
        //                SectionLine4.FadeTo(0, 100),
        //                SectionLine5.FadeTo(1, 100),
        //                sectionText4.FadeTo(0.8, 100),
        //                sectionText5.FadeTo(1, 100)
        //                );

        //        }
        //        activeSectionNumber++;
        //        isWaitingState = false;
        //    }
        //}

        //private async void tapFavorite_Tapped(object sender, EventArgs e)
        //{
        //    Frame lbl = sender as Frame;
        //    ProductCardModelIndex vm = lbl.BindingContext as ProductCardModelIndex;
        //    if ( vm.isLiked)
        //    {
        //        vm.LikeChanged(false);
        //        FavoritesStorage.RemoveFavoriteMenuItem(vm.MenuItem.Id);
        //        await decreaseServices.RemoveFavoriteMenuItem(vm.MenuItem.Id);

        //    }
        //    else
        //    {
        //        vm.LikeChanged(true);
        //        FavoritesStorage.AddFavoriteMenuItem(vm.MenuItem.Id);
        //        await increaseServices.AddFavoriteMenuItem(vm.MenuItem.Id);
        //    }          
        //}




        private async void tapSeeAllSection1_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled )
            {
                isProcessEnabled = false;
                //await Navigation.PushAsync(new SearchResultPage(1, "all"), true);
                await Navigation.PushAsync(new SearchIndexPage(false, 9, "all"));
                isProcessEnabled = true;
            }
        }

        private void tapShowGeneralCampaign_Tapped(object sender, EventArgs e)
        {

        }

        private async void tapSelectionCoffee_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled && activeSectionNumber != 2)
            {
                isProcessEnabled = false;
                if( activeSectionNumber == 1)
                    await Task.WhenAny(frameSelectionTab1.FadeTo(0.5), frameSelectionTab2.FadeTo(1));
                else if( activeSectionNumber == 3 )
                    await Task.WhenAny(frameSelectionTab3.FadeTo(0.5), frameSelectionTab2.FadeTo(1));
                else if (activeSectionNumber == 4)
                    await Task.WhenAny(frameSelectionTab4.FadeTo(0.5), frameSelectionTab2.FadeTo(1));
                else if (activeSectionNumber == 5)
                    await Task.WhenAny(frameSelectionTab5.FadeTo(0.5), frameSelectionTab2.FadeTo(1));
                else if (activeSectionNumber == 6)
                    await Task.WhenAny(frameSelectionTab6.FadeTo(0.5), frameSelectionTab2.FadeTo(1));
                activeSectionNumber = 2;
                lblHeaderSection1.Text = ExplorePageTexts.Header1CoffeeText;
                lblSubTitleSection1.Text = ExplorePageTexts.SubTitleCoffeeText;
                await LoadingState.StartLoading(mainScrollView, stackLoading, lottieLoading);
                await FillFirstList(2);
                await LoadingState.StopLoading(mainScrollView, stackLoading, lottieLoading);
                isProcessEnabled = true;
                await FillSecondList(2);

            }
        }

        private async void tapSelectionDinner_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled && activeSectionNumber != 3)
            {
                isProcessEnabled = false;
                if (activeSectionNumber == 1)
                    await Task.WhenAny(frameSelectionTab1.FadeTo(0.5), frameSelectionTab3.FadeTo(1));
                else if (activeSectionNumber == 2)
                    await Task.WhenAny(frameSelectionTab2.FadeTo(0.5), frameSelectionTab3.FadeTo(1));
                else if (activeSectionNumber == 4)
                    await Task.WhenAny(frameSelectionTab4.FadeTo(0.5), frameSelectionTab3.FadeTo(1));
                else if (activeSectionNumber == 5)
                    await Task.WhenAny(frameSelectionTab5.FadeTo(0.5), frameSelectionTab3.FadeTo(1));
                else if (activeSectionNumber == 6)
                    await Task.WhenAny(frameSelectionTab6.FadeTo(0.5), frameSelectionTab3.FadeTo(1));
                activeSectionNumber = 3;
                lblHeaderSection1.Text = ExplorePageTexts.Header1DinnerText;
                lblSubTitleSection1.Text = ExplorePageTexts.SubTitleDinnerText;
                await LoadingState.StartLoading(mainScrollView, stackLoading, lottieLoading);
                await FillFirstList(3);
                await LoadingState.StopLoading(mainScrollView, stackLoading, lottieLoading);
                isProcessEnabled = true;
                await FillSecondList(3);

            }
        }

        private async void tapSelectionBreakfast_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled && activeSectionNumber != 4)
            {
                isProcessEnabled = false;
                if (activeSectionNumber == 1)
                    await Task.WhenAny(frameSelectionTab1.FadeTo(0.5), frameSelectionTab4.FadeTo(1));
                else if (activeSectionNumber == 2)
                    await Task.WhenAny(frameSelectionTab2.FadeTo(0.5), frameSelectionTab4.FadeTo(1));
                else if (activeSectionNumber == 3)
                    await Task.WhenAny(frameSelectionTab3.FadeTo(0.5), frameSelectionTab4.FadeTo(1));
                else if (activeSectionNumber == 5)
                    await Task.WhenAny(frameSelectionTab5.FadeTo(0.5), frameSelectionTab4.FadeTo(1));
                else if (activeSectionNumber == 6)
                    await Task.WhenAny(frameSelectionTab6.FadeTo(0.5), frameSelectionTab4.FadeTo(1));
                activeSectionNumber = 4;
                lblHeaderSection1.Text = ExplorePageTexts.Header1BreakfastText;
                lblSubTitleSection1.Text = ExplorePageTexts.SubTitleBreakfastText;
                await LoadingState.StartLoading(mainScrollView, stackLoading, lottieLoading);
                await FillFirstList(4);
                await LoadingState.StopLoading(mainScrollView, stackLoading, lottieLoading);
                isProcessEnabled = true;
                await FillSecondList(4);

            }
        }

        private async void tapSelectionSweet_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled && activeSectionNumber != 5)
            {
                isProcessEnabled = false;
                if (activeSectionNumber == 1)
                    await Task.WhenAny(frameSelectionTab1.FadeTo(0.5), frameSelectionTab5.FadeTo(1));
                else if (activeSectionNumber == 2)
                    await Task.WhenAny(frameSelectionTab2.FadeTo(0.5), frameSelectionTab5.FadeTo(1));
                else if (activeSectionNumber == 3)
                    await Task.WhenAny(frameSelectionTab3.FadeTo(0.5), frameSelectionTab5.FadeTo(1));
                else if (activeSectionNumber == 4)
                    await Task.WhenAny(frameSelectionTab4.FadeTo(0.5), frameSelectionTab5.FadeTo(1));
                else if (activeSectionNumber == 6)
                    await Task.WhenAny(frameSelectionTab6.FadeTo(0.5), frameSelectionTab5.FadeTo(1));
                activeSectionNumber = 5;
                lblHeaderSection1.Text = ExplorePageTexts.Header1SweetText;
                lblSubTitleSection1.Text = ExplorePageTexts.SubTitleSweetText;
                await LoadingState.StartLoading(mainScrollView, stackLoading, lottieLoading);
                await FillFirstList(5);
                await LoadingState.StopLoading(mainScrollView, stackLoading, lottieLoading);
                isProcessEnabled = true;
                await FillSecondList(5);

            }
        }

        private async void tapSelectionOpenAir_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled && activeSectionNumber != 6)
            {
                isProcessEnabled = false;
                if (activeSectionNumber == 1)
                    await Task.WhenAny(frameSelectionTab1.FadeTo(0.5), frameSelectionTab6.FadeTo(1));
                else if (activeSectionNumber == 2)
                    await Task.WhenAny(frameSelectionTab2.FadeTo(0.5), frameSelectionTab6.FadeTo(1));
                else if (activeSectionNumber == 3)
                    await Task.WhenAny(frameSelectionTab3.FadeTo(0.5), frameSelectionTab6.FadeTo(1));
                else if (activeSectionNumber == 5)
                    await Task.WhenAny(frameSelectionTab5.FadeTo(0.5), frameSelectionTab6.FadeTo(1));
                else if (activeSectionNumber == 4)
                    await Task.WhenAny(frameSelectionTab4.FadeTo(0.5), frameSelectionTab6.FadeTo(1));
                activeSectionNumber = 6;
                lblHeaderSection1.Text = ExplorePageTexts.Header1OpenAirText;
                lblSubTitleSection1.Text = ExplorePageTexts.SubTitleOpenAirText;
                await LoadingState.StartLoading(mainScrollView, stackLoading, lottieLoading);
                await FillFirstList(6);
                await LoadingState.StopLoading(mainScrollView, stackLoading, lottieLoading);
                isProcessEnabled = true;
                await FillSecondList(6);
            }
        }

        private async void tapSelectionForYou_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled && activeSectionNumber != 1)
            {
                isProcessEnabled = false;
                if (activeSectionNumber == 6)
                    await Task.WhenAny(frameSelectionTab6.FadeTo(0.5), frameSelectionTab1.FadeTo(1));
                else if (activeSectionNumber == 2)
                    await Task.WhenAny(frameSelectionTab2.FadeTo(0.5), frameSelectionTab1.FadeTo(1));
                else if (activeSectionNumber == 3)
                    await Task.WhenAny(frameSelectionTab3.FadeTo(0.5), frameSelectionTab1.FadeTo(1));
                else if (activeSectionNumber == 5)
                    await Task.WhenAny(frameSelectionTab5.FadeTo(0.5), frameSelectionTab1.FadeTo(1));
                else if (activeSectionNumber == 4)
                    await Task.WhenAny(frameSelectionTab4.FadeTo(0.5), frameSelectionTab1.FadeTo(1));
                activeSectionNumber = 1;
                lblHeaderSection1.Text = ExplorePageTexts.Header1ForYouText;
                lblSubTitleSection1.Text = ExplorePageTexts.SubTitleForYouText;
                await LoadingState.StartLoading(mainScrollView, stackLoading, lottieLoading);
                await FillFirstList(1);
                await LoadingState.StopLoading(mainScrollView, stackLoading, lottieLoading);
                isProcessEnabled = true;
                await FillSecondList(1);
            }
        }

        private async void collectionView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private async void tapSelectRestaurant_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                RestaurantCardModelBasic selected = senderGrid.BindingContext as RestaurantCardModelBasic;
                await Navigation.PushAsync(new RestaurantProfilePage(selected.RestaurantId));
                isProcessEnabled = true;
            }
        }

        private async void tapSelectMenuItem_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled)
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                ProductCardModelIndex cardModelIndex = senderGrid.BindingContext as ProductCardModelIndex;
                SelectedMenuItemStorage.AddNewItem(cardModelIndex.MenuItem, cardModelIndex.Restaurant.Id);
                await Navigation.PushAsync(new RestaurantProfilePage(cardModelIndex.Restaurant.Id));
                isProcessEnabled = true;
            }           
        }

        private async void tapGoToSearch_Tapped(object sender, EventArgs e)
        {
            if( isProcessEnabled )
            {
                isProcessEnabled = false;
                await Navigation.PushAsync(new SearchIndexPage(true, 1), true);
                isProcessEnabled = true;
            }
        }
    }
}