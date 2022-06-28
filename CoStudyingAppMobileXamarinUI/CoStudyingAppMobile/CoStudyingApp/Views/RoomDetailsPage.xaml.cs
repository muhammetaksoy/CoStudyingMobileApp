using CoStudyApp.Models.DBModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.MainTabbedPages;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomDetailsPage : ContentPage
    {
        RoomDetailsResponseModel roomDetails = new RoomDetailsResponseModel();
        IncreaseServices increaseServices = new IncreaseServices(); 
        public RoomDetailsPage(RoomDetailsResponseModel _roomDetails)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            roomDetails = _roomDetails;
            StartPage();
        }

        private class RoomDetailsPageUserViewModel
        {
            public User User { get; set; } 
            public int UserId { get; set; }
            public string FullName
            {
                get
                {
                    return User.Name + " " + User.Surname;
                }
            }

            public bool isAdmin { get; set; }

            public string ShieldIcon
            {
                get
                {
                    if (isAdmin)
                        return " (Yönetici) ";
                    return "";
                }
            
            }

            public string PhotoPath
            {

                get
                {
                    return User.ProfilePicturePath;
                }

            }

            public string DepartmentName { get; set; }
        }

        private async void StartPage()
        {

            lblGroupName.Text = roomDetails.Room.RoomName;
            if (!String.IsNullOrEmpty(roomDetails.Room.Description))
            {
                lblGroupDetailsTextHeader.IsVisible = true;
                lblGroupDetails.IsVisible = true;
                lblGroupDetails.Text = roomDetails.Room.Description;
            }
            lblGroupAccesCode.Text = roomDetails.Room.RoomAccessCode;
            if ( roomDetails.isMember)
            {
                stackMemberOptions.IsVisible = true;
                lblLeaveGroup.IsVisible = true;
                if (roomDetails.isAdmin)
                {
                    stackEditorOptions.IsVisible = true;
                }              
            }
            else
            {
                lblJoinGroup.IsVisible = true;
            }


            

            List<RoomDetailsPageUserViewModel> viewModels = new List<RoomDetailsPageUserViewModel>();
            foreach( UserRoom userRoom in roomDetails.Room.UserRooms.OrderBy(x=> x.CreatedOn))
            {
                RoomDetailsPageUserViewModel vm = new RoomDetailsPageUserViewModel()
                {
                    isAdmin = (userRoom.isAdmin ? true : false),
                    DepartmentName = userRoom.User.Department.DepartmentName,
                    UserId = userRoom.UserId,
                    User = userRoom.User,

                };
                viewModels.Add(vm);
            }
            listViewMembers.BindingContext = viewModels;
            listViewMembers.ItemsSource = viewModels;
            lblMembersHeader.Text = "Tüm üyeler (" + viewModels.Count + ")";

        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void tapSendInvite_Tapped(object sender, EventArgs e)
        {

        }

        private async void tapJoinGroup_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPage(true));
            bool resp = await increaseServices.JoinGroup(roomDetails.Room.Id);
            if ( resp)
            {
                await Navigation.PopAllPopupAsync();
                DisplayAlert("Başarılı", "Gruba katıldınız.", "Tamam");
                App.Current.MainPage = new NavigationPage(new MainTabbedPage());
            }
            else
            {
                await Navigation.PopAllPopupAsync();
                DisplayAlert("Uyarı", "Bir hata oluştu. Daha sonra tekrar deneyiniz.", "Tamam");
            }
        }

        private void tapCancelInvite_Tapped(object sender, EventArgs e)
        {

        }

        private void tapLeaveGroup_Tapped(object sender, EventArgs e)
        {

        }

        private void tapShowTasks_Tapped(object sender, EventArgs e)
        {

        }

        private void tapEditGroupDetails_Tapped(object sender, EventArgs e)
        {

        }

        private void tapShowMessages_Tapped(object sender, EventArgs e)
        {

        }

        private void listViewMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tapShowInvites_Tapped(object sender, EventArgs e)
        {

        }

        private async void tapShowAnnouncements_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAnnouncement(roomDetails.Room.Id));
        }
    }
}