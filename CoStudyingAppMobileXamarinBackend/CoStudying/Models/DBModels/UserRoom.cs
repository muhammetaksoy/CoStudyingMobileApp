using CoStudying.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    [Table("UserRooms")]
    public class UserRoom:EntityBase
    {
       
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public bool isAdmin { get; set; } 
        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
        public virtual List<ChatMessage> ChatMessages { get; set; }

        public virtual List<UserRoomTask> UserRoomTasks { get; set; }

        public UserRoom()
        {
            ChatMessages = new List<ChatMessage>();
            UserRoomTasks = new List<UserRoomTask>();
        }



    }
}