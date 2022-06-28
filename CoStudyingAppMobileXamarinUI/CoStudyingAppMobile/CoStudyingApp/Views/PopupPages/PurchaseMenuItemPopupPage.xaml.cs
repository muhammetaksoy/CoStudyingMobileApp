using Entities;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.ServiceController;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace YeatyAppMobile.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseMenuItemPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        Entities.MenuItem menuItem = new Entities.MenuItem();
        PurchaseServices purchaseServices = new PurchaseServices();
        Restaurant restaurant = new Restaurant();
        bool isProgressEnabled = true;
        QRServices qrService = new QRServices();
        int timer = 0;
        bool isTimerRunning = false;
        bool isDirectQR = false;
        int purchaseMenuItemId = 0; // after purchaseComplete


        public PurchaseMenuItemPopupPage(Entities.MenuItem _menuItem, Restaurant _restaurant, bool _isDirectQR = false, int purchaseId = 0)
        {
            InitializeComponent();
            isDirectQR = _isDirectQR;
            if ( isDirectQR)
            {               
                purchaseMenuItemId = purchaseId;
            }
            else
            {
                menuItem = _menuItem;
                restaurant = _restaurant;
            }
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!isDirectQR)
                await AttemptBuyItem();
            else
            {
                await GetPurchase();
            }

        }

        private async Task GetPurchase()
        {
            await GetQRInfos(true);
            await stackLoading.FadeTo(0);
            stackLoading.IsVisible = false;
            stackMainQR.IsVisible = true;
            await stackMainQR.FadeTo(1);
            isProgressEnabled = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<PurchaseMenuItemPopupPage>(this, "PurchaseComplete");
        }

        private async Task AttemptBuyItem()
        {
            PurchaseMenuItemResponseModel response = await purchaseServices.PurchaseMenuItemByQR(menuItem.Id, menuItem.Price);
            if ( response.Status == PurchaseMenuItemResponseCodes.NoBalance)
            {
                //purchaseMenuItemId = response.PurchaseMenuItem.Id;
                string needed = response.NeededYeatyPoints.ToString();
                string lastPart = "";
                string firstPart = "";
                int indexComma = 0;
                if (needed.Contains(","))
                    indexComma = needed.IndexOf(",");
                else if ( needed.Contains(".") )
                    indexComma = needed.IndexOf(".");
                if ( indexComma != 0)
                {
                    firstPart = needed.Substring(0, indexComma);
                    lastPart = needed.Substring(indexComma + 1);
                    if (lastPart == "0")
                        needed = firstPart;
                    else if (lastPart.Length == 1)
                        needed = firstPart + "," + lastPart + "0";
                    else
                        needed = firstPart + "," + lastPart;
                }
                lblNeededYeatyPoints.Text = needed;
                await stackLoading.FadeTo(0);
                lottieLoading.StopAnimation();
                stackLoading.IsVisible = false;
                stackNotEnoughBalance.IsVisible = true;
                await stackNotEnoughBalance.FadeTo(1);

            }
            else
            {
                purchaseMenuItemId = response.PurchaseMenuItem.Id;
                lblCompletedItemName.Text = menuItem.Name;
                lblRestaurantNameItem.Text = restaurant.RestaurantName;
                imgCompletedItemImage.Source = menuItem.PhotoPath;
                await stackLoading.FadeTo(0);
                lottieLoading.StopAnimation();
                stackLoading.IsVisible = false;
                await Task.Delay(200);
                stackSuccessAnimation.IsVisible = true;
                //CurrentUserStorage.UpdateUserWallet(0-menuItem.Price);
                lottieSuccess.PlayAnimation();
                
            }
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            if (isProgressEnabled)
            {
                isProgressEnabled = false;
                await Navigation.PopPopupAsync();
                stackLoading.IsVisible = true;
            }               
        }

        private async void createQRNow_Tapped(object sender, EventArgs e)
        {
            if( isProgressEnabled )
            {
                isProgressEnabled = false;
                await stackCompleted.FadeTo(0);
                stackCompleted.IsVisible = false;
                lottieLoading.PlayAnimation();
                stackLoading.IsVisible = true;
                await stackLoading.FadeTo(1);
                await GetQRInfos(true);
                await stackLoading.FadeTo(0);
                stackLoading.IsVisible = false;
                stackMainQR.IsVisible = true;
                await stackMainQR.FadeTo(1);
                isProgressEnabled = true;

            }
        }

        async Task GetQRInfos(bool isLast)
        {
            string barcodeValue = "";
            barcodeValue = await qrService.CreatePurchaseMenuItemQR(purchaseMenuItemId, isLast);

            if (String.IsNullOrEmpty(barcodeValue))
            {
                await DisplayAlert("Hata", "Bir hata oluştu. Daha sonra deneyiniz.", "OK");
                return;
            }
            lblQRSubText.Text = "Yeaty bakiyesi ile ödenen " + menuItem.Name + " ürününüzü sipariş etmek için hemen QR kodu yetkiliye gösterin.";
            //QRStorage.QRCreated(barcodeValue);
            StartTimer();
            RunTimer();
            TimeSpan diff = new TimeSpan(0, 10, 0);
            timer = Convert.ToInt32(diff.TotalSeconds);
            stackQR.Children.Add(new ZXingBarcodeImageView
            {
                BarcodeFormat = ZXing.BarcodeFormat.QR_CODE,
                BarcodeValue = barcodeValue,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Width = 200,
                    Height = 200
                },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 250,
                HeightRequest = 250,
                ClassId = "QRCODE"
            });
        }

        private async void createLater_Tapped(object sender, EventArgs e)
        {
            if (isProgressEnabled)
            {
                isProgressEnabled = false;
                await Navigation.PopPopupAsync();
            }
        }

        private async void lottieSuccess_OnFinishedAnimation(object sender, EventArgs e)
        {
            UpdateStorage.ChangeUpdateSituationOwnedQR(true);
            await Task.Delay(200);
            await stackSuccessAnimation.FadeTo(0);
            stackSuccessAnimation.IsVisible = false;
            stackCompleted.IsVisible = true;
            await Task.WhenAny(
                lblTextSuccess.FadeTo(1),
                gridImageMenuItem.FadeTo(1)
                
                );
            await stackButtons.FadeTo(1);

        }

        private async void tapNewQR_Tapped(object sender, EventArgs e)
        {

        }


        #region TimerMethods
        private async void StartTimer()
        {
            //Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            //{

            //    if (isTimerRunning && timer > 0)
            //    {
            //        timer--;
            //        lblTimer.Text = timer.ToString();
            //        if (timer < 5)
            //        {
            //            timerFrame.BackgroundColor = CustomColors.defaultRedColor;
            //            lblTimer.TextColor = CustomColors.defaultRedColor;
            //        }

            //    }
            //    else
            //    {

            //    }
            //    return true;

            //});
            Device.StartTimer(new TimeSpan(0, 0, 1), OnTimerClick);
        }

        private bool OnTimerClick()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (isTimerRunning && timer > 0)
                {
                    timer--;
                    //await lblTimer.FadeTo(0, 10);
                    ChangeTimerText();
                    //await lblTimer.FadeTo(1, 10);


                }
                else if (isTimerRunning && timer >= 0)
                {
                    TimeOut();
                }
            });
            return true;
        }

        private void TimeOut()
        {
            StopTimer();
            frameTimer.IsVisible = false;
            frameNewQr.IsVisible = true;
            lblTimerSituation.Text = "Süre doldu.Lütfen yeni bir kod üretin.";
        }

        private void StopTimer()
        {
            isTimerRunning = false;
        }



        private void RunTimer()
        {
            isTimerRunning = true;
        }


        private void ChangeTimerText()
        {
            int totalSecs = timer;
            TimeSpan timeSpan = new TimeSpan(0, 0, totalSecs);
            int mins = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;
            string minstr = "";
            string secsStr = "";
            if (seconds < 10)
                secsStr = "0" + seconds.ToString();
            else
                secsStr = seconds.ToString();
            if (mins >= 10)
            {
                minstr = mins.ToString();
            }
            else if (mins >= 0)
            {
                minstr = "0" + mins.ToString();
            }
            lblTimer.Text = minstr + ":" + secsStr;
        }
        #endregion
    }
}