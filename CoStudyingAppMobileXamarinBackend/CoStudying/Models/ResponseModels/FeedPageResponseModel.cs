using CoStudyApp.Models.DBModels;
using CoStudying.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying.Models.ResponseModels
{
    public class FeedPageResponseModel
    {
        public List<Room> MyStudyGroups { get; set; }

        public List<Announcement> MyAnnouncements { get; set; }

        public List<DepartmentAnnouncement> MyDepartmentAnnouncements { get; set; }

        public List<Task> PersonalTasks { get; set; }

        public List<UserRoomTask> UserRoomTasks { get; set; }

    }

    //public class AnnouncementCompoundModel
    //{
    //    List<Ann>
    //}
}