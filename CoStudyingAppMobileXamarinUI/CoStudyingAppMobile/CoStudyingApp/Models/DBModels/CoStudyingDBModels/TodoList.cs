using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class TodoList:EntityBase
    {
        public int RelatedRoomId { get; set; }

        public virtual List<Task> Tasks { get; set; }

        public TodoList()
        {
            Tasks = new List<Task>();

        }
    }
}