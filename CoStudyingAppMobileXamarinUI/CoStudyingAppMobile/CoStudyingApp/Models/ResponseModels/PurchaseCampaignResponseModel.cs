using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class PurchaseCampaignResponseModel
    {

        public PurchaseCampaign PurchaseCampaign { get; set; }

        public bool isSuccess { get; set; }

        public string Status { get; set; }

    }

    public class PurchaseCampaignResponseCodes
    {
        public static string OutOfDate
        {
            get
            {
                return "Campaign is out od date";
            }
        }

        public static string Success
        {
            get
            {
                return "Success";
            }
        }

        public static string StokFinished
        {
            get
            {
                return "StokFinished";
            }
        }

        public static string AlreadyUsed
        {
            get
            {
                return "AlreadyUsed";
            }
        }

        public static string AlreadyHave
        {
            get
            {
                return "AlreadyHave";
            }
        }
    }
}
