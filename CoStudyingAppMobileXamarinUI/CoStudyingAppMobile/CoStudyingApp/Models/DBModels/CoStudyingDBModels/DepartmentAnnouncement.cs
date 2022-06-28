using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.DBModels.CoStudyingDBModels
{
    public class DepartmentAnnouncement : EntityBase
    {
        public string Text { get; set; }

        public int DepartmentId { get; set; }

        public bool isOnSchedule { get; set; }

        public DateTime begin { get; set; }

        public DateTime finish { get; set; }

        public virtual Department Department { get; set; }
    }
}
