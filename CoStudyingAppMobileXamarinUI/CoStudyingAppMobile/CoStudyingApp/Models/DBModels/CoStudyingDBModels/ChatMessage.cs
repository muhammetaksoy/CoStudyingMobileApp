using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class ChatMessage:EntityBase
    {
        public string Text { get; set; }

        public bool isRead { get; set; }

        public int UserRoomId { get; set; }

        public virtual UserRoom UserRoom { get; set; }

    }
}