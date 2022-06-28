using CoStudying.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying.Models.ResponseModels
{
    public class TaskResponseModel
    {
        public PersonalTask PersonalTask { get; set; }

        public UserRoomTask UserRoomTask { get; set; }




    }
}