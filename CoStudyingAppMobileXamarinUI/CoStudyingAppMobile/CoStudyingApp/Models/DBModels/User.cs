using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User: EntityBase
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool isFacebookLogin { get; set; }

        public bool isGoogleLogin { get; set; }

        public bool isStandartLogin { get; set; }

        public string FacebookLoginPass { get; set; }

        public string GoogleLoginPass { get; set; }

        public bool isAppleLogin { get; set; }

        public string AppleLoginPass { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PersonalInfo { get; set; }

        public string ProfilePicturePath { get; set; }

        public bool isActive { get; set; }

        public bool isPremium { get; set; }

        public double inWallet { get; set; }

        public string Language { get; set; }

        public string WalletCurrency { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

        public int RelatedRestaurantId { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<PurchaseMenuItem> PurchaseMenuItems { get; set; }

        public virtual List<Report> Reports { get; set; }

        public virtual List<Payback> Paybacks { get; set; }

        public virtual List<Article> Articles { get; set; }

        public virtual List<ArticleRead> ArticleReads { get; set; }

        public virtual List<FavoriteArticle> FavoriteArticles { get; set; }

        public virtual List<FavoriteMenuItem> FavoriteMenuItems { get; set; }

        public virtual List<FavoriteRecipe> FavoriteRecipes { get; set; }

        public virtual List<FavoriteRestaurant> FavoriteRestaurants { get; set; }

        public virtual List<Notification> Notifications { get; set; }

        public virtual List<QRCode> QRCodes { get; set; }

        public virtual List<ResetPassword> ResetPasswords { get; set; }

        public virtual List<RestaurantProfileVisit> RestaurantProfileVisits { get; set; }

        public virtual List<Tip> Tips { get; set; }

        public virtual List<ArticlePhoto> BlogPhotos { get; set; }
        public virtual List<RecipeRead> RecipeReads { get; set; }

        public User()
        {
            Reviews = new List<Review>();
            PurchaseMenuItems = new List<PurchaseMenuItem>();
            FavoriteRestaurants = new List<FavoriteRestaurant>();
            Reports = new List<Report>();
            Paybacks = new List<Payback>();
            Articles = new List<Article>();
            ArticleReads = new List<ArticleRead>();
            FavoriteArticles = new List<FavoriteArticle>();
            FavoriteMenuItems = new List<FavoriteMenuItem>();
            FavoriteRestaurants = new List<FavoriteRestaurant>();
            FavoriteRecipes = new List<FavoriteRecipe>();
            Notifications = new List<Notification>();
            QRCodes = new List<QRCode>();
            ResetPasswords = new List<ResetPassword>();
            RestaurantProfileVisits = new List<RestaurantProfileVisit>();
            Tips = new List<Tip>();
            BlogPhotos = new List<ArticlePhoto>();
            RecipeReads = new List<RecipeRead>();
        }
    }
}
