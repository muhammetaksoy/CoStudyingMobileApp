using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models
{
    public class LoginModel
    {
        public User currentUser { get; set; }

        public int countUnreadNotifications { get; set; }
    }
}
