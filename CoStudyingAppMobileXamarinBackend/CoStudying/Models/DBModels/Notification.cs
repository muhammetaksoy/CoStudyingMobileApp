using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    [Table("Notifications")]
    public class Notification:EntityBase
    {
        public string Header { get; set; }

        public string Type { get; set; }

        public int UserId { get; set; }

        public int RelatedId { get; set; }

        public bool isRead { get; set; }

        public virtual User User { get; set; }

    }
}