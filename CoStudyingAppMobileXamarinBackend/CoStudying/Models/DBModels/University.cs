using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudying.Models.DBModels
{
    [Table("Universities")]
    public class University : EntityBase
    {
        public string UniversityName { get; set; }

        public string City { get; set; }

        public bool isActive { get; set; }

        public virtual List<Faculty> Faculties { get; set; }

        public University()
        {
            Faculties = new List<Faculty>();
        }

    }
}