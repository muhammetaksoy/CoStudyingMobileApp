using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    [Table("Tasks")]
    public class Task:EntityBase
    {
        public string Header { get; set; }

        public string Text { get; set; }

        public DateTime DeadLine { get; set; }

    }
}