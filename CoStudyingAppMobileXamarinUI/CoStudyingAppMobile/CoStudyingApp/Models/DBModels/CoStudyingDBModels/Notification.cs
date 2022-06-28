using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class Notification:EntityBase
    {
        public string Header { get; set; }

        public string Type { get; set; }

        public int UserId { get; set; }

        public int RelatedId { get; set; }

        public virtual User User { get; set; }

    }
}