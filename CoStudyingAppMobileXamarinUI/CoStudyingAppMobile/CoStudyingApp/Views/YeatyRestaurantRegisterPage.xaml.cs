using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YeatyRestaurantRegisterPage : ContentPage
    {
        bool isProcessEnabled = true;
        IncreaseServices increaseServices = new IncreaseServices();
        public YeatyRestaurantRegisterPage()
        {
            InitializeComponent();
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            if (isProcessEnabled)
            {
                isProcessEnabled = false;
                await Navigation.PopAsync();
            }
        }

        private async void tapSend_Tapped(object sender, EventArgs e)
        {
            if ( String.IsNullOrEmpty(entryRestaurantContact.Text) || String.IsNullOrEmpty(entryRestaurantLocation.Text) || String.IsNullOrEmpty(entryRestaurantName.Text)) 
            {
                await DisplayAlert("Hata", "Lütfen tüm alanları doldurunuz", "Tamam");                     
            }

        }
    }
}