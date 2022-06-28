using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class FavoritesModel
    {


        public List<FavoriteRestaurant> FavoriteRestaurants { get; set; }

        public List<FavoriteMenuItem> FavoriteMenuItems { get; set; }

        public List<FavoriteArticle> FavoriteArticles { get; set; }

        public List<FavoriteRecipe> FavoriteRecipes { get; set; }

        public bool isReloadRequiredMenuItem { get; set; }

        public bool isReloadRequiredRestaurant { get; set; }

        public FavoritesModel()
        {
            FavoriteRestaurants = new List<FavoriteRestaurant>();

            FavoriteMenuItems = new List<FavoriteMenuItem>();

            FavoriteArticles = new List<FavoriteArticle>();

            FavoriteRecipes = new List<FavoriteRecipe>();
        }

    }
}
