using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("UserRooms")]
    public class UserRoom:EntityBase
    {
       
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public virtual User User { get; set; }
        public virtual Room Room { get; set; }

    }
}