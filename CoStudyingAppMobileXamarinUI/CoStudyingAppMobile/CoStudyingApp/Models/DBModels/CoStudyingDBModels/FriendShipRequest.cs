using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoStudyApp.Models.DBModels
{
    public class FriendShipRequest:EntityBase
    {
        public int SenderId { get; set; }

        public int RecieverId { get; set; }

        public bool isAccepted { get; set; }


    }
}
