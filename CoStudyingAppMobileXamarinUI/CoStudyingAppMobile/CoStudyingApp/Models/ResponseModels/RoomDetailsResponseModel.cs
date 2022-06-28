using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class RoomDetailsResponseModel
    {
        public Room Room { get; set; }

        public bool isMember { get; set; }

        public bool hasSentInvite { get; set; }

        public bool isAdmin { get; set; }

    }
}
