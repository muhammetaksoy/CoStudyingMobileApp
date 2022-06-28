using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class SearchIndexDistrictsResponseModel
    {
        public string DistrictName { get; set; }
        public int RestaurantCount { get; set; }
        public string PhotoPath { get; set; }
    }
}
