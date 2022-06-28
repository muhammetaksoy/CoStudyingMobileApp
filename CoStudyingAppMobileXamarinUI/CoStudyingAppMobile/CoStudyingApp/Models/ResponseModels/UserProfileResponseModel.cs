using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class UserProfileResponseModel
    {
        public User User { get; set; }

        public List<Review> Reviews { get; set; }

        public int CountReviews
        {
            get
            {
                if (Reviews != null)
                    return Reviews.Count;
                return 0;
            }
        }

        public int CountVisits { get; set; }

    }
}
