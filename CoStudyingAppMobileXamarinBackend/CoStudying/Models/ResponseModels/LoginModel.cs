using CoStudyApp.Models.DBModels;
using CoStudying.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying.Models.ResponseModels
{
    public class LoginModel
    {

        public User currentUser { get; set; }
        public int countUnreadNotifications { get; set; }

        public List<FriendShipRequest> friendShipRequests { get; set; }

        public List<FriendShip> friendShips { get; set; }

        public Faculty faculty { get; set; }

        public Department department { get; set; }

    }
}