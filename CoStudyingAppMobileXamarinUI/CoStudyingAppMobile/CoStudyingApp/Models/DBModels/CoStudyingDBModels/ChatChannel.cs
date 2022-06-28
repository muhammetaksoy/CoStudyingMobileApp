using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class ChatChannel:EntityBase
    {
        public int RoomId { get; set; }

        public virtual List<ChannelUser> ChannelUsers { get; set; }

        public ChatChannel()
        {
            ChannelUsers = new List<ChannelUser>();
        }
    }
}