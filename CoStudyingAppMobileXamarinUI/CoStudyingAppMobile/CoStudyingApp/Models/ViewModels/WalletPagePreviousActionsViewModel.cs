using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YeatyAppMobile.BusinessModels;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.ResponseModels;

namespace YeatyAppMobile.Models.ViewModels
{
    public class WalletPagePreviousActionsViewModel
    {

        public PreviousActivitiesWalletResponseModel responseModel { get; set; }

        public string PhotoPath
        {
            get
            {
                return responseModel.PhotoPath;
            }
        }

        public string ItemName
        {
            get
            {
                return responseModel.ItemName;
            }
        }

        public string RestaurantName
        {
            get
            {
                return responseModel.RestaurantName;
            }
        }

        public string SubText
        {
            get
            {
                return responseModel.SubText;
            }
        }

        public string DateStr
        {
            get
            {
                return DateTimeToString.DefinePastTime(responseModel.CreatedOn);
            }
        }

        public string CostStr
        {
            get
            {
                if (responseModel.isPurchaseCampaign)
                    return "Ücretsiz";
                else if ( responseModel.isPayback)
                {
                    string str = "+" + Math.Round(responseModel.PriceCost, 2).ToString();
                    if (str.Contains("."))
                        str = str.Replace('.', ',');
                    return str;
                }
                else
                {
                    string str = "-" + Math.Round(responseModel.PriceCost, 2).ToString();
                    if (str.Contains("."))
                        str = str.Replace('.', ',');
                    return str;
                }
            }
        }

        public Color CostStrColor
        {
            get
            {
                if (responseModel.isPayback || responseModel.isPurchaseCampaign)
                    return Color.FromHex("#006b1d");
                else
                    return CustomColors.defaultFoggyColor;
            }
        }





    }
}
