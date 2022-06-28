using CoStudyApp.Models.DBModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.DBModels.CoStudyingDBModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRoomTasksPage : ContentPage
    {
        ExploreServices service = new ExploreServices();
        public UserRoomTasksPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            StartPage();
        }

        private async void StartPage()
        {
            await Navigation.PushPopupAsync(new LoadingPage());
            List<UserRoomTask> tasks = new List<UserRoomTask>();
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void tapAddTask_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Crea)
        }
    }
}