using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("Announcements")]
    public class Announcement : EntityBase
    {
        public string Text { get; set; }

        public int SenderId { get; set; }

        public int RoomId { get; set; }

        public int ChatChannelId { get; set; }

        public virtual Room Room { get; set; }

        public virtual ChatChannel ChatChannel { get; set; }

    }
}