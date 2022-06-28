using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Report:EntityBase
    {
        public int UserId { get; set; }

        public string ReportType { get; set; }

        public int ReportedId { get; set; }

        public string Reason { get; set; }

        public virtual User User { get; set; }
    }
}
