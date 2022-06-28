using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ViewModels
{
    public class ReviewViewModel
    {
        public Review Review { get; set; }

        public string RestaurantName
        {
            get { return Review.Restaurant.RestaurantName; }
        }

        public string UserName
        {
            get { return Review.User.Name; }
        }

        public bool isVisibleStar1
        {
            get
            {
                return true;
            }
        }

        public bool isVisibleStar2
        {
            get
            {
                if (Review.Rate > 1)
                    return true;
                return false;
            }
        }

        public bool isVisibleStar3
        {
            get
            {
                if (Review.Rate > 2)
                    return true;
                return false;
            }
        }

        public bool isVisibleStar4
        {
            get
            {
                if (Review.Rate > 3)
                    return true;
                return false;
            }
        }

        public bool isVisibleStar5
        {
            get
            {
                if (Review.Rate > 4)
                    return true;
                return false;
            }
        }

        public string ReviewText
        {
            get { return Review.Comment; }
        }

        public string ProfilePicturePath
        {
            get
            {
                return Review.User.ProfilePicturePath;
            }
        }








    }
}
