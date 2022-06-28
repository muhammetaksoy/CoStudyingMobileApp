using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.DBModels.CoStudyingDBModels;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class FeedPageResponseModel
    {
        public List<Room> MyStudyGroups { get; set; }

        public List<Announcement> MyAnnouncements { get; set; }

        public List<DepartmentAnnouncement> MyDepartmentAnnouncements { get; set; }

        public List<Task> PersonalTasks { get; set; }

        public List<UserRoomTask> UserRoomTasks { get; set; }

    }
}
