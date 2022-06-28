using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class ChannelUser:EntityBase
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual List<ChatMessage> ChatMessages { get; set; }
    }
}