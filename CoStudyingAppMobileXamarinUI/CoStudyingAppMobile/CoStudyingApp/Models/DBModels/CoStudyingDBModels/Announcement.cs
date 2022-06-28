using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class Announcement:EntityBase
    {
        public string Text { get; set; }

        public int SenderId { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        //public virtual ChatChannel ChatChannel { get; set; }

    }
}