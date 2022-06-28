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
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views.HomeTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        IList<CampaignCardModelIndex> campaignViewModels = new ObservableCollection<CampaignCardModelIndex>();
        List<StorePageMenuItemsResponseModel> menuItemCategoriesModels = new List<StorePageMenuItemsResponseModel>();
        //List<RestaurantAndProductsCardModelIndex> dummyRestaurantProducts = new List<RestaurantAndProductsCardModelIndex>();
        ExploreServices exploreServices = new ExploreServices();
        bool isAppeared = false;
        bool isProcessEnabled = true;

        public StorePage()
        {
            InitializeComponent();
            this.Title = "Fırsatlar";
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if( !isAppeared)
            {
                isAppeared = true;
                await LoadingState.StartLoading(mainStack, stackLoading, lottieLoading);
                stackDailyCampaign.IsVisible = false;
                stackDailyCampaign.Opacity = 0;
                stackYeatyCampaigns.IsVisible = false;
                stackYeatyCampaigns.Opacity = 0;
                //stackProductsRestaurant1.IsVisible = false;
                //stackProductsRestaurant2.IsVisible = false;
                //stackProductsRestaurant3.IsVisible = false;
                //stackProductsRestaurant4.IsVisible = false;               
                bool isDailyCamp = await CheckDailyCampaign();
                await GetCampaingsIndex();
                await LoadingState.StopLoading(mainStack, stackLoading, lottieLoading);
                await Task.Delay(150);
                if (isDailyCamp)
                {
                    stackDailyCampaign.IsVisible = true;
                    await stackDailyCampaign.FadeTo(1,300);
                    await Task.Delay(150);                    
                }
                else
                {

                }
                stackYeatyCampaigns.IsVisible = true;
                await stackYeatyCampaigns.FadeTo(1, 300);
                await GetMenuItemCategories();
                if (menuItemCategoriesModels != null)
                {
                    if (menuItemCategoriesModels.Count > 0)
                    {
                        stackYeatyProducts.IsVisible = true;
                        await stackYeatyProducts.FadeTo(1, 300);
                    }
                }
            }
        }

        public async Task<bool> CheckDailyCampaign()
        {
            Campaign campaign = new Campaign();
            campaign = await exploreServices.GetRegularCampaign();
            if (campaign == null || campaign.Id == 0)
            {
                return false;
            }
            else
            {
                imgDailyCampaignBg.Source = campaign.BackgroundImageUrl;
                lblDailyCampaignTitle.Text = campaign.Text;
                lblDailyCampaignText.Text = campaign.SubText;
                return true;
            }
        }

        public async Task GetCampaingsIndex()
        {
            collectionViewCampaigns.BindingContext = null;
            collectionViewCampaigns.ItemsSource = null;
            List<CampaignIndexResponseModel> responseModels = new List<CampaignIndexResponseModel>();
            responseModels = await exploreServices.GetCampaignsIndex();
            if ( responseModels == null || responseModels.Count == 0)
            {
                return;
            }
            foreach(CampaignIndexResponseModel model in responseModels)
            {
                campaignViewModels.Add(new CampaignCardModelIndex()
                {
                    Campaign = model.Campaign
                });
            }
            collectionViewCampaigns.BindingContext = campaignViewModels;
            collectionViewCampaigns.ItemsSource = campaignViewModels;
        }

        public async Task GetMenuItemCategories()
        {
            menuItemCategoriesModels = await exploreServices.GetStorePageItemNumbers();
            if (menuItemCategoriesModels != null)
            {
                if (menuItemCategoriesModels.Count == 0)
                    return;
                if (menuItemCategoriesModels.Count > 0)
                {
                    frameMenuItems1.IsVisible = true;
                    if (!String.IsNullOrEmpty(menuItemCategoriesModels[0].CategoryPhotoLocalPath))
                        imgMenuItems1.Source = menuItemCategoriesModels[0].CategoryPhotoLocalPath;
                    else
                        imgMenuItems1.Source = menuItemCategoriesModels[0].CategoryPhotoUrl;
                    lblMenuItemsTitle1.Text = menuItemCategoriesModels[0].MenuItemsTitle;
                    lblNumberItems1.Text = menuItemCategoriesModels[0].NumberMenuItems.ToString() + "+";
                    lblNumberRestaurants1.Text = menuItemCategoriesModels[0].MenuItemsRestaurantCount.ToString() + "+";
                }
                if (menuItemCategoriesModels.Count > 1)
                {
                    frameMenuItems2.IsVisible = true;
                    if (!String.IsNullOrEmpty(menuItemCategoriesModels[1].CategoryPhotoLocalPath))
                        imgMenuItems2.Source = menuItemCategoriesModels[1].CategoryPhotoLocalPath;
                    else
                        imgMenuItems2.Source = menuItemCategoriesModels[1].CategoryPhotoUrl;
                    lblMenuItemsTitle2.Text = menuItemCategoriesModels[1].MenuItemsTitle;
                    lblNumberItems2.Text = menuItemCategoriesModels[1].NumberMenuItems.ToString() + "+";
                    lblNumberRestaurants2.Text = menuItemCategoriesModels[1].MenuItemsRestaurantCount.ToString() + "+";
                }
                if (menuItemCategoriesModels.Count > 2)
                {
                    frameMenuItems3.IsVisible = true;
                    if (!String.IsNullOrEmpty(menuItemCategoriesModels[2].CategoryPhotoLocalPath))
                        imgMenuItems3.Source = menuItemCategoriesModels[2].CategoryPhotoLocalPath;
                    else
                        imgMenuItems3.Source = menuItemCategoriesModels[2].CategoryPhotoUrl;
                    lblMenuItemsTitle3.Text = menuItemCategoriesModels[2].MenuItemsTitle;
                    lblNumberItems3.Text = menuItemCategoriesModels[2].NumberMenuItems.ToString() + "+";
                    lblNumberRestaurants3.Text = menuItemCategoriesModels[2].MenuItemsRestaurantCount.ToString() + "+";
                }
                if (menuItemCategoriesModels.Count > 3)
                {
                    frameMenuItems4.IsVisible = true;
                    if (!String.IsNullOrEmpty(menuItemCategoriesModels[3].CategoryPhotoLocalPath))
                        imgMenuItems4.Source = menuItemCategoriesModels[3].CategoryPhotoLocalPath;
                    else
                        imgMenuItems4.Source = menuItemCategoriesModels[3].CategoryPhotoUrl;
                    lblMenuItemsTitle4.Text = menuItemCategoriesModels[3].MenuItemsTitle;
                    lblNumberItems4.Text = menuItemCategoriesModels[3].NumberMenuItems.ToString() + "+";
                    lblNumberRestaurants4.Text = menuItemCategoriesModels[3].MenuItemsRestaurantCount.ToString() + "+";
                }
                if (menuItemCategoriesModels.Count > 4)
                {
                    frameMenuItems5.IsVisible = true;
                    if (!String.IsNullOrEmpty(menuItemCategoriesModels[4].CategoryPhotoLocalPath))
                        imgMenuItems5.Source = menuItemCategoriesModels[4].CategoryPhotoLocalPath;
                    else
                        imgMenuItems5.Source = menuItemCategoriesModels[4].CategoryPhotoUrl;
                    lblMenuItemsTitle5.Text = menuItemCategoriesModels[4].MenuItemsTitle;
                    lblNumberItems5.Text = menuItemCategoriesModels[4].NumberMenuItems.ToString() + "+";
                    lblNumberRestaurants5.Text = menuItemCategoriesModels[4].MenuItemsRestaurantCount.ToString() + "+";
                }
                if (menuItemCategoriesModels.Count > 5)
                {
                    frameMenuItems6.IsVisible = true;
                    if (!String.IsNullOrEmpty(menuItemCategoriesModels[5].CategoryPhotoLocalPath))
                        imgMenuItems6.Source = menuItemCategoriesModels[5].CategoryPhotoLocalPath;
                    else
                        imgMenuItems6.Source = menuItemCategoriesModels[5].CategoryPhotoUrl;
                    lblMenuItemsTitle6.Text = menuItemCategoriesModels[5].MenuItemsTitle;
                    lblNumberItems6.Text = menuItemCategoriesModels[5].NumberMenuItems.ToString() + "+";
                    lblNumberRestaurants6.Text = menuItemCategoriesModels[5].MenuItemsRestaurantCount.ToString() + "+";
                }
            }
        }


        private void tapFavv_Tapped(object sender, EventArgs e)
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

        private async void tapCategory_Tapped(object sender, EventArgs e)
        {
            Frame senderFrame = sender as Frame;
            int classId = Convert.ToInt32(senderFrame.ClassId);
            StorePageMenuItemsResponseModel selected = menuItemCategoriesModels[classId - 1];
            //TODO
        }

        private async void tapSelectCampaign_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                CampaignCardModelIndex vm = senderGrid.BindingContext as CampaignCardModelIndex;
                await Navigation.PushPopupAsync(new PurchaseCampaignPopupPage(vm.Campaign.Id, true));
                isProcessEnabled = true;
            }
        }
    }
}