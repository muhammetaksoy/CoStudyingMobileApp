using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{


    public class SelectedMenuItemStorageCell
    {

    }


    public class SelectedMenuItemStorage
    {

        private static string KEY_MENUITEM = "currentMenuItem";
        public static SelectedMenuItemModel GetModel()
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(KEY_MENUITEM))
            {
                string str = Xamarin.Essentials.Preferences.Get(KEY_MENUITEM, null);
                SelectedMenuItemModel model = JsonConvert.DeserializeObject<SelectedMenuItemModel>(str);
                return model;
            }
            else
            {
                SelectedMenuItemModel model = new SelectedMenuItemModel();
                model.LastUpdated = DateTime.Now;
                SaveModel(model);
                return model;
            }
        }


        public static void SaveModel(SelectedMenuItemModel menuItemModel)
        {
            var jsonResult = JsonConvert.SerializeObject(menuItemModel);
            Xamarin.Essentials.Preferences.Set(KEY_MENUITEM, jsonResult);
        }

        public static void RemoveItemFromModel(int menuItemId)
        {
            SelectedMenuItemModel model = GetModel();
            SelectedMenuItemModelCell modelCell = model.ModelCells.Where(x => x.MenuItem.Id == menuItemId).FirstOrDefault();
            if ( modelCell != null)
            {
                model.ModelCells.Remove(modelCell);
                model.LastUpdated = DateTime.Now;
                SaveModel(model);
            }
        }

        public static void AddNewItem(Entities.MenuItem menuItem, int restaurantId)
        {
            SelectedMenuItemModel model = GetModel();
            SelectedMenuItemModelCell modelCell = new SelectedMenuItemModelCell()
            {
                RestaurantId = restaurantId,
                MenuItem = menuItem
            };
            if ( model.ModelCells.Any(x=> x.RestaurantId == restaurantId) )
            {
                List<SelectedMenuItemModelCell> modelCells = new List<SelectedMenuItemModelCell>();
                modelCells = model.ModelCells.Where(x => x.RestaurantId == restaurantId).ToList();
                for (int i = 0; i < modelCells.Count; i++)
                    model.ModelCells.Remove(modelCells[i]);
            }
            model.ModelCells.Add(modelCell);
            model.LastUpdated = DateTime.Now;
            SaveModel(model);
        }

        public static Entities.MenuItem GetSelectedMenuItem(int restaurantId)
        {
            SelectedMenuItemModel model = GetModel();
            SelectedMenuItemModelCell modelCell = model.ModelCells.Where(x => x.RestaurantId == restaurantId).FirstOrDefault();
            if (modelCell != null)
                return modelCell.MenuItem;
            return null;
        }

        public static void ClearModel()
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(KEY_MENUITEM))
                Xamarin.Essentials.Preferences.Remove(KEY_MENUITEM);
        }


    }
}
