using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class QRCode:EntityBase
    {
        public string PassCode { get; set; }

        public int RelatedCampaignId { get; set; }

        public int RelatedMenuItemId { get; set; }

        public double Price { get; set; }

        public bool isUsed { get; set; }

        public DateTime FinishingDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
