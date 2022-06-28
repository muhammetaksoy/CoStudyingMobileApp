using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ViewModels
{
    public class CampaignCardModelIndex
    {
        public Campaign Campaign { get; set; }
        public string RestaurantName
        {
            get
            {
                return Campaign.Restaurant.RestaurantName;
            }
        }

        public string CardPhotoUrl
        {
            get
            {
                return Campaign.Restaurant.ProfilePicturePath;
            }
        }

        public string District
        {
            get
            {
                return Campaign.Restaurant.District;
            }
        }

        public double AverageScore
        {
            get
            {
                return Campaign.Restaurant.Rating;
            }
        }
        public int NumberOfReviews
        {
            get
            {
                return Campaign.Restaurant.ReviewCount;
            }
        }

        public string CampaignText
        {
            get
            {
                return Campaign.SubText;
            }
        }

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
