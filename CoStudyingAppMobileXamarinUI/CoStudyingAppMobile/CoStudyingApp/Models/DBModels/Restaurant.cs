using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Restaurant: EntityBase
    {
        public string RestaurantName { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Neighborhood { get; set; }

        public string BaseAdress { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public int AverageWageForTwoPeople { get; set; }

        public string RestaurantType { get; set; }

        public string OpeningTime { get; set; }

        public string ClosingTime { get; set; }

        public bool isActive { get; set; }

        public string RestaurantInfo { get; set; }

        public double CommissionPercentage { get; set; }
        public double PaybackPercentage { get; set; }

        public double Rating { get; set; }

        public int ReviewCount { get; set; }

        public string ProfilePicturePath { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactEmail { get; set; }

        public string InstagramUrl { get; set; }

        public string FacebookUrl { get; set; }

        public string WebsiteUrl { get; set; }

        public string ClosedDay1 { get; set; }

        public string ClosedDay2 { get; set; }

        public string Currency { get; set; }

        public bool isTypeCoffee { get; set; }

        public bool isTypeDinner { get; set; }

        public bool isTypeBreakfast { get; set; }

        public bool isTypeDessert { get; set; }

        public bool isTypeOpenAir { get; set; }

        public virtual List<MenuCategory> MenuCategories { get; set; }
        public virtual List<Review> Reviews { get; set; }

        public virtual List<FavoriteRestaurant> FavoriteRestaurants { get; set; }

        public virtual List<RestaurantPhoto> RestaurantPhotos { get; set; }

        public virtual List<Payback> Paybacks { get; set; }

        public virtual List<Campaign> Campaigns { get; set; }

        public virtual List<Recipe> Recipes { get; set; }

        public virtual List<RestaurantProfileVisit> RestaurantProfileVisits { get; set; }

        public virtual List<RestaurantPropertyList> RestaurantPropertyLists { get; set; }

        public virtual List<Tip> Tips { get; set; }

        public virtual List<RestaurantVideo> RestaurantVideos { get; set; }

        public Restaurant()
        {
            MenuCategories = new List<MenuCategory>();
            FavoriteRestaurants = new List<FavoriteRestaurant>();
            RestaurantPhotos = new List<RestaurantPhoto>();
            Reviews = new List<Review>();
            RestaurantPhotos = new List<RestaurantPhoto>();
            Paybacks = new List<Payback>();
            Campaigns = new List<Campaign>();
            Recipes = new List<Recipe>();
            RestaurantProfileVisits = new List<RestaurantProfileVisit>();
            RestaurantPropertyLists = new List<RestaurantPropertyList>();
            Tips = new List<Tip>();
            RestaurantVideos = new List<RestaurantVideo>();
        }

    }
}
