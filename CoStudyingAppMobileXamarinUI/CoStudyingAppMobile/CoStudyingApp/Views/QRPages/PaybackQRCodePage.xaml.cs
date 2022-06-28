using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.BusinessModels;
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.ServiceController;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace YeatyAppMobile.Views.QRPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaybackQRCodePage : ContentPage
    {
        //SignalrClient client = new SignalrClient();
        string signalRPaymentUsername;
        QRServices qrService = new QRServices();
        int timer = 0;
        bool isTimerRunning = false;
        public PaybackQRCodePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //signalRPaymentUsername = LocalDataUtil.Username + "_PaymentClient";
            //client.ConnectPayment(signalRPaymentUsername);
            //client.OnPaymentMessageReceived += Client_OnPaymentMessageReceived;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            StartTimer();
            await LoadingState.StartLoading(mainStack, stackLoading, lottieLoading);
            await GetPaymentInfos();
            await LoadingState.StopLoading(mainStack, stackLoading, lottieLoading);
            await mainStack.FadeTo(1, 400);
        }

        private async void RequestNewCode()
        {

            await LoadingState.StartLoading(mainStack, stackLoading, lottieLoading);
            View vieww = stackQR.Children.Where(x => x.ClassId == "QRCODE").FirstOrDefault();
            if( vieww != null )
                stackQR.Children.Remove(vieww);
            await GetPaymentInfos();
            await LoadingState.StopLoading(mainStack, stackLoading, lottieLoading);
            await mainStack.FadeTo(1, 400);
        }

        async Task GetPaymentInfos()
        {
            string barcodeValue = "";
            QRStorage.QRTimeCheck();
            QRModel existedQR = QRStorage.GetQRModel();
            if( existedQR == null || String.IsNullOrEmpty(existedQR.HashedPassCode))
            {
                barcodeValue = await qrService.CreatePaybackQR();
               
                if (String.IsNullOrEmpty(barcodeValue))
                {
                    await DisplayAlert("Hata", "Bir hata oluştu. Daha sonra deneyiniz.", "OK");
                    return;
                }
                QRStorage.QRCreated(barcodeValue);
            }
            else
            {
                barcodeValue = existedQR.HashedPassCode;
            }
            RunTimer();
            TimeSpan diff = QRStorage.GetQRModel().FinishOn.Subtract(DateTime.Now);
            timer = Convert.ToInt32(diff.TotalSeconds);
            stackQR.Children.Add(new ZXingBarcodeImageView
            {
                BarcodeFormat = ZXing.BarcodeFormat.QR_CODE,
                BarcodeValue = barcodeValue,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Width = 250,
                    Height = 250
                },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 250,
                HeightRequest = 250,
                ClassId = "QRCODE"
            });
        }


        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void tapNewQR_Tapped(object sender, EventArgs e)
        {
            await mainStack.FadeTo(0, 300);
            RequestNewCode();
        }


        //private async void Client_OnPaymentMessageReceived(SignalrPaymentMessage paymentMessage)
        //{
        //    try
        //    {
        //        int restaurantId = Convert.ToInt32(paymentMessage.restaurantId);
        //        //int lengthOfMessage = purchaseMessage.Message.Length;
        //        //int restaurantId;
        //        //string restaurantIdStr = purchaseMessage.Message.
        //        if (signalRPaymentUsername == paymentMessage.ReceiverUser)
        //        {
        //            Device.BeginInvokeOnMainThread(() =>
        //            {
        //                PopupNavigation.Instance.PushAsync(new OrderCompletePage(restaurantId));
        //            });

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string err = ex.Message;
        //        throw;
        //    }

        //}

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