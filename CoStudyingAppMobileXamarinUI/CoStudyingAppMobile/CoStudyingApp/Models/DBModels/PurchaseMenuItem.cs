using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PurchaseMenuItem:EntityBase
    {
        public int UserId { get; set; }

        public double TotalCost { get; set; }  // YEATY BAKİYESİ İLE ÖDENEN TUTAR

        public int RelatedQRId { get; set; }

        public bool isCompleted { get; set; }

        public int MenuItemId { get; set; }

        public int RelatedRestaurantId { get; set; }

        public DateTime CompletedOn { get; set; }

        public virtual User User { get; set; }

        public virtual MenuItem MenuItem { get; set; }

    }
}
