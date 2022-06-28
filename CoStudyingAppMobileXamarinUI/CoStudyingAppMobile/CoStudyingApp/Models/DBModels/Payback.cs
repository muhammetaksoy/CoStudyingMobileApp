using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Payback:EntityBase
    {
        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public double TotalCost { get; set; }  // TOPLAM TUTAR

        public double PaybackCost { get; set; }  // GERİ ÖDEME TUTARI

        public double Commision { get; set; }  // KOMİSYON TUTAR

        public double CommissionPercentage { get; set; }  // KOMSİYON YÜZDESİ(MEKAN İÇİN SABİT)

        public bool isApproved { get; set; }

        public bool isDelivered { get; set; }

        public int RelatedQRId { get; set; }

        public virtual User User { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
