using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.DBModels.CoStudyingDBModels;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class TaskResponseModel
    {

        public PersonalTask PersonalTask { get; set; }

        public UserRoomTask UserRoomTask { get; set; }

        public DateTime deadline
        {
            get
            {
                if (PersonalTask != null)
                    return PersonalTask.DeadLine;
                else
                    return UserRoomTask.DeadLine;
            }
        }

    }
}
