using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Entities;
using Xamarin.Forms;
using YeatyAppMobile.Models.ResponseModels;

namespace YeatyAppMobile.Models.ViewModels
{
    public class ProductCardModelIndex : INotifyPropertyChanged
    {
        public ProductCardModelIndex(bool _isLiked)
        {
            isLiked = _isLiked;
            LikeChanged(isLiked);
        }

        public RestaurantMenuItemResponseModel RootItem { get; set; }

        public Entities.MenuItem MenuItem
        {
            get
            {
                return RootItem.MenuItem;
            }
        }

        public Restaurant Restaurant
        {
            get { return RootItem.Restaurant; }
        }



        public string ProductImageUrl
        {
            get { return MenuItem.PhotoPath; }
        }
        public string RestaurantImageUrl { get { return Restaurant.ProfilePicturePath; } }
        public string RestaurantName { get { return Restaurant.RestaurantName; } }
        public string ProductName { get { return MenuItem.Name; } }
        public double ProductPrice { get { return MenuItem.Price; } }

        public double AverageScore { get { return Restaurant.Rating; } }
        public int NumberOfReviews { get { return Restaurant.ReviewCount; } }
        public string ProductPriceStr
        {
            get
            {
                return Math.Round(ProductPrice, 2).ToString();
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

        public bool isLiked { get; set; } 

        private string _favoriteIconFontFamily;

        public string favoriteIconFontFamily
        {
            get
            {
                return _favoriteIconFontFamily;
            }
            set
            {
                if ( value != _favoriteIconFontFamily)
                {
                    _favoriteIconFontFamily = value;
                    OnPropertyChanged("favoriteIconFontFamily");
                }
                  
            }
        }

        public void LikeChanged(bool _isLiked)
        {
            isLiked = _isLiked;
            if (isLiked)
            {              
                if ( Device.RuntimePlatform == Device.iOS)
                {
                    favoriteIconFontFamily = "FontAwesome5Free-Solid";
                }
                else
                {
                    favoriteIconFontFamily = "FontAwesome5Solid.otf#Regular";
                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    favoriteIconFontFamily = "FontAwesome5Free-Regular";
                }
                else
                {
                    favoriteIconFontFamily = "FontAwesome5Regular.otf#Regular";
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

    }
}
