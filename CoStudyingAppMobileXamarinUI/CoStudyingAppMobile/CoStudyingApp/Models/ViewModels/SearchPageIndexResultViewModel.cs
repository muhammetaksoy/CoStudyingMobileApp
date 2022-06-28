using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.ResponseModels;

namespace YeatyAppMobile.Models.ViewModels
{
    public class SearchPageIndexResultViewModel
    {
        public SearchTopicResultResponseModel responseModel { get; set; }

        public bool isImage
        {
            get
            {
                if (responseModel.isRestaurant)
                    return true;
                return false;
            }
        }

        public string iconImage
        {
            get
            {
                if (isImage)
                    return responseModel.RestaurantPhotoPath;
                else
                {
                    if ( responseModel.isMenuItemType )
                    {
                        return "coffee";
                    }
                    if ( responseModel.isLocation)
                    {
                        return "map-marker-alt";
                    }
                    else
                    {
                        return "history";
                    }
                }
            }
        }

        public string DefinitionString
        {
            get
            {
                if (responseModel.isLocation)
                    return responseModel.LocationName + " bölgesindeki mekanlar";
                else if (responseModel.isRestaurant)
                    return responseModel.RestaurantName + ", " + responseModel.RestaurantDistrict;
                else
                    return responseModel.MenuItemTypeName + " için sonuçlar";
            }
        }


    }
}
