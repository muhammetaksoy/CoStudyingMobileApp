using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PurchaseCampaign : EntityBase
    {
        public int UserId { get; set; }

        public double TotalCost { get; set; }  // YEATY BAKİYESİ İLE ÖDENEN TUTAR

        public int RelatedQRId { get; set; }

        public bool isCompleted { get; set; }

        public int CampaignId { get; set; }

        public DateTime CompletedOn { get; set; }

        public virtual User User { get; set; }

        public virtual Campaign Campaign { get; set; }

    }
}
