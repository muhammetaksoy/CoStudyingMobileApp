using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class Department : EntityBase
    {
        public int FacultyId { get; set; }

        public string DepartmentName { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual List<User> Users { get; set; }

        public Department()
        {
            Users = new List<User>();
        }

    }
}