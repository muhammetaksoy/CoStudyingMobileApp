using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class QRCodesIndexResponseModel
    {
        public bool isPurchaseCampaign { get; set; }

        public bool isPurchaseMenuItem { get; set; }

        public int purchaseId { get; set; }

        public int menuItemId { get; set; }

        public int campaignId { get; set; }

        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public DateTime CreatedOn { get; set; }
        public string PhotoPath { get; set; }



    }
}
