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
using YeatyAppMobile.Models.StorageModels;
using YeatyAppMobile.ServiceController;
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views.MainTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {
        List<UserRoomResponseModel> myUserRooms;
        ExploreServices exploreServices = new ExploreServices();
        public FeedPage()
        {
            InitializeComponent();
            this.Title = "Akış";
            User loggedUser = CurrentUserStorage.GetCurrentUser();
            spanUserFirstName.Text = loggedUser.Name + ", ";
            StartPage();
        }

        public async void StartPage()
        {
            await System.Threading.Tasks.Task.Delay(500);
            await stackWelcomeText.FadeTo(1,400);          
            myUserRooms = await exploreServices.GetMyUserRooms();
            if ( myUserRooms != null)
            {
                if (myUserRooms.Count > 0)
                {
                    scrollViewMyGroups.IsVisible = true;
                    stackGroupsHeader.IsVisible = true;
                    lblStudyGroupName1.Text = myUserRooms[0].UserRoom.Room.RoomName;
                    lblStudyGroupUserCount1.Text = myUserRooms[0].numberOfUsers + " üyeler";
                    frameSelectionTab1.IsVisible = true;
                }
                if (myUserRooms.Count > 1)
                {
                    lblStudyGroupName2.Text = myUserRooms[1].UserRoom.Room.RoomName;
                    frameSelectionTab2.IsVisible = true;
                    lblStudyGroupUserCount2.Text = myUserRooms[1].numberOfUsers + " üyeler";
                }
                if (myUserRooms.Count > 2)
                {
                    lblStudyGroupName3.Text = myUserRooms[2].UserRoom.Room.RoomName;
                    lblStudyGroupUserCount3.Text = myUserRooms[2].numberOfUsers + " üyeler";
                    frameSelectionTab3.IsVisible = true;
                }
                if (myUserRooms.Count > 3)
                {
                    lblStudyGroupName4.Text = myUserRooms[3].UserRoom.Room.RoomName;
                    lblStudyGroupUserCount4.Text = myUserRooms[3].numberOfUsers + " üyeler";
                    frameSelectionTab4.IsVisible = true;
                }
                if (myUserRooms.Count > 4)
                {
                    lblStudyGroupName5.Text = myUserRooms[4].UserRoom.Room.RoomName;
                    lblStudyGroupUserCount5.Text = myUserRooms[4].numberOfUsers + " üyeler";
                    frameSelectionTab5.IsVisible = true;
                }
                await System.Threading.Tasks.Task.WhenAny(
                    stackGroupsHeader.FadeTo(1, 300),
                    scrollViewMyGroups.FadeTo(1, 300)

                    );

                await System.Threading.Tasks.Task.Delay(250);
            }
            List<AnnouncementResponseModel> announcementResponseModels = await exploreServices.GetAnnouncementResponseModels();
            if (announcementResponseModels.Count > 0)
            {
                stackAnnouncementsHeader.IsVisible = true;
                stackAnnouncementsNew.IsVisible = true;
                frameAnnouncement1.IsVisible = true;
                announcementIcon1.Text = announcementResponseModels[0].announcementIconText;
                announcementFrom1.Text = announcementResponseModels[0].announcementFrom;
                announcementText1.Text = announcementResponseModels[0].AnnouncementText;
            }
            if (announcementResponseModels.Count > 1)
            {

                frameAnnouncement2.IsVisible = true;
                announcementIcon2.Text = announcementResponseModels[1].announcementIconText;
                announcementFrom2.Text = announcementResponseModels[1].announcementFrom;
                announcementText2.Text = announcementResponseModels[1].AnnouncementText;
            }
            if (announcementResponseModels.Count > 2)
            {

                frameAnnouncement3.IsVisible = true;
                announcementIcon3.Text = announcementResponseModels[2].announcementIconText;
                announcementFrom3.Text = announcementResponseModels[2].announcementFrom;
                announcementText3.Text = announcementResponseModels[2].AnnouncementText;
            }
            if (announcementResponseModels.Count > 3)
            {

                frameAnnouncement4.IsVisible = true;
                announcementIcon4.Text = announcementResponseModels[3].announcementIconText;
                announcementFrom4.Text = announcementResponseModels[3].announcementFrom;
                announcementText4.Text = announcementResponseModels[3].AnnouncementText;
            }
            if (announcementResponseModels.Count > 4)
            {

                frameAnnouncement5.IsVisible = true;
                announcementIcon5.Text = announcementResponseModels[4].announcementIconText;
                announcementFrom5.Text = announcementResponseModels[4].announcementFrom;
                announcementText5.Text = announcementResponseModels[4].AnnouncementText;
            }
            if (announcementResponseModels.Count > 5)
            {

                frameAnnouncement6.IsVisible = true;
                announcementIcon6.Text = announcementResponseModels[5].announcementIconText;
                announcementFrom6.Text = announcementResponseModels[5].announcementFrom;
                announcementText6.Text = announcementResponseModels[5].AnnouncementText;
            }
            await System.Threading.Tasks.Task.WhenAny(
                stackAnnouncementsHeader.FadeTo(1, 300),
                stackAnnouncementsNew.FadeTo(1, 300)

                );


        }

        private async void tapGoToGroup_Tapped(object sender, EventArgs e)
        {
            Frame senderFrame = sender as Frame;
            string classId = senderFrame.ClassId;
            int roomId = 0;


            if (classId == "group1")
            {
                roomId = myUserRooms[0].UserRoom.Room.Id;
            }
            if (classId == "group2")
            {
                roomId = myUserRooms[1].UserRoom.Room.Id;
            }
            if (classId == "group3")
            {
                roomId = myUserRooms[2].UserRoom.Room.Id;
            }
            if (classId == "group4")
            {
                roomId = myUserRooms[3].UserRoom.Room.Id;
            }
            if (classId == "group5")
            {
                roomId = myUserRooms[4].UserRoom.Room.Id;
            }
            
            await Navigation.PushPopupAsync(new LoadingPage(true));
            RoomDetailsResponseModel responseModel = await exploreServices.GetStudyRoom(roomId);
            if (responseModel == null)
            {
                await Navigation.PopAllPopupAsync();
                DisplayAlert("Bulunamadı", "Bir hata oluştu.", "Tamam");
            }
            else
            {
                await Navigation.PopAllPopupAsync();
                await Navigation.PushAsync(new RoomDetailsPage(responseModel));
            }
        }
    }
}