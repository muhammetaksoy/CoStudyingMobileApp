using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudying.Models.DBModels
{
    [Table("UserRoomTasks")]
    public class UserRoomTask : EntityBase
    {
        public int UserRoomId { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public virtual UserRoom UserRoom { get; set; }

        public bool isFinished { get; set; }
        public DateTime DeadLine { get; set; }


    }
}