using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    [Table("Invites")]
    public class Invite:EntityBase
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
        public bool isAccepted { get; set; }

    }
}