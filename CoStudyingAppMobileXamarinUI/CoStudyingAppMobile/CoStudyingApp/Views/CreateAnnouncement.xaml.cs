using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.MainTabbedPages;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAnnouncement : ContentPage
    {
        int roomId = 0;
        IncreaseServices services = new IncreaseServices();
        public CreateAnnouncement(int _roomId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            roomId = _roomId;
        }




        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void tapCreate_Tapped(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(entryDescription.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanları doldurun.", "Tamam");
            }
            else
            {
                await Navigation.PushPopupAsync(new LoadingPage(true));
                bool resp = await services.CreateAnnouncement(roomId,entryDescription.Text);
                await Navigation.PopAllPopupAsync();
                if (resp)
                {
                    DisplayAlert("Başarılı", "Duyuru eklendi", "Tamam");
                    App.Current.MainPage = new NavigationPage(new MainTabbedPage());
                }
                else
                {
                    await DisplayAlert("Hata", "Bir sorun oluştu", "Tamam");
                }
            }
        }
    }
}