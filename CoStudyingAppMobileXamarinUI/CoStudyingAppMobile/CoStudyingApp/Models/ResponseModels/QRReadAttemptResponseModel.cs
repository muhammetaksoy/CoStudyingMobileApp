using Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class QRReadAttemptResponseModel
    {

        public string hashedPassCode { get; set; }
        public bool isSuccess { get; set; }
        public HttpResponseMessage Status { get; set; }

        //Payback, Purchase, Campaign
        public string QRType { get; set; }

        public Campaign RelatedCampaign { get; set; }

        public MenuItem RelatedMenuItem { get; set; }


    }
}
