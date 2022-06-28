using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Tip:EntityBase
    {
        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }

        public virtual User User { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
