using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    [Table("Announcements")]
    public class Announcement:EntityBase
    {
        public string Text { get; set; }

        public int SenderId { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }


    }
}