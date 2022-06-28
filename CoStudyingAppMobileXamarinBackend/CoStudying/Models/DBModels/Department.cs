using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudying.Models.DBModels
{
    [Table("Departments")]
    public class Department : EntityBase
    {
        public int FacultyId { get; set; }

        public string DepartmentName { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual List<User> Users { get; set; }

        public virtual List<DepartmentAnnouncement> DepartmentAnnouncements { get; set; }

        public Department()
        {
            Users = new List<User>();
            DepartmentAnnouncements = new List<DepartmentAnnouncement>();
        }

    }
}