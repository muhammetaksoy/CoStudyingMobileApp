
using CoStudyApp.Models.DBModels;
using CoStudying.Models;
using CoStudying.Models.BusinessModels;
using CoStudying.Models.DBModels;
using CoStudying.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoStudying.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")] 
    public class ExploreController : ApiController
    {
        private DatabaseContext GetDatabaseContext()
        {
            DatabaseContext context = new DatabaseContext();
            context.Configuration.LazyLoadingEnabled = false;
            return context;
        }

     
        public HttpResponseMessage GetExample()
        {
            DatabaseContext context = GetDatabaseContext();


            var listUsers = context.Users.Where(x => x.Email == "ualsdeniz@hotmail").OrderBy(x=> x.Id).ToList();


            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [Authorize]
        [HttpGet]
        public FeedPageResponseModel GetFeedPage()
        {
            FeedPageResponseModel resp = new FeedPageResponseModel();
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();
            myUser = context.Users.Include("UserRooms").AsNoTracking().Where(x => x.Id == userId).FirstOrDefault();
            DateTime nowTime = CustomTime.GetNowTime();
            List<int> roomIds = myUser.UserRooms.Select(x => x.RoomId).ToList();
            List<int> userRoomIds = myUser.UserRooms.Select(x => x.Id).ToList();
            List<Room> myStudyRooms = context.Rooms.Where(x => roomIds.Contains(x.Id)).OrderByDescending(x=> x.Id).ToList();
            List<Announcement> announcements = context.Announcements.Where(x => roomIds.Contains(x.RoomId)).OrderByDescending(x => x.Id).Take(15).ToList();
            List<DepartmentAnnouncement> departmentAnnouncements = context.DepartmentAnnouncements.
                Where(x => x.DepartmentId == myUser.DepartmentId).OrderByDescending(x=> x.Id).Take(10).ToList();

            List<Task> personalTasks = context.Tasks.Where(x => x.UserId == userId && x.isFinished != true && nowTime < x.DeadLine).
                OrderBy(x => x.DeadLine).Take(20).ToList();

            List<UserRoomTask> userRoomTasks = context.UserRoomTasks.Where(x=> userRoomIds.Contains(x.UserRoomId) && x.isFinished != true && nowTime < x.DeadLine).
                OrderBy(x => x.DeadLine).Take(20).ToList();
            resp.MyAnnouncements = announcements;
            resp.MyDepartmentAnnouncements = departmentAnnouncements;
            resp.MyStudyGroups = myStudyRooms;
            resp.PersonalTasks = personalTasks;
            resp.UserRoomTasks = userRoomTasks;
            return resp;

        }


        [Authorize]
        [HttpGet]
        public List<UserRoomResponseModel> GetMyUserRooms()
        {
            List<UserRoom> userRooms = new List<UserRoom>();
            List<UserRoomResponseModel> responseModels = new List<UserRoomResponseModel>();
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();            
            List<UserRoom> myUserRooms = context.UserRooms.Include("Room").AsNoTracking().Where(x => x.UserId == userId).ToList();            
            foreach(UserRoom userRoom in myUserRooms)
            {
                UserRoomResponseModel resp = new UserRoomResponseModel();
                resp.UserRoom = userRoom;
                resp.numberOfUsers = context.UserRooms.Where(x => x.RoomId == userRoom.RoomId).Count();
                responseModels.Add(resp);
            }
            
            return responseModels;
        }

        [Authorize]
        [HttpGet]
        public List<AnnouncementResponseModel> GetAnnouncementResponseModels()
        {
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();
            myUser = context.Users.Include("UserRooms").AsNoTracking().Where(x => x.Id == userId).FirstOrDefault();
            DateTime nowTime = CustomTime.GetNowTime();
            List<int> roomIds = myUser.UserRooms.Select(x => x.RoomId).ToList();
            List<int> userRoomIds = myUser.UserRooms.Select(x => x.Id).ToList();

            List<AnnouncementResponseModel> responseModels = new List<AnnouncementResponseModel>();

            List<Announcement> announcements = context.Announcements.Include("Room").AsNoTracking().Where(x => userRoomIds.Contains(x.RoomId)).OrderByDescending(x => x.Id).ToList();
            List<DepartmentAnnouncement> departmentAnnouncements = context.DepartmentAnnouncements.Include("Department").AsNoTracking()
                .Where(x => x.DepartmentId == myUser.DepartmentId).OrderByDescending(x => x.Id).ToList();

            if (announcements != null)
            {
                foreach( Announcement announcement in announcements)
                {
                    responseModels.Add(new AnnouncementResponseModel()
                    {
                        isDepartmentAnnouncement = false,
                        Announcement = announcement
                    });
                }
            }

            if (departmentAnnouncements != null)
            {
                foreach (DepartmentAnnouncement departmentAnnouncement in departmentAnnouncements)
                {
                    responseModels.Add(new AnnouncementResponseModel()
                    {
                        isDepartmentAnnouncement = true,
                        DepartmentAnnouncement = departmentAnnouncement
                    });
                }
            }

            return responseModels.OrderByDescending(x=> x.CreatedOn).ToList();
        }

        [Authorize]
        [HttpGet]
        public List<Room> GetSuggestedGroups()
        {
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();
            myUser = context.Users.Include("UserRooms").AsNoTracking().Where(x => x.Id == userId).FirstOrDefault();
            DateTime nowTime = CustomTime.GetNowTime();
            List<int> roomIds = myUser.UserRooms.Select(x => x.RoomId).ToList();
            List<int> userRoomIds = myUser.UserRooms.Select(x => x.Id).ToList();

            List<Room> responseModels = new List<Room>();

            responseModels = context.Rooms.Include("UserRooms").AsNoTracking().
                Where(x => !roomIds.Contains(x.Id) && x.CreatorDepartmentId == myUser.DepartmentId && x.isPrivateRoom == false).ToList();

            return responseModels.OrderByDescending(x => x.Id).ToList();
        }

        
        [Authorize]
        [HttpGet]
        public RoomDetailsResponseModel SearchStudyRoom(string searchKey)
        {
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();
            bool hasInvite = false;
            bool isMember = false;
            bool isAdmin = false;
            myUser = context.Users.Include("UserRooms").Include("Invites").Include("Department").Include("Department.Faculty")
                .AsNoTracking().Where(x => x.Id == userId).FirstOrDefault();

            List<int> roomIds = myUser.UserRooms.Select(x => x.RoomId).ToList();
            List<int> userRoomIds = myUser.UserRooms.Select(x => x.Id).ToList();

            Room room = context.Rooms.Include("UserRooms").Include("UserRooms.User").Include("UserRooms.User.Department").AsNoTracking().
                Where(x => x.RoomAccessCode.ToUpper() == searchKey.ToUpper()).FirstOrDefault();



            if (room == null)
                return null;

            int roomCreatorUniId = context.Users.Include("Department").Include("Department.Faculty").AsNoTracking()
                .Where(x => x.Id == room.CreatorId).FirstOrDefault().Department.Faculty.UniversityId;

            if (room.CreatorId == myUser.Id)
                isAdmin = true;

            if (roomCreatorUniId != myUser.Department.Faculty.UniversityId)
                return null;

            if (room.UserRooms.Any(x => userRoomIds.Contains(x.Id)))
            {
                isMember = true;
            }
            if (isMember == false && myUser.Invites.Any(x => x.isAccepted == false && x.RoomId == room.Id))
            {
                hasInvite = true;
            }
            return new RoomDetailsResponseModel()
            {
                Room = room,
                hasSentInvite = hasInvite,
                isMember = isMember,
                isAdmin = true
            };

        }

        [Authorize]
        [HttpGet]
        public RoomDetailsResponseModel GetStudyRoom(int roomId)
        {
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();
            bool hasInvite = false;
            bool isMember = false;
            bool isAdmin = false;
            myUser = context.Users.Include("UserRooms").Include("Invites").Include("Department").Include("Department.Faculty")
                .AsNoTracking().Where(x => x.Id == userId).FirstOrDefault();

            List<int> roomIds = myUser.UserRooms.Select(x => x.RoomId).ToList();
            List<int> userRoomIds = myUser.UserRooms.Select(x => x.Id).ToList();

            Room room = context.Rooms.Include("UserRooms").Include("UserRooms.User").Include("UserRooms.User.Department").AsNoTracking().
                Where(x => x.Id == roomId).FirstOrDefault();



            if (room == null)
                return null;

            int roomCreatorUniId = context.Users.Include("Department").Include("Department.Faculty").AsNoTracking()
                .Where(x => x.Id == room.CreatorId).FirstOrDefault().Department.Faculty.UniversityId;

            if (room.CreatorId == myUser.Id)
                isAdmin = true;

            if (roomCreatorUniId != myUser.Department.Faculty.UniversityId)
                return null;

            if (room.UserRooms.Any(x => userRoomIds.Contains(x.Id)))
            {
                isMember = true;
            }
            if (isMember == false && myUser.Invites.Any(x => x.isAccepted == false && x.RoomId == room.Id))
            {
                hasInvite = true;
            }
            return new RoomDetailsResponseModel()
            {
                Room = room,
                hasSentInvite = hasInvite,
                isMember = isMember,
                isAdmin = true
            };

        }

        [Authorize]
        [HttpGet]
        public List<TaskResponseModel> GetMyTasks()
        {
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();

            myUser = context.Users.Include("UserRooms").AsNoTracking().Where(x => x.Id == userId).FirstOrDefault();

            List<int> roomIds = myUser.UserRooms.Select(x => x.RoomId).ToList();
            List<int> userRoomIds = myUser.UserRooms.Select(x => x.Id).ToList();
            List<TaskResponseModel> resp = new List<TaskResponseModel>();

            List<PersonalTask> personalTasks = context.PersonalTasks.Where(x => x.isFinished == false &&
           x.UserId == myUser.Id
            ).ToList();
            List<UserRoomTask> UserRoomTasks = context.UserRoomTasks.Where(x => x.isFinished == false &&
           userRoomIds.Contains(x.UserRoomId)
            ).ToList();

            if (personalTasks != null)
            {
                foreach( PersonalTask pers in personalTasks)
                {
                    resp.Add(new TaskResponseModel() { PersonalTask = pers, UserRoomTask = null });
                }
            }
            if (UserRoomTasks != null)
            {
                foreach (UserRoomTask pers in UserRoomTasks)
                {
                    resp.Add(new TaskResponseModel() { PersonalTask = null, UserRoomTask = pers });
                }
            }
            return resp;

        }

        [Authorize]
        [HttpGet]
        public List<UserRoomTask> GetUserRoomTasks(int roomId)
        {
            DatabaseContext context = GetDatabaseContext();
            UserInfo loggedUser = UserInfoMethod.CurrentUser();
            int userId = loggedUser.UserId;
            User myUser = new User();
            DateTime nowtime = CustomTime.GetNowTime();
            List<UserRoomTask> userRoomTasks = context.UserRoomTasks.Include("UserRoom").Include("UserRoom.User")
                .Where(x => x.UserRoom.RoomId == roomId && nowtime < x.DeadLine && x.isFinished == false).OrderBy(x=> x.DeadLine).ToList();
            return userRoomTasks;


        }


        public List<Faculty> GetFaculties()
        {
            var context = GetDatabaseContext();
            return context.Faculties.Include("Departments").ToList();
        }

        public List<University> GetUniversities()
        {
            var context = GetDatabaseContext();
            return context.Universities.Include("Faculties").Include("Faculties.Departments").AsNoTracking().ToList();
        }



    }
}
