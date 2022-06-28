using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying.Models.ResponseModels
{
    public class StudyRoomResponseModel
    {
        public Room Room { get; set; }

        public int numberOfUsers { get; set; }

    }
}