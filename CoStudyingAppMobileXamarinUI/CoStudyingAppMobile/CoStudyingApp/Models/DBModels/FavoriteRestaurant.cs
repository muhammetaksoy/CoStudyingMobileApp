using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FavoriteRestaurant:EntityBase
    {
        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
