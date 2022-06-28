using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    [Table("ChatMessages")]
    public class ChatMessage:EntityBase
    {
        public string Text { get; set; }

        public bool isRead { get; set; }

        public int UserRoomId { get; set; }

        public virtual UserRoom UserRoom { get; set; }

    }
}