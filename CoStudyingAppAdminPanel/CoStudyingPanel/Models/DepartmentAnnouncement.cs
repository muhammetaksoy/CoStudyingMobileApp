using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    public class DepartmentAnnouncement : EntityBase
    {
        public string Text { get; set; }
        public bool isActive { get; set; }

        public int DepartmentId { get; set; }

        public bool isOnSchedule { get; set; }

        public DateTime begin { get; set; }

        public DateTime finish { get; set; }

        public virtual Department Department { get; set; }



        



    }
}