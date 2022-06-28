using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("ChatMessages")]
    public class ChatMessage:EntityBase
    {
        public string Text { get; set; }

        public bool isRead { get; set; }

        public int ChannelUserId { get; set; }

        public virtual ChannelUser ChannelUser { get; set; }

    }
}