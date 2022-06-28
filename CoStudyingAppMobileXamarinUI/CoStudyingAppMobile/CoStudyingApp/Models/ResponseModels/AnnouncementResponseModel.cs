using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.DBModels.CoStudyingDBModels;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class AnnouncementResponseModel
    {
        public bool isDepartmentAnnouncement { get; set; }

        public int announcementId
        {
            get
            {
                if (isDepartmentAnnouncement)
                    return DepartmentAnnouncement.Id;
                return Announcement.Id;
            }
        }

        public DepartmentAnnouncement DepartmentAnnouncement { get; set; }

        public Announcement Announcement { get; set; }

        public DateTime CreatedOn
        {
            get
            {
                if (isDepartmentAnnouncement)
                    return DepartmentAnnouncement.CreatedOn;
                return Announcement.CreatedOn;
            }
        }

        public string announcementIconText
        {
            get
            {
                if (isDepartmentAnnouncement)
                    return "graduation-cap" + " ";
                else
                    return "book" + " ";
            }
        }

        public string announcementFrom
        {
            get
            {
                if (isDepartmentAnnouncement)
                    return DepartmentAnnouncement.Department.DepartmentName;
                else
                    return Announcement.Room.RoomName;
            }
        }

        public string AnnouncementText
        {
            get
            {
                if (isDepartmentAnnouncement)
                    return DepartmentAnnouncement.Text;
                else
                    return Announcement.Text;
            }
        }

    }
}
