using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.ResponseModels
{
    public class FavoriteMenuItemResponseModel
    {

        public MenuItem MenuItem { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
