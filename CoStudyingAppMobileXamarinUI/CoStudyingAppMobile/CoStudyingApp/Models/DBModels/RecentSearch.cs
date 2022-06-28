using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.DBModels
{
    public class RecentSearch : EntityBase
    {
        public int UserId { get; set; }
        public bool isCity { get; set; }
        public bool isDistrict { get; set; }
        public bool isRestaurant { get; set; }
        public bool isMenuItemType { get; set; }
        public bool isNearMe { get; set; }
        public bool isGeneral { get; set; }
        public bool isClassified { get; set; }
        public bool isSuccessfull { get; set; }
        public int RelatedRestaurantId { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string SearchKey { get; set; }
        public string SearchFixedkey { get; set; }
        public virtual User User { get; set; }


    }
}
