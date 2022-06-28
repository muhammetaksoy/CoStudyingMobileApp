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
    public partial class SearchIndexPage : ContentPage
    {




        /// <summary>
        /// SEARCHING TYPES:
        ///
        /// 1-  Default
        /// 2-  Restaurant/Cafe
        /// 3-  RestaurantType
        /// 4-  MenuItemType
        /// 5-  City
        /// 6-  District
        /// 7-  User
        /// 8-  Custom
        /// 9-   All Restaurants
        /// 10 - All MenuItems
        /// 11 - All Campaigns 
        /// </summary>

        List<SearchIndexDistrictsResponseModel> districtResponses = new List<SearchIndexDistrictsResponseModel>();
        List<SearchTopicResultResponseModel> searchTopicResults = new List<SearchTopicResultResponseModel>();
        List<SearchPageIndexResultViewModel> searchPageIndexResultViewModels = new List<SearchPageIndexResultViewModel>();
        List<DistrictCardViewCell> viewCellsDistricts = new List<DistrictCardViewCell>();
        List<RecentSearch> recentSearches = new List<RecentSearch>();
        bool isAppeared = false;
        bool isDirectFocus = false;
        bool isProcessEnabled = true;
        int searchingType;
        string searched = "";
        bool isCustomPageOn = true;
        int activeSectionNumber = 0;
        SearchServices searchServices = new SearchServices();
        IList<RestaurantCardModelBasic> suggestedRestaurants = new ObservableCollection<RestaurantCardModelBasic>();
        IList<RestaurantCardModelBasic> restaurantViewModels = new ObservableCollection<RestaurantCardModelBasic>();


        public SearchIndexPage(bool _isDirectFocus, int _searchingType = 1, string _searched = "")
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            isDirectFocus = _isDirectFocus;
            searchingType = _searchingType;
            searched = _searched;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if ( isDirectFocus )
            {
                entrySearch.Focus();
            }
            if ( !isAppeared)
            {
                isAppeared = true;
                if (searchingType == 1)
                {
                    activeSectionNumber = 1;
                    stackDefaultSearch.IsVisible = true;
                    bool isSuccess = await FillDistricts();
                    if ( isSuccess)
                    {
                        stackDistricts.IsVisible = true;
                        scrollViewDistricts.IsVisible = true;
                        await Task.Delay(250);
                        await scrollViewDistricts.FadeTo(1);
                    }
                    bool isSuccess1 = await FillRecentSearches();
                    if ( isSuccess1)
                    {
                        stackRecentSearches.IsVisible = true;
                        await stackRecentSearches.FadeTo(1);
                    }
                    bool isSuccess2 = await FillSuggestedRestaurants();
                    if ( isSuccess2)
                    {
                        stackSuggestedRestaurants.IsVisible = true;
                        await stackSuggestedRestaurants.FadeTo(1);
                    }
                }
                else if ( searchingType == 9)
                {
                    activeSectionNumber = 2;
                    stackDefaultSearch.IsVisible = false;
                    stackDefaultSearch.Opacity = 0;
                    bool isSuccess = false;
                    stackLoading.IsVisible = true;
                    isSuccess = await FillRestaurants(searched);
                    if ( isSuccess )
                    {
                        lblSearchResultsRestaurantsText.IsVisible = true;
                        lblNoResultRestaurants.IsVisible = false;
                    }
                    else
                    {
                        lblSearchResultsRestaurantsText.IsVisible = false;
                        lblNoResultRestaurants.IsVisible = true;
                    }
                    stackLoading.IsVisible = false;
                    await ActivateSection(3);

                }
                //else if ( searchingType ==  )

            }
        }

        private async Task<bool> FillRestaurants(string keyword)
        {
            lblSearchResultsRestaurantsText.IsVisible = false;
            lblNoResultRestaurants.IsVisible = false;
            string title;
            GeolocationRequest locationRequest;
            Location currentLocation = null;
            List<Restaurant> restaurants = new List<Restaurant>();
            LatLongInfoModel latLongModel = new LatLongInfoModel()
            {
                Lat = 0,
                Long = 0
            };


            if (keyword == "all")
            {
                title = "Yeaty ile anlaşmalı mekanlar";
                restaurants = await searchServices.GetSearchRestaurants(latLongModel, "all");

            }
            else if (keyword == "near")
            {
                title = "Size en yakın mekanlar";
                try
                {
                    locationRequest = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(5));
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
            foreach (Restaurant restaurant in restaurants)
            {
                restaurantViewModels.Add(new RestaurantCardModelBasic()
                {
                    Restaurant = restaurant
                });
            }
            lblSearchResultsRestaurantsText.Text = title;
            collectionViewRestaurants.BindingContext = restaurantViewModels;
            collectionViewRestaurants.ItemsSource = restaurantViewModels;
            return true;

        }

        private async Task<bool> FillRecentSearches()
        {
            recentSearches.Clear();
            recentSearches = await searchServices.GetRecentSearches();
            if (recentSearches == null || recentSearches.Count <= 0)
                return false;
            if ( recentSearches.Count > 0 )
            {
                lblRecentSearch1.Text = GetSentenceRecentSearch(recentSearches[0]);
                stackRecentSearch1.IsVisible = true;               
            }
            if ( recentSearches.Count > 1)
            {
                lblRecentSearch2.Text = GetSentenceRecentSearch(recentSearches[1]);
                stackRecentSearch2.IsVisible = true;
                boxView1.IsVisible = true;
            }
            if( recentSearches.Count > 2)
            {
                lblRecentSearch3.Text = GetSentenceRecentSearch(recentSearches[2]);
                stackRecentSearch3.IsVisible = true;
                boxView2.IsVisible = true;
            }
            return true;
        }

        private async Task<bool> FillDistricts()
        {
            districtResponses = await searchServices.GetSearchIndexDistricts();
            if (districtResponses == null || districtResponses.Count <= 0)
                return false;
            foreach( SearchIndexDistrictsResponseModel response in districtResponses)
            {
                DistrictCardViewCell viewCell = new DistrictCardViewCell(response);
                viewCellsDistricts.Add(viewCell);
                insideScrollViewDistrictsFlexLayout.Children.Add(viewCell.mainFrame);
            }
            return true;
        }

        private async Task<bool> FillSuggestedRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants = await searchServices.GetSuggestedRestaurantsIndex();
            if (restaurants.Count > 0)
            {
                foreach (Restaurant rest in restaurants)
                {
                    suggestedRestaurants.Add(new RestaurantCardModelBasic()
                    {
                        Restaurant = rest
                    });
                    //currentRestaurantIds.Add(rest.Id);
                }

                collectionViewSuggestedRestaurants.BindingContext = suggestedRestaurants;
                collectionViewSuggestedRestaurants.ItemsSource = suggestedRestaurants;
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void tapSearch_Tapped(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(entrySearch.Text))
            {
                if ( entrySearch.Text.Length >1)
                {
                    if( isProcessEnabled)
                    {
                        searched = "";
                        isProcessEnabled = false;
                        lblSearchResultsText.IsVisible = false;
                        lblNoResult.IsVisible = false;
                        searched = entrySearch.Text;
                        stackDefaultSearch.IsVisible = false;
                        stackSearchResults.IsVisible = false;
                        stackSearchResultItems.IsVisible = false;
                        stackDefaultSearch.Opacity = 0;
                        stackSearchResults.Opacity = 0;
                        stackLoading.IsVisible = true;
                        await DeactivateCurrentSection();
                        bool isFound = await FillSearchResults();
                        stackLoading.IsVisible = false;
                        stackSearchResults.IsVisible = true;
                        if ( isFound)
                        {
                            lblSearchResultsText.IsVisible = true;
                            stackSearchResultItems.IsVisible = true;
                            await stackSearchResults.FadeTo(1);
                        }
                        else
                        {
                            lblNoResult.IsVisible = true;
                            await stackSearchResults.FadeTo(1);
                        }
                        activeSectionNumber = 2;
                        isProcessEnabled = true;
                    }
                }
               
            }            
        }

        private async Task<bool> FillSearchResults()
        {
            searchTopicResults.Clear();
            stackSearchResultItems.Children.Clear();
            searchPageIndexResultViewModels.Clear();
            searchTopicResults = await searchServices.GetSearchTopicResults(searched);
            if(searchTopicResults == null || searchTopicResults.Count == 0)
            {
                return false;
            }
            else
            {
                foreach(SearchTopicResultResponseModel respModel in searchTopicResults)
                {
                    SearchPageIndexResultViewModel vm = new SearchPageIndexResultViewModel() { responseModel = respModel };
                    searchPageIndexResultViewModels.Add(vm);
                    SearchMainPageSearchResultViewCell viewCell = new SearchMainPageSearchResultViewCell(vm);
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += tapSelectSearchResult;
                    viewCell.mainGrid.GestureRecognizers.Add(tapGestureRecognizer);
                    stackSearchResultItems.Children.Add(viewCell.mainGrid);
                }
                return true;
            }
        }

        private async void tapSelectSearchResult(object sender, EventArgs e)
        {
            if( isProcessEnabled )
            {
                isProcessEnabled = false;
                await DeactivateCurrentSection();
                stackLoading.IsVisible = true;
                Grid senderGrid = sender as Grid;
                SearchPageIndexResultViewModel vm = senderGrid.BindingContext as SearchPageIndexResultViewModel; 
                if ( vm.responseModel.isLocation )
                {
                    bool isSuccess = await FillRestaurants(vm.responseModel.LocationName);
                    if (isSuccess)
                    {
                        lblSearchResultsRestaurantsText.IsVisible = true;
                        lblNoResultRestaurants.IsVisible = false;
                    }
                    else
                    {
                        lblSearchResultsRestaurantsText.IsVisible = false;
                        lblNoResultRestaurants.IsVisible = true;
                    }
                    stackLoading.IsVisible = false;
                    await ActivateSection(3);
                }
                else
                {

                }
                
            }
        }

       


        private string GetSentenceRecentSearch(RecentSearch model)
        {
            if ( model.isCity)
            {
                return model.CityName + " bölgesindeki mekanlar";
            }
            else if ( model.isDistrict)
            {
                return model.DistrictName + " bölgesindeki mekanlar";
            }
            else if ( model.isRestaurant)
            {
                return model.SearchFixedkey + ", " + model.DistrictName;
            }
            else if( model.isMenuItemType)
            {
                return model.SearchFixedkey + " için sonuçlar";
            }
            else
            {
                return "";
            }
        }

        private async void tapSelectRestaurant_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled)
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
                double height = frameSymbol.Height;
                double width = frameSymbol.Width;
                isProcessEnabled = false;
                await Navigation.PopAsync();
                isProcessEnabled = true;
            }
        }

        private void SituationControl()
        {
            int a = stackSearchResultItems.Children.Count;
            int b = stackSearchResults.Children.Count;
            bool ab = stackSearchResults.IsVisible;
            bool ac = stackSearchResultItems.IsVisible;
            double ad = stackSearchResults.Opacity;
            double ae = stackSearchResultItems.Opacity;
        }

        private async void entrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (activeSectionNumber != 1 && isProcessEnabled)
            {
                if ( String.IsNullOrEmpty(entrySearch.Text))
                {
                    isProcessEnabled = false;
                    //isCustomPageOn = true;
                    stackDefaultSearch.Opacity = 0;
                    stackDefaultSearch.IsVisible = false;
                    stackSearchResults.IsVisible = false;
                    stackDefaultSearch.IsVisible = true;
                    await stackDefaultSearch.FadeTo(1);
                    //await DeactivateCurrentSection();
                    //await ActivateSection(1);
                    isProcessEnabled = true;
                }                
            }
        }







        /// <summary>
        /// SECTIONS
        /// 1- INDEX
        /// 2- SEARCH SUGGESTIONS
        /// 3- RESTAURANTS
        /// </summary>
        /// <returns></returns>
        private async Task ActivateSection(int sectionNumber, bool isOpacityActive = true)
        {

            if ( sectionNumber == 1 )
            {
                stackDefaultSearch.IsVisible = true;
                await stackDefaultSearch.FadeTo(1);
                activeSectionNumber = 1;
            }
            else if ( sectionNumber == 2)
            {
                stackSearchResults.IsVisible = true;
                await stackSearchResults.FadeTo(1);
                activeSectionNumber = 2;
            }
            else if ( sectionNumber == 3)
            {
                stackResultsRestaurants.IsVisible = true;
                await stackResultsRestaurants.FadeTo(1);
                activeSectionNumber = 3;
            }
        }

        private async Task DeactivateCurrentSection()
        {
            if (activeSectionNumber == 1)
            {
                await stackDefaultSearch.FadeTo(0);
                stackDefaultSearch.IsVisible = false;
                
            }
            else if (activeSectionNumber == 2)
            {

                await stackSearchResults.FadeTo(0);
                stackSearchResults.IsVisible = false;
            }
            else if (activeSectionNumber == 3)
            {

                await stackResultsRestaurants.FadeTo(0);
                stackResultsRestaurants.IsVisible = false;              
            }
        }
        






    }
}