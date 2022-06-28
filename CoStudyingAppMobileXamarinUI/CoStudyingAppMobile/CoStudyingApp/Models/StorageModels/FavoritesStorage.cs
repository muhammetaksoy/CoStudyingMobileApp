using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class FavoritesStorage
    {

        private static string KEY_FAV = "currentFavoritesModel";


        public static FavoritesModel GetFavoritesModel()
        {
            //if (App.Current.Properties.ContainsKey(KEY_FAV))
            //{
            //    FavoritesModel model = new FavoritesModel();
            //    model = App.Current.Properties[KEY_FAV] as FavoritesModel;
            //    return model;
            //}
            //else
            //{
            //    if (CurrentUserStorage.isUserExist())
            //    {
            //        FavoritesModel model = new FavoritesModel();
            //        User myUser = CurrentUserStorage.GetCurrentUser();
            //        model.FavoriteRestaurants = myUser.FavoriteRestaurants;
            //        model.FavoriteMenuItems = myUser.FavoriteMenuItems;
            //        model.FavoriteArticles = myUser.FavoriteArticles;
            //        model.FavoriteRecipes = myUser.FavoriteRecipes;
            //        model.isReloadRequiredMenuItem = false;
            //        model.isReloadRequiredRestaurant = false;
            //        App.Current.Properties[KEY_FAV] = model;
            //        return model;
            //    }
            //    return new FavoritesModel() { };
            //}
            return new FavoritesModel() { };
        }



        public static bool isFavoriteRestaurant(int restaurantId)
        {
            FavoritesModel model = GetFavoritesModel();
            return model.FavoriteRestaurants.Any(x => x.RestaurantId == restaurantId);
        }

        public static bool isFavoriteMenuItem(int favoriteMenuId)
        {
            FavoritesModel model = GetFavoritesModel();
            return model.FavoriteMenuItems.Any(x => x.MenuItemId == favoriteMenuId);
        }

        public static bool isFavoriteArticle(int favoriteArticleId)
        {
            FavoritesModel model = GetFavoritesModel();
            return model.FavoriteArticles.Any(x => x.ArticleId == favoriteArticleId);
        }

        public static bool isFavoriteRecipe(int favoriteRecipeId)
        {
            FavoritesModel model = GetFavoritesModel();
            return model.FavoriteRecipes.Any(x => x.RecipeId == favoriteRecipeId);
        }

        public static int GetNumberOfFavoriteRestaurants()
        {
            FavoritesModel model = GetFavoritesModel();
            return model.FavoriteRestaurants.Count;
        }

        public static int GetNumberOfFavoriteMenuItems()
        {
            FavoritesModel model = GetFavoritesModel();
            return model.FavoriteMenuItems.Count;
        }

        public static int GetNumberOfFavoriteRecipes()
        {
            FavoritesModel model = GetFavoritesModel();
            return model.FavoriteRecipes.Count;
        }


        public static void AddFavoriteRestaurant(int restaurantId)
        {
            FavoritesModel model = GetFavoritesModel();
            if (!model.FavoriteRestaurants.Any(x => x.RestaurantId == restaurantId))
            {
                FavoriteRestaurant newFav = new FavoriteRestaurant()
                {
                    CreatedOn = DateTime.Now,
                    RestaurantId = restaurantId,

                };
                model.FavoriteRestaurants.Add(newFav);
                model.isReloadRequiredRestaurant = true;
                SaveModel(model);
            }
        }

        public static void AddFavoriteRestaurant(List<int> restaurantIds)
        {
            FavoritesModel model = GetFavoritesModel();
            for(int i = 0; i < restaurantIds.Count; i++)
            {
                if (!model.FavoriteRestaurants.Any(x => x.RestaurantId == restaurantIds[i]))
                {
                    FavoriteRestaurant newFav = new FavoriteRestaurant()
                    {
                        CreatedOn = DateTime.Now,
                        RestaurantId = restaurantIds[i],

                    };
                    model.FavoriteRestaurants.Add(newFav);                   
                }
            }
            model.isReloadRequiredRestaurant = true;
            SaveModel(model);
        }

        public static void AddFavoriteMenuItem(int menuItemId)
        {
            FavoritesModel model = GetFavoritesModel();
            if (!model.FavoriteMenuItems.Any(x => x.MenuItemId == menuItemId))
            {
                FavoriteMenuItem newFav = new FavoriteMenuItem()
                {
                    CreatedOn = DateTime.Now,
                    MenuItemId = menuItemId,

                };
                model.FavoriteMenuItems.Add(newFav);
                model.isReloadRequiredMenuItem = true;
                SaveModel(model);
            }
        }

        public static void AddFavoriteMenuItem(List<int> menuItemIds)
        {
            FavoritesModel model = GetFavoritesModel();
            for(int i = 0; i < menuItemIds.Count; i++)
            {
                if (!model.FavoriteMenuItems.Any(x => x.MenuItemId == menuItemIds[i]))
                {
                    FavoriteMenuItem newFav = new FavoriteMenuItem()
                    {
                        CreatedOn = DateTime.Now,
                        MenuItemId = menuItemIds[i],

                    };
                    model.FavoriteMenuItems.Add(newFav);
                   
                }
            }
            model.isReloadRequiredMenuItem = true;
            SaveModel(model);
        }

        public static void AddFavoriteRecipe(int recipeId)
        {
            FavoritesModel model = GetFavoritesModel();
            if (!model.FavoriteRecipes.Any(x => x.RecipeId == recipeId))
            {
                FavoriteRecipe newFav = new FavoriteRecipe()
                {
                    CreatedOn = DateTime.Now,
                    RecipeId = recipeId,

                };
                model.FavoriteRecipes.Add(newFav);
                //model.ReloadRequired = true;
                SaveModel(model);
            }
        }

        public static void AddFavoriteRecipe(List<int> recipeIds)
        {
            FavoritesModel model = GetFavoritesModel();
            for(int i = 0; i < recipeIds.Count; i++)
            {
                if (!model.FavoriteRecipes.Any(x => x.RecipeId == recipeIds[i]))
                {
                    FavoriteRecipe newFav = new FavoriteRecipe()
                    {
                        CreatedOn = DateTime.Now,
                        RecipeId = recipeIds[i],

                    };
                    model.FavoriteRecipes.Add(newFav);
                    
                }
            }
            //model.ReloadRequired = true;
            SaveModel(model);
        }

        public static void RemoveFavoriteMenuItem(int menuItemId)
        {
            FavoritesModel model = GetFavoritesModel();
            if (model.FavoriteMenuItems.Any(x => x.MenuItemId == menuItemId))
            {
                FavoriteMenuItem fav = model.FavoriteMenuItems.Where(x => x.MenuItemId == menuItemId).FirstOrDefault();
                model.FavoriteMenuItems.Remove(fav);
                model.isReloadRequiredMenuItem = true;
                SaveModel(model);
            }
        }
        public static void RemoveFavoriteRestaurant(int restaurantId)
        {
            FavoritesModel model = GetFavoritesModel();
            if (model.FavoriteRestaurants.Any(x => x.RestaurantId == restaurantId))
            {
                FavoriteRestaurant fav = model.FavoriteRestaurants.Where(x => x.RestaurantId == restaurantId).FirstOrDefault();
                model.FavoriteRestaurants.Remove(fav);
                model.isReloadRequiredRestaurant = true;
                SaveModel(model);
            }
        }

        public static bool isReloadRequiredRestaurant()
        {
            FavoritesModel model = GetFavoritesModel();
            return model.isReloadRequiredRestaurant;
        }

        public static bool isReloadRequiredmenuItem()
        {
            FavoritesModel model = GetFavoritesModel();
            return model.isReloadRequiredMenuItem;
        }

        public static void FavoriteRestaurantReloaded()
        {
            FavoritesModel model = GetFavoritesModel();
            model.isReloadRequiredRestaurant = false;
            SaveModel(model);
        }

        public static void FavoritemenuItemReloaded()
        {
            FavoritesModel model = GetFavoritesModel();
            model.isReloadRequiredMenuItem = false;
            SaveModel(model);
        }




        public static void ClearStorage()
        {
            FavoritesModel favModel = GetFavoritesModel();
            if (favModel.FavoriteArticles.Count > 0)
            {
                favModel.FavoriteArticles.Clear();              
            }
            if (favModel.FavoriteMenuItems.Count > 0)
            {
                favModel.FavoriteMenuItems.Clear();
            }
            if (favModel.FavoriteRestaurants.Count > 0)
            {
                favModel.FavoriteRestaurants.Clear();
            }
            if (favModel.FavoriteRecipes.Count > 0)
            {
                favModel.FavoriteRecipes.Clear();
            }
            SaveModel(favModel);
        }


        public static void SaveModel(FavoritesModel favModel)
        {
            App.Current.Properties[KEY_FAV] = favModel;          
        }


    }
}
