using Entities;
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
using YeatyAppMobile.ServiceController;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace YeatyAppMobile.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseCampaignPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        PurchaseServices purchaseServices = new PurchaseServices();
        CampaignDetailsResponseModel campaignDetails = new CampaignDetailsResponseModel();
        Campaign campaign = new Campaign();
        ExploreServices exploreServices = new ExploreServices();
        Restaurant restaurant = new Restaurant();
        bool isProgressEnabled = true;
        QRServices qrService = new QRServices();
        int timer = 0;
        int campaignId = 0;
        bool isTimerRunning = false;
        bool isRedirectToRestaurantProfileEnabled = false;
        int purchaseMenuItemId = 0; // after purchaseComplete


        public PurchaseCampaignPopupPage(int _campaignId, bool _isRedirectToRestaurantProfileEnabled = false)
        {
            InitializeComponent();
            campaignId = _campaignId ;
            isRedirectToRestaurantProfileEnabled = _isRedirectToRestaurantProfileEnabled;

        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetCampaignDetails();
        }

        async Task GetCampaignDetails()
        {

            campaignDetails = await exploreServices.GetCampaignDetails(campaignId);
            if ( campaignDetails == null )
            {
                lblWarningTitle.Text = "Kampanya bulunamadı.";
                lblWarningText.Text = "Aradığınız kampanya kaldırılmış yada artık erişilebilir değil.";
                await StopWaiting();
                await StartWarningSection();
                return;
            }
            campaign = campaignDetails.Campaign;
            if ( campaignDetails.isOutOfDate  )
            {
                lblWarningTitle.Text = "Kampanya süresi doldu";
                lblWarningText.Text = "Katılmaya çalıştığınız kampanyanın süresi dolmuş";
                await StopWaiting();
                await StartWarningSection();
            }
            else if( campaignDetails.isPurchased)
            {
                if( campaignDetails.isUsed)
                {
                    lblWarningTitle.Text = "Kampanya kullanılmış.";
                    lblWarningText.Text = "Yeaty özel kampanyalarına günde 1 kez katılım gerçekleştirebilirsiniz. Kampanyaya yarın tekrar katılım gösterebilirsiniz"; ;
                    await StopWaiting();
                    await StartWarningSection();
                }
                else
                {
                    if (isProgressEnabled)
                    {
                        isProgressEnabled = false;
                        await GetQRInfos();
                        await StopWaiting();
                        await StartMainQRSection();
                        isProgressEnabled = true;
                    }
                }
              
            }
            else
            {
                await StopWaiting();
                await StartDetailsSection();
            }
            

        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            if (isProgressEnabled)
            {
                isProgressEnabled = false;
                await Navigation.PopPopupAsync();
            }
        }

        private async void createQRNow_Tapped(object sender, EventArgs e)
        {
            if (isProgressEnabled)
            {
                isProgressEnabled = false;
                await StopCompletedSection();
                await StartWaiting();
                await GetQRInfos();
                await StopWaiting();
                await StartMainQRSection();                
                isProgressEnabled = true;

            }
        }

        async Task GetQRInfos()
        {
            string barcodeValue = "";
            barcodeValue = await qrService.CreatePurchaseMenuItemQR(purchaseMenuItemId, true);

            if (String.IsNullOrEmpty(barcodeValue))
            {
                await DisplayAlert("Hata", "Bir hata oluştu. Daha sonra deneyiniz.", "OK");
                return;
            }
            lblQRSubText.Text = "Kampanyadan hemen faydalanmak için QR kodu yetkiliye gösterin.";
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

        private async void tapNewQR_Tapped(object sender, EventArgs e)
        {

        }

        private async void tapJoinCampaign_Tapped(object sender, EventArgs e)
        {
            await StopDetailsSection();
            await StartWaiting();
            PurchaseCampaignResponseModel respModel = await purchaseServices.PurchaseCampaignByQR(campaignId);
            if ( respModel.isSuccess == false )
            {
                if ( respModel.Status == PurchaseCampaignResponseCodes.AlreadyHave)
                {
                    lblWarningTitle.Text = "Kampanya kullanılmış.";
                    lblWarningText.Text = "Yeaty özel kampanyalarına günde 1 kez katılım gerçekleştirebilirsiniz. Kampanyaya yarın tekrar katılım gösterebilirsiniz";
                }
                else if ( respModel.Status == PurchaseCampaignResponseCodes.AlreadyUsed)
                {
                    lblWarningTitle.Text = "Kampanya kullanılmış.";
                    lblWarningText.Text = "Yeaty özel kampanyalarına günde 1 kez katılım gerçekleştirebilirsiniz. Kampanyaya yarın tekrar katılım gösterebilirsiniz"; 
                }
                else if(respModel.Status == PurchaseCampaignResponseCodes.OutOfDate)
                {
                    lblWarningTitle.Text = "Kampanya süresi dolmuş.";
                    lblWarningText.Text = "Bu kampanyaya artık katılım sağlayamazsınız. Başka Yeaty özel kampanyalarından faydalanmaya devame edebilirsiniz.";

                }
                else if  ( respModel.Status == PurchaseCampaignResponseCodes.StokFinished)
                {
                    lblWarningTitle.Text = "Stok yetersiz";
                    lblWarningText.Text = "Bu kampanyaya artık katılım sağlayamazsınız.";
                }
                await StopWaiting();
                await StartWarningSection();

            }
            else
            {
                await StopWaiting();
                await StartCompletedAnimation();
            }
        }

        private async void lottieSuccess_OnFinishedAnimation(object sender, EventArgs e)
        {
            UpdateStorage.ChangeUpdateSituationOwnedQR(true);
            await StopCompletedAnimation();
        }


        #region STATE CONTROL

        async Task StopWaiting()
        {
            await stackLoading.FadeTo(0);
            //lottieLoading.StopAnimation();
            stackLoading.IsVisible = false;
        }

        async Task StartWaiting()
        {
            stackLoading.IsVisible = true;
            await stackLoading.FadeTo(1);
            //lottieLoading.StopAnimation();        
        }

        async Task StartCompletedAnimation()
        {
            //await Task.Delay(200);
            stackSuccessAnimation.IsVisible = true;
            lottieSuccess.PlayAnimation();
        }

        async Task StopCompletedAnimation()
        {
            await Task.Delay(200);
            await stackSuccessAnimation.FadeTo(0);
            stackSuccessAnimation.IsVisible = false;
            await StartCompletedSection();
        }

        //async Task StopCompleted()
        //{
        //    stackSuccessAnimation.IsV
        //}

        async Task StartWarningSection()
        {
            stackWarning.IsVisible = true;
            await stackWarning.FadeTo(1);
        }

        async Task StopWarningSection()
        {
            await stackWarning.FadeTo(0);
            stackWarning.IsVisible = false;
          
        }

        async Task StartDetailsSection()
        {
            lblCampaignDetails.Text = campaign.SubText;
            lblRestaurantNameCampaign.Text = campaign.Restaurant.RestaurantName;
            imgRestaurantProfileCampaingDetails.Source = campaign.Restaurant.ProfilePicturePath;
            lblRestaurantLocation.Text = campaign.Restaurant.City + ", " + campaign.Restaurant.District;
            lblCampaignLastUsageDate.Text = "Son katılım tarihi: " + DateTimeToString.ConvertDateTimeToStr(campaign.FinishingDate);
            
            stackCampaignDetails.IsVisible = true;
            await stackCampaignDetails.FadeTo(1);
        }

        async Task StopDetailsSection()
        {
            await stackCampaignDetails.FadeTo(0);
            stackCampaignDetails.IsVisible = false;          
        }

        async Task StartCompletedSection()
        {
            imgCompletedItemImage.Source = campaign.Restaurant.ProfilePicturePath;
            lblCompletedCampaignText.Text = campaign.SubText;
            lblRestaurantNameItem.Text = campaign.Restaurant.RestaurantName;
            stackCompleted.IsVisible = true;
            await stackCompleted.FadeTo(1);
            await Task.WhenAny(
                lblTextSuccess.FadeTo(1),
                gridImageCampaign.FadeTo(1)
                );
            await stackButtons.FadeTo(1);
        }

        async Task StopCompletedSection()
        {
            await stackCompleted.FadeTo(0);
            stackCompleted.IsVisible = false;

        }

        async Task StartMainQRSection()
        {
            stackMainQR.IsVisible = true;
            await stackMainQR.FadeTo(1);
        }

        async Task StopMainQRSection()
        {
            await stackMainQR.FadeTo(0);
            stackMainQR.IsVisible = false;           
        }



        #endregion


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

        private async void tapGoToRestaurantProfile_Tapped(object sender, EventArgs e)
        {
            if (isProgressEnabled && isRedirectToRestaurantProfileEnabled)
            {
                isProgressEnabled = false;
                await Navigation.PushAsync(new RestaurantProfilePage(campaign.Restaurant.Id),true);
                await Navigation.PopAllPopupAsync();
            }
        }
    }
}