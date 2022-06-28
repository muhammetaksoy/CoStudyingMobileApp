using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("ChannelUsers")]
    public class ChannelUser:EntityBase
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual List<ChatMessage> ChatMessages { get; set; }
    }
}