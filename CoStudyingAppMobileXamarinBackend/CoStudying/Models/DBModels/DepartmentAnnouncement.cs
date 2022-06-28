using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudying.Models.DBModels
{
    [Table("DepartmentAnnouncements")]
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