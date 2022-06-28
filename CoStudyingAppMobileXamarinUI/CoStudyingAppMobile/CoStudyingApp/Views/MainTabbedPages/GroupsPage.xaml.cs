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
using YeatyAppMobile.Views.PopupPages;

namespace YeatyAppMobile.Views.MainTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsPage : ContentPage
    {
        ExploreServices exploreServices = new ExploreServices();
        List<UserRoomResponseModel> myUserRooms;
        List<Room> suggestedGroups;
        public GroupsPage()
        {
            InitializeComponent();
            this.Title = "Gruplar";
            StartPage();
        }

        public async void StartPage()
        {
            await System.Threading.Tasks.Task.Delay(500);
            
            myUserRooms = await exploreServices.GetMyUserRooms();
            lblStudyGroupsHeader.Text = "Çalışma gruplarım (" + myUserRooms.Count + ")";
            await stackGroupsHeader.FadeTo(1, 400);
            if (myUserRooms != null)
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
            }
            
            suggestedGroups = await exploreServices.GetSuggestedGroups();
            stackSuggestedGroupsHeader.IsVisible = true;
            await stackSuggestedGroupsHeader.FadeTo(1, 400);
            if (suggestedGroups == null)
                return;
            if (suggestedGroups.Count > 0)
            {
                scrollViewSuggestedGroups.IsVisible = true;
                stackSuggestedGroupsHeader.IsVisible = true;
                lblSuggestedGroupName1.Text = suggestedGroups[0].RoomName;
                lblSuggestedGroupUserCount1.Text = suggestedGroups[0].UserRooms.Count + " üyeler";
                frameSuggestedTab1.IsVisible = true;
            }
            if (suggestedGroups.Count > 1)
            {
                lblSuggestedGroupName2.Text = suggestedGroups[1].RoomName;
                lblSuggestedGroupUserCount2.Text = suggestedGroups[1].UserRooms.Count + " üyeler";
                frameSuggestedTab2.IsVisible = true;
            }
            if (suggestedGroups.Count > 2)
            {
                lblSuggestedGroupName3.Text = suggestedGroups[2].RoomName;
                lblSuggestedGroupUserCount3.Text = suggestedGroups[2].UserRooms.Count + " üyeler";
                frameSuggestedTab3.IsVisible = true;
            }
            if (suggestedGroups.Count > 3)
            {
                lblSuggestedGroupName4.Text = suggestedGroups[3].RoomName;
                lblSuggestedGroupUserCount4.Text = suggestedGroups[3].UserRooms.Count + " üyeler";
                frameSuggestedTab4.IsVisible = true;
            }
            if (suggestedGroups.Count > 4)
            {
                lblSuggestedGroupName5.Text = suggestedGroups[4].RoomName;
                lblSuggestedGroupUserCount5.Text = suggestedGroups[4].UserRooms.Count + " üyeler";
                frameSuggestedTab5.IsVisible = true;
            }
            await System.Threading.Tasks.Task.WhenAny(
                stackSuggestedGroupsHeader.FadeTo(1, 300),
                scrollViewSuggestedGroups.FadeTo(1, 300)
                );

        }

        private async void tapCreateRoom_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateStudyRoomPage());
        }

        private async void tapSearch_Tapped(object sender, EventArgs e)
        {
            if ( !String.IsNullOrEmpty(entrySearch.Text))
            {
                await Navigation.PushPopupAsync(new LoadingPage(true));
                RoomDetailsResponseModel responseModel = await exploreServices.SearchStudyRoom(entrySearch.Text);
                if (responseModel == null)
                {
                    await Navigation.PopAllPopupAsync();
                    DisplayAlert("Bulunamadı", "Bu kod ile herhangi bir çalışma grubu bulunamadı.", "Tamam");
                }
                else
                {
                    await Navigation.PopAllPopupAsync();
                    await Navigation.PushAsync(new RoomDetailsPage(responseModel));
                }
            }
        }

        private async void tapGoToGroup_Tapped(object sender, EventArgs e)
        {
            Frame senderFrame = sender as Frame;
            string classId = senderFrame.ClassId;
            int roomId = 0;


            if ( classId == "group1")
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
            if (classId == "suggested1")
            {
                roomId = suggestedGroups[0].Id;
            }
            if (classId == "suggested2")
            {
                roomId = suggestedGroups[1].Id;
            }
            if (classId == "suggested3")
            {
                roomId = suggestedGroups[2].Id;
            }
            if (classId == "suggested4")
            {
                roomId = suggestedGroups[3].Id;
            }
            if (classId == "suggested5")
            {
                roomId = suggestedGroups[4].Id;
            }

            await Navigation.PushPopupAsync(new LoadingPage(true));
            RoomDetailsResponseModel responseModel = await exploreServices.GetStudyRoom(roomId);
            if (responseModel == null)
            {
                await Navigation.PopAllPopupAsync();
                DisplayAlert("Bulunamadı", "Bir  oluştu.", "Tamam");
            }
            else
            {
                await Navigation.PopAllPopupAsync();
                await Navigation.PushAsync(new RoomDetailsPage(responseModel));
            }
        }
    }
}