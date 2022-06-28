using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class Task:EntityBase
    {
        public string Header { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }


        //public int TodoListId { get; set; }

        public virtual User User { get; set; }

        //public virtual TodoList TodoList { get; set; }

        public bool isFinished { get; set; }

        public DateTime DeadLine { get; set; }

    }
}