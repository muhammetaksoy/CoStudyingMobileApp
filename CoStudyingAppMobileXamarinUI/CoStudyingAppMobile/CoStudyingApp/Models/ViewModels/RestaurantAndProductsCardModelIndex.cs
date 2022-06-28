using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YeatyAppMobile.Models.ViewModels
{
    public class RestaurantAndProductsCardModelIndex
    {

        public RestaurantCardModelBasic RestaurantModel { get; set; }

        public IList<ProductCardModelIndex> productModels { get; set; }

        public RestaurantAndProductsCardModelIndex()
        {
            productModels = new ObservableCollection<ProductCardModelIndex>();
        }
    }
}
