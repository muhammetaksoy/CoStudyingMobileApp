using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class RestaurantProfileResponseModel
    {
        public Restaurant Restaurant { get; set; }

        public double LiveDistance { get; set; }

        public bool isOpenNow { get; set; }



    }
}
