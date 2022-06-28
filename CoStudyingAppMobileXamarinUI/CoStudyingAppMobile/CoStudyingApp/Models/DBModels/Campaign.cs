using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Campaign:EntityBase
    {
        public string Text { get; set; }

        public string SubText { get; set; }  // SADECE BİZİM OLUŞTURACAĞIMIZ KAMPANYALARDA GEÇERLİ. RESTORANLARA GÖRÜNMEZ OLACAK

        public string BackgroundImageUrl { get; set; }  // SADECE BİZİM OLUŞTURACAĞIMIZ KAMPANYALARDA GEÇERLİ. RESTORANLARA GÖRÜNMEZ OLACAK

        public bool isActive { get; set; }  

        public DateTime StartingDate { get; set; }

        public DateTime FinishingDate { get; set; }
        public string Role { get; set; } //Custom, Daily...vb
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual List<PurchaseCampaign> PurchaseCampaigns { get; set; }

        public Campaign()
        {
            PurchaseCampaigns = new List<PurchaseCampaign>();
        }
    }
}
