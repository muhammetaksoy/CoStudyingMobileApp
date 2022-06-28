using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyApp.Models.DBModels
{
    public class Documentation:EntityBase
    {
        public string Url { get; set; }

        public int SenderId { get; set; }

        public int ChatChannelId { get; set; }

        public string Text { get; set; }

        public virtual ChatChannel ChatChannel { get; set; }
    }
}