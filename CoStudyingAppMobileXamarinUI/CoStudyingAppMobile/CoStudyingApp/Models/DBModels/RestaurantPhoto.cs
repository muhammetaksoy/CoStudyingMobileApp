using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RestaurantPhoto:EntityBase
    {
        public int RestaurantId { get; set; }

        public string PhotoPath { get; set; }

        public virtual Restaurant Restaurant { get; set; }

    }
}
