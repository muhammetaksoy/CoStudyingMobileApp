using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class SearchTopicResultResponseModel
    {

        public bool isLocation { get; set; }

        public bool isRestaurant { get; set; }

        public bool isMenuItemType { get; set; }

        public bool isDrink { get; set; }

        public bool isFood { get; set; }

        public string LocationName { get; set; }

        public string RestaurantName { get; set; }

        public string MenuItemTypeName { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantPhotoPath { get; set; }
        public string RestaurantDistrict { get; set; }


    }
}
