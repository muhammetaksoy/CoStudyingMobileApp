using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RestaurantVideo:EntityBase
    {
        public int RestaurantId { get; set; }

        public string VideoPath { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
