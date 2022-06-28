using CoStudyApp.Models.DBModels;
using CoStudying.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying.Models.ResponseModels
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


    }
}