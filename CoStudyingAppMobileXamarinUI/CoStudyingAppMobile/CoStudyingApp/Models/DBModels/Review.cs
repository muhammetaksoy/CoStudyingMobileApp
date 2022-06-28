using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Review:EntityBase
    {
        public int UserId { get; set; }

        public string Comment { get; set; }

        public int Rate { get; set; }

        public int RestaurantId { get; set; }

        public virtual User User { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
