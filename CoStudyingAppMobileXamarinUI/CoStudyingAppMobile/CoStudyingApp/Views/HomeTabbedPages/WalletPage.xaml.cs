using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.BusinessModels;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.Models.ViewModels;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views.HomeTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletPage : ContentPage
    {
        List<WalletPagePreviousActionsViewModel> viewModels = new List<WalletPagePreviousActionsViewModel>();
        ExploreServices exploreServices = new ExploreServices();
        bool isAppeared = false;
        bool isCollectionEmpty = false;
        bool isProcessEnabled = true;
        
        public WalletPage()
        {
            InitializeComponent();
            this.Title = "Cüzdan";
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //double inWallet = CurrentUserStorage.GetUserWallet();
            //string inWalletStr = Math.Round(inWallet,2).ToString();
            //string inWalletStrUpdated = "";
            //if (inWalletStr.Contains("."))
            //    inWalletStr = inWalletStr.Replace('.', ',');
            //if ( !inWalletStr.Contains(","))
            //{
            //    inWalletStrUpdated = inWalletStr + ",00";
            //}
            //else
            //{
            //    int indexComma = inWalletStr.IndexOf(',');
            //    int afterComma = inWalletStr.Substring(indexComma).Length;
            //    if ( afterComma  == 1)
            //    {
            //        inWalletStrUpdated = inWalletStr + "0";
            //    }
            //    else
            //    {
            //        inWalletStrUpdated = inWalletStr;
            //    }
            //}
            //lblinWallet.Text = inWalletStrUpdated;
            //if( !isAppeared )
            //{
            //    isAppeared = true;
            //    await FillCollection();
            //}

        }

        private async Task FillCollection()
        {
            stackPreviousActivities.Opacity = 0;
            await LoadingState.StartLoading(stackPreviousActivities, stackLoading, lottieLoading);
            collectionViewPreviousActions.BindingContext = null;
            collectionViewPreviousActions.ItemsSource = null;
            viewModels.Clear();            
            List<PreviousActivitiesWalletResponseModel> responses = new List<PreviousActivitiesWalletResponseModel>();
            responses = await exploreServices.GetPreviousActivitiesWallet();
            if (responses == null || responses.Count == 0)
            {
                isCollectionEmpty = true;
                return;
            }
            else
            {
                isCollectionEmpty = false;
                responses = responses.OrderByDescending(x => x.CreatedOn).ToList();
            }
            foreach(PreviousActivitiesWalletResponseModel resp in responses )
            {
                viewModels.Add(new WalletPagePreviousActionsViewModel()
                {
                    responseModel = resp
                });
            }
            collectionViewPreviousActions.BindingContext = viewModels;
            collectionViewPreviousActions.ItemsSource = viewModels;
            await LoadingState.StopLoading(stackPreviousActivities, stackLoading, lottieLoading);
            await stackPreviousActivities.FadeTo(1, 300);
        }

        private async void tapGoProfile_Tapped(object sender, EventArgs e)
        {
            if ( isProcessEnabled )
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                WalletPagePreviousActionsViewModel vm = senderGrid.BindingContext as WalletPagePreviousActionsViewModel;
                await Navigation.PushAsync(new RestaurantProfilePage(vm.responseModel.RestaurantId), true);
                isProcessEnabled = true;
            }
        }


         


        

        
    }
}