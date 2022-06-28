using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class RestaurantMenuItemResponseModel
    {
        public Restaurant Restaurant { get; set; }

        public MenuItem MenuItem { get; set; }
    }

}
