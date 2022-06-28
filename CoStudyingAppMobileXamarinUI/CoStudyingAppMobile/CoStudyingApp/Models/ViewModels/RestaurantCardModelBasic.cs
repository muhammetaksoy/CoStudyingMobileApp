using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ViewModels
{
    public class RestaurantCardModelBasic
    {
        public Restaurant Restaurant { get; set; }
        public int RestaurantId
        {
            get { return Restaurant.Id; }
        }
        public string RestaurantName { get { return  Restaurant.RestaurantName; } }
        public double AverageScore { get { return Restaurant.Rating; } }
        public string City { get { return Restaurant.City; } }
        public string District { get { return Restaurant.District; } }
        public string CardPhotoUrl { get { return Restaurant.ProfilePicturePath; } }
        public string RestaurantType { get { return Restaurant.RestaurantType; } }
        public int NumberOfReviews { get { return Restaurant.ReviewCount; } }

        public double FontSizeRestaurantName
        {
            get
            {
                if (RestaurantName.Length <= 30)
                    return 12;
                else if (RestaurantName.Length <= 42)
                    return 10;
                else
                    return 8;
            }
        }

        public string NumberOfReviewsStr
        {
            get
            {
                return "(" + NumberOfReviews.ToString() + " reviews)";
            }
        }
        public bool isVisibleFirstStar
        {
            get { return true; }
        }

        public bool isVisibleSecondStar
        {
            get
            {
                if (AverageScore >= 1.5) return true;
                return false;
            }
        }

        public bool isVisibleThirdStar
        {
            get
            {
                if (AverageScore >= 2.5) return true;
                return false;
            }
        }

        public bool isVisibleFourthStar
        {
            get
            {
                if (AverageScore >= 3.5) return true;
                return false;
            }
        }

        public bool isVisibleFifthStar
        {
            get
            {
                if (AverageScore >= 4.5) return true;
                return false;
            }
        }



    }
}
