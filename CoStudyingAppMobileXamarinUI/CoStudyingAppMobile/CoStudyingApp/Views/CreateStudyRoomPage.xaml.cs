using CoStudyApp.Models.DBModels;
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
    public partial class CreateStudyRoomPage : ContentPage
    {

        IncreaseServices increaseServices = new IncreaseServices();
        bool flagProcess = true;
        public CreateStudyRoomPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void checkStudyPrivate_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private async void tapCreate_Tapped(object sender, EventArgs e)
        {
            if (flagProcess)
            {
                if (String.IsNullOrEmpty(entryRoomName.Text))
                {
                    await DisplayAlert("Uyarı", "Lütfen bir oda ismi belirleyin.", "Tamam");
                }
                else
                {
                    flagProcess = false;
                    await Navigation.PushPopupAsync(new LoadingPage());
                    Room room = await increaseServices.CreateRoom(entryRoomName.Text, checkStudyPrivate.IsChecked, entryRoomDescription.Text);
                    if (room != null)
                    {
                        await Navigation.PopAllPopupAsync();
                        DisplayAlert("Başarılı", "Çalışma odası oluşturuldu", "Tamam");
                        //TODO : Çalışma odası detay sayfasına yönlendir.
                        flagProcess = true;
                        App.Current.MainPage = new NavigationPage(new MainTabbedPage());
                    }
                    else
                    {
                        await Navigation.PopAllPopupAsync();
                        await DisplayAlert("Hata", "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.", "Tamam");
                        flagProcess = true;
                    }
                }
            }

        }
    }
}