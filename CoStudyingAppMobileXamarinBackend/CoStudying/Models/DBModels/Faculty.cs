using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudying.Models.DBModels
{
    [Table("Faculties")]
    public class Faculty : EntityBase
    {
        public int UniversityId { get; set; }
        public string FacultyName { get; set; }
        public virtual List<Department> Departments { get; set; }
        public virtual University University { get; set; }

        public Faculty()
        {
            Departments = new List<Department>();
        }



    }
}