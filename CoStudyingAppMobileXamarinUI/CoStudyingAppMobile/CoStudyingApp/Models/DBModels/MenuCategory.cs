using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MenuCategory: EntityBase
    {
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public bool isActive { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual List<MenuItem> MenuItems { get; set; }

        public MenuCategory()
        {
            MenuItems = new List<MenuItem>();
        }

    }
}
