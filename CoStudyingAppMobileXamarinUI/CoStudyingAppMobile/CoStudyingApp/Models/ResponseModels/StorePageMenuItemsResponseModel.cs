using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class StorePageMenuItemsResponseModel
    {
        public int NumberMenuItems { get; set; }

        public string MenuItemsTitle { get; set; }

        public int MenuItemsRestaurantCount { get; set; }

        public string MenuItemsCagetory { get; set; }

        public string CategoryPhotoUrl { get; set; }

        public string CategoryPhotoLocalPath { get; set; }

    }
}
