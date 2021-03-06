using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.DBModels.CoStudyingDBModels
{
    public class PersonalTask : EntityBase
    {

        public string Header { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public bool isFinished { get; set; }


        //public int TodoListId { get; set; }

        public virtual User User { get; set; }

        //public virtual TodoList TodoList { get; set; }

        public DateTime DeadLine { get; set; }

    }
}
