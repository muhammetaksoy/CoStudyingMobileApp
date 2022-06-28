using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying.Models.ResponseModels
{
    public class RegisterResponseModel
    {

        public User User { get; set; }

        public bool isSuccess { get; set; }

        public string ReturnMessage { get; set; }
        public Exception customException { get; set; }
    }
}