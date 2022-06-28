//using Entities;
using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class RegisterResponseModel
    {
        public User User { get; set; }

        public bool isSuccess { get; set; }

        public string ReturnMessage { get; set; }

    }
}
