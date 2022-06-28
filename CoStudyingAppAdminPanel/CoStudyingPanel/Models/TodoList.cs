using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("TodoLists")]
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