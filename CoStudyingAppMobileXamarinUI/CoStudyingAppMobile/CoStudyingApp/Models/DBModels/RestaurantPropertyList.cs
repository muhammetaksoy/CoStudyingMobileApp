using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RestaurantPropertyList:EntityBase
    {
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public bool haveGarden { get; set; }

        public bool haveVegan { get; set; }

        public bool haveFreeAnimal { get; set; }

        public bool haveWifi { get; set; }

        public bool haveParkingSpace { get; set; }

        public bool haveTakeAway { get; set; }

        public bool haveSmokingZone { get; set; }

        public bool haveSisha { get; set; }

        public bool haveTerrace { get; set; }

        public string ExtraProperty1 { get; set; }

        public string ExtraProperty2 { get; set; }

        public string ExtraProperty3 { get; set; }

        public string ExtraProperty4 { get; set; }

        public string ExtraProperty5 { get; set; }

        public string ExtraProperty6 { get; set; }

        public string ExtraProperty7 { get; set; }

        public string ExtraProperty8 { get; set; }

    }
}
