using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class CampaignRestaurantProfileResponseModel
    {
        public Campaign Campaign { get; set; }

        public bool isUsed { get; set; }

        public bool isPurchased { get; set; }



    }
}
