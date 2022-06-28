using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class PreviousActivitiesWalletResponseModel
    {
        public int purchaseId { get; set; }

        public int PaybackId { get; set; }

        public int CampaignId { get; set; }

        public int MenuItemId { get; set; }

        public int RestaurantId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool isPurchaseMenuItem { get; set; }

        public bool isPurchaseCampaign { get; set; }

        public double PriceCost { get; set; }

        public bool isPayback { get; set; }

        public string PhotoPath { get; set; }

        public string RestaurantName { get; set; }

        public string ItemName { get; set; }

        public string SubText
        {
            get
            {
                if (isPayback)
                    return "Yeaty hesabına yüklenen geri bakiye";
                else if (isPurchaseCampaign)
                    return "Yeaty özel kampanyası katılımı";
                else if (isPurchaseMenuItem)
                    return "Yeaty puanlar ile ödendi";
                else
                    return "";
            }
        }


    }
}
