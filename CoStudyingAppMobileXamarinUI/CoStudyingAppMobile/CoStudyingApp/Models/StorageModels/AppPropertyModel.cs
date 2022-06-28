using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class AppPropertyModel
    {

        public string LangCode { get; set; } // tr,en

        public bool isAllowedLocation { get; set; }

        public LatLongInfoModel LastLocation { get; set; }


    }
}
