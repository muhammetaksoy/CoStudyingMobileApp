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
using YeatyAppMobile.Models;
using YeatyAppMobile.Models.DBModels;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.ViewCells.CodeBased;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultPage : ContentPage
    {

        SearchServices searchServices = new SearchServices();
        IList<RestaurantCardModelBasic> restaurantViewModels = new ObservableCollection<RestaurantCardModelBasic>();
        string title = "";
        int searchType = 0;
        string keyword = "";
        bool isProcessEnabled = true;
        /// <summary>
        /// TYPES
        /// 1- Restaurant
        /// 2- MenuItem
        /// 3- Campaign
        /// </summary>
        /// 


        public SearchResultPage(int  _type, string _keyword)
        {
            InitializeComponent();
            searchType = _type;
            keyword = _keyword;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if ( searchType == 1)
            {
                stackLoading.IsVisible = true;               
                bool isFoundRest = await FillRestaurants();
                stackLoading.IsVisible = false;
               
                if ( isFoundRest )
                {
                    lblSearchResultsText.IsVisible = true;
                    stackResultsRestaurants.IsVisible = true;
                    stackResults.IsVisible = true;
                    await stackResults.FadeTo(1);
                }
                else
                {
                    lblNoResult.IsVisible = true;
                    stackResults.IsVisible = true;
                    await stackResults.FadeTo(1);
                }


            }
           
        }

        private async Task<bool> FillRestaurants()
        {
            GeolocationRequest locationRequest;
            Location currentLocation = null;
            List<Restaurant> restaurants = new List<Restaurant>();
            LatLongInfoModel latLongModel = new LatLongInfoModel()
            {
                Lat = 0,
                Long = 0
            };


            if ( keyword == "all" )
            {
                title = "Yeaty ile anlaşmalı mekanlar";
                restaurants = await searchServices.GetSearchRestaurants(latLongModel, "all");

            }
            else if ( keyword == "near" )
            {
                title = "Size en yakın mekanlar";
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
                    var response = await DisplayAlert("UYARI", "Cihazınızın Konum Servisleri açık değil", "Ayarlara Git", "Vazgeç");
                    if (response)
                        Xamarin.Essentials.AppInfo.ShowSettingsUI();
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                    var response = await DisplayAlert("UYARI", "Cihazınızın Konum Servis izinlerini düzenleyin", "Ayarlara Git", "Vazgeç");
                    if (response)
                        Xamarin.Essentials.AppInfo.ShowSettingsUI();
                }
                catch (Exception ex)
                {
                    // Unable to get location
                    await DisplayAlert("HATA", "Error: " + ex, "TAMAM");
                }
               

                if (currentLocation != null)
                {
                    latLongModel.Lat = currentLocation.Latitude;
                    latLongModel.Long = currentLocation.Longitude;
                }
                else
                    return false;
                restaurants = await searchServices.GetSearchRestaurants(latLongModel, "near");               
            }
            else
            {
                title = keyword + " bölgesindeki mekanlar";
                restaurants = await searchServices.GetSearchRestaurants(latLongModel, keyword);
            }
            if (restaurants == null || restaurants.Count == 0)
                return false;
            foreach( Restaurant restaurant in restaurants)
            {
                restaurantViewModels.Add(new RestaurantCardModelBasic()
                {
                    Restaurant = restaurant
                });
            }
            lblSearchResultsText.Text = title;
            collectionViewRestaurants.BindingContext = restaurantViewModels;
            collectionViewRestaurants.ItemsSource = restaurantViewModels;
            return true;

        }

        private async void tapSearch_Tapped(object sender, EventArgs e)
        {
            if( isProcessEnabled)
            {

            }
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                await Navigation.PopAsync();
                isProcessEnabled= true;
            }
        }

        private async void tapSelectRestaurant_Tapped(object sender, EventArgs e)
        {

        }
    }
}