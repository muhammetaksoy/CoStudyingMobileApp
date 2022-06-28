using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class RestaurantProductsAndCampaignsResponseModel
    {
        public List<MenuItem> MenuItems { get; set; }

        public List<Campaign> Campaigns { get; set; }


    }
}
