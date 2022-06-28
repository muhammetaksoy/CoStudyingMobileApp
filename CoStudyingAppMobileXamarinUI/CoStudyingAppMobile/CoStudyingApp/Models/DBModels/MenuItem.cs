using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MenuItem:EntityBase
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Info { get; set; }
        
        public int MenuCategoryId { get; set; }

        public string PhotoPath { get; set; }

        public int PurchasedCount { get; set; }

        public bool isActive { get; set; }

        public int RelatedRestaurantId { get; set; }

        public int DiscountPercantage { get; set; }

        public DateTime DiscountFinishingDate { get; set; }

        public virtual MenuCategory MenuCategory { get; set; }

        public virtual List<PurchaseMenuItem> PurchaseMenuItems { get; set; }

        public MenuItem()
        {
            PurchaseMenuItems = new List<PurchaseMenuItem>();
        }

    }
}
