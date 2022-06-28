//using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.BusinessModels;

namespace YeatyAppMobile.Models.ViewModels
{
    public class OwnedQRCodeViewModel
    {
        //public MenuItem MenuItem { get; set; }
        
        //public Campaign Campaign { get; set; }
        //public QRCode qrCode { get; set; }

        public bool isCampaign { get; set; }

        public bool isMenuItem { get; set; }

        public int purchaseId { get; set; }

        public string PhotoUrl { get; set; }

        public string RestaurantName { get; set; }

        public string SubText { get; set; }

        public string Title { get; set; }

        public int campaignId { get; set; }

        public double TitleFontSize
        {
            get
            {
                if (Title.Length > 18)
                    return 14;
                else
                    return 16;
            }
        }

    }
}
