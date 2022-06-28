using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ViewModels
{
    public class SelectedMenuItemViewModel
    {
        public MenuItem MenuItem { get; set; }

        public string MenuItemNameAndPrice
        {
            get
            {
                return MenuItem.Name + " / " + Math.Round(MenuItem.Price, 2).ToString() + "₺";
            }
        }

        public string MenuItemImageUrl
        {
            get
            {
                return MenuItem.PhotoPath;
            }
        }



    }
}
