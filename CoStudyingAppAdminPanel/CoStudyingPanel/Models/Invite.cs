using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("Invites")]
    public class Invite:EntityBase
    {
        public int SenderId { get; set; }

        public int RecieverId { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

    }
}