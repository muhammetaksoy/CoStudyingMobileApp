using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class PurchaseMenuItemResponseModel
    {
        public bool isSuccess { get; set; }

        public string Status { get; set; }

        public PurchaseMenuItem PurchaseMenuItem { get; set; }

        public double NeededYeatyPoints { get; set; }

    }

    public class PurchaseMenuItemResponseCodes
    {
        public static string NoBalance
        {
            get
            {
                return "Not enough balance";
            }
        }

        public static string Success
        {
            get
            {
                return "Success";
            }
        }
    }
}
