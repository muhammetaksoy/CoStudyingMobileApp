
using CoStudyApp.Models.DBModels;
using CoStudying.Models;
using CoStudying.Models.BusinessModels;
using CoStudying.Models.DBModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoStudying.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    public class IncreaseController : ApiController
    {
        DatabaseContext context = DBContextMethods.GetDatabaseContext();

        

        [Authorize]
        [HttpGet]
        public Room CreateRoom(string roomName, bool isPrivateRoom,string description)
        {
            //var context = GetDatabaseContext();
            try
            {
                UserInfo loggedUser = UserInfoMethod.CurrentUser();
                string allowedCharacters = "QWERTYUOPASDFGHJKLMNBVCXZZ";
                Random rnd = new Random();
                string specialAccess = "";
                for (int i = 0; i < 4; i++)
                {
                    int specIndex = rnd.Next(0, allowedCharacters.Length - 1);
                    char spec = allowedCharacters[specIndex];
                    specialAccess += spec;
                }

                User currentUser = new User();
                currentUser = context.Users.Where(x => x.Id == loggedUser.UserId).FirstOrDefault();

                Room newRoom = new Room()
                {
                    CreatorId = loggedUser.UserId,
                    CreatorDepartmentId = currentUser.DepartmentId,
                    CreatedOn = CustomTime.GetNowTime(),
                    UserRooms = new List<UserRoom>()
                {
                    new UserRoom(){isAdmin = true,CreatedOn = CustomTime.GetNowTime(),UserId = currentUser.Id }
                },
                    isPrivateRoom = isPrivateRoom,
                    RoomName = roomName,
                    Description = description,
                    RoomAccessCode = specialAccess,
                    Type = "StudyRoom",
                };
                context.Rooms.Add(newRoom);
                context.SaveChanges();
                Room lastAddedRoom = context.Rooms.OrderByDescending(x => x.Id).FirstOrDefault();
                string oldCode = lastAddedRoom.RoomAccessCode;
                oldCode += lastAddedRoom.Id;
                lastAddedRoom.RoomAccessCode = oldCode;
                context.SaveChanges();
                Room lastAddedRoom2 = context.Rooms.Include("UserRooms").OrderByDescending(x => x.Id).FirstOrDefault();
                return lastAddedRoom2;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage JoinGroup(int groupId)
        {
            UserInfo loggedUser = UserInfoMethod.CurrentUser();

            Room room = context.Rooms.Where(x => x.Id == groupId).FirstOrDefault();

            if (room == null)
                return null;

            UserRoom userRoom = new UserRoom();
            userRoom.isAdmin = false;
            userRoom.CreatedOn = CustomTime.GetNowTime();
            userRoom.RoomId = room.Id;
            userRoom.UserId = loggedUser.UserId;
            room.UserRooms.Add(userRoom);
            context.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CreatePersonalTask(int month, int day, int year, string text)
        {
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            PersonalTask task = new PersonalTask()
            {
                CreatedOn = CustomTime.GetNowTime(),
                isFinished = false,
                Header = "",
                Text = text,
                DeadLine = new DateTime(year, month, day),
                UserId = loggedUser.UserId,
                
            };
            context.PersonalTasks.Add(task);
            context.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CreateAnnouncement(int roomId, string text)
        {
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            Announcement ann = new Announcement()
            {
                CreatedOn = CustomTime.GetNowTime(),
                RoomId = roomId,
                SenderId = loggedUser.UserId,
                Text = text
            };
            context.Announcements.Add(ann);
            context.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }



        //[Authorize]
        //public HttpResponseMessage GetExample()
        //{
        //    UserInfo info = new UserInfo();

        //}


    }
}
