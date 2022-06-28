using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class SelectedMenuItemModel
    {
        public List<SelectedMenuItemModelCell> ModelCells { get; set; }
        public DateTime LastUpdated { get; set; }


        public SelectedMenuItemModel()
        {
            ModelCells = new List<SelectedMenuItemModelCell>();
        }
    }

    public class SelectedMenuItemModelCell
    {
        public MenuItem MenuItem { get; set; }
        public int RestaurantId { get; set; }

    }
}
