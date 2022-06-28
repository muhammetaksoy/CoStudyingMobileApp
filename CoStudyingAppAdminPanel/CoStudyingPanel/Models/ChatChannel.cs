using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("ChatChannels")]
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