using Rg.Plugins.Popup.Extensions;
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
using YeatyAppMobile.ViewCells.CodeBased;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views.HomeTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyQRCodesPage : ContentPage
    {
        List<OwnedQRCodeViewModel> ownedQRCodeViewModels = new List<OwnedQRCodeViewModel>();
        List<OwnedQRCodeViewCell> ownedQRCodeViewCells = new List<OwnedQRCodeViewCell>();
        ExploreServices exploreServices = new ExploreServices();
        List<QRCodesIndexResponseModel> responseModels = new List<QRCodesIndexResponseModel>();
        int countQRS = 0;
        bool isProcessEnabled = true;
        bool isAppeared = false;
        public MyQRCodesPage()
        {
            InitializeComponent();
            this.Title = "Kodlarım";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if ( isAppeared != true )
            {
                isAppeared = true;
                await LoadingState.StartLoading(stackMyCodes, stackLoading, lottieLoading);
                await FillList();
                if (responseModels == null)
                {
                    stackNoQr.IsVisible = true;
                }
                else if (responseModels.Count == 0)
                {
                    stackNoQr.IsVisible = true;
                }
                else
                {
                    stackNoQr.IsVisible = false;
                }
                await LoadingState.StopLoading(stackMyCodes, stackLoading, lottieLoading);
                UpdateStorage.ChangeUpdateSituationOwnedQR(false);

            }
            else if ( UpdateStorage.isUpdateRequiredOwnedQR())
            {
                await LoadingState.StartLoading(stackMyCodes, stackLoading, lottieLoading);
                ownedQRCodeViewModels.Clear();
                ownedQRCodeViewCells.Clear();
                gridQRList.Children.Clear();
                gridQRList.RowDefinitions.Clear();
                gridQRList.ColumnDefinitions.Clear();
                await FillList();
                if (responseModels == null)
                {
                    stackNoQr.IsVisible = true;
                }
                else if (responseModels.Count == 0)
                {
                    stackNoQr.IsVisible = true;
                }
                else
                {
                    stackNoQr.IsVisible = false;
                }
                await LoadingState.StopLoading(stackMyCodes, stackLoading, lottieLoading);
                UpdateStorage.ChangeUpdateSituationOwnedQR(false);
            }

            //ownedQRCodeViewModels.Add(new OwnedQRCodeViewModel()
            //{
            //    ExpireDate = new DateTime(2020,9,29),
            //    PhotoUrl = "https://media-cdn.tripadvisor.com/media/photo-s/09/65/59/5a/kulup-bahce-kafe.jpg",
            //    isCustomCampaign = true,
            //    isGeneralCampaign = false,
            //    isMenuItem = false,
            //    ImageText = "Kampanya",
            //    RestaurantName = "Kahve Tiryakisi",
            //    SubText = "50 lira ve üstü hesap tutarında %50 indirim"



            //});
            //ownedQRCodeViewModels.Add(new OwnedQRCodeViewModel()
            //{
            //    ExpireDate = new DateTime(2000, 1, 1),
            //    PhotoUrl = "cappucino.jpg",
            //    isCustomCampaign = false,
            //    isGeneralCampaign = false,
            //    isMenuItem = true,
            //    ImageText = "Cappucino",
            //    RestaurantName = "De'la Carte Cafe",
            //    SubText = "Yeaty puanlar ile ödenmiş bir adet Cappucino",
                



            //});
            //foreach(OwnedQRCodeViewModel vm in ownedQRCodeViewModels)
            //{
            //    OwnedQRCodeViewCell viewCell = new OwnedQRCodeViewCell(vm);
            //    ownedQRCodeViewCells.Add(viewCell);
            //    stackQRList.Children.Add(viewCell.mainFrame);
            //}

        }

        private async Task FillList()
        {
            responseModels = await exploreServices.GetOwnedItems();
            if (responseModels != null || responseModels.Count > 0)
            {
                foreach (QRCodesIndexResponseModel resp in responseModels)
                {
                    ownedQRCodeViewModels.Add(new OwnedQRCodeViewModel()
                    {
                        SubText = resp.SubTitle,
                        PhotoUrl = resp.PhotoPath,
                        isCampaign = resp.isPurchaseCampaign,
                        isMenuItem = resp.isPurchaseMenuItem,
                        purchaseId = resp.purchaseId,
                        RestaurantName = resp.RestaurantName,
                        Title = resp.Title,
                        campaignId = resp.campaignId
                    });
                }
                gridQRList.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                gridQRList.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                countQRS = ownedQRCodeViewModels.Count;
                //collectionViewItems.BindingContext = ownedQRCodeViewModels;
                //collectionViewItems.ItemsSource = ownedQRCodeViewModels;
                int neededRows = (countQRS / 2) + 1;

                for (int i = 0; i < neededRows; i++)
                {
                    gridQRList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
                int counter = 0;
                int turn = -1;
                foreach (OwnedQRCodeViewModel vm in ownedQRCodeViewModels)
                {
                    OwnedQRCodeViewCell viewCell = new OwnedQRCodeViewCell(vm);
                    ownedQRCodeViewCells.Add(viewCell);
                    Grid mainGrid = viewCell.mainGrid;
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += tapItem_Tapped;
                    mainGrid.GestureRecognizers.Add(tapGestureRecognizer);
                    if (counter % 2 == 0)
                    {
                        turn++;
                        gridQRList.Children.Add(mainGrid, 0, turn);
                    }
                    else
                        gridQRList.Children.Add(mainGrid, 1, turn);
                    counter++;
                }

            }
            return;



        }

        private async void tapCreatePaybackQR_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QRPages.PaybackQRCodePage());
        }

        private void frameGoToCampaigns_Clicked(object sender, EventArgs e)
        {
            //TODO
        }

        private async void tapItem_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                Grid senderGrid = sender as Grid;
                OwnedQRCodeViewModel vm = senderGrid.BindingContext as OwnedQRCodeViewModel;
                if (vm.isCampaign)
                    await Navigation.PushPopupAsync(new PurchaseCampaignPopupPage(vm.campaignId));
                else if (vm.isMenuItem)
                    await Navigation.PushPopupAsync(new PurchaseMenuItemPopupPage(null, null, true, vm.purchaseId));
                isProcessEnabled = true;
            }
        

        }
    }
}