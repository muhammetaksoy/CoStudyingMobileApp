using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Recipe:EntityBase
    {
        public int Text { get; set; }

        public string PhotoPath1 { get; set; }

        public string PhotoPath2 { get; set; }

        public string PhotoPath3 { get; set; }

        public string VideoUrl { get; set; }

        public string Header { get; set; }

        public int MenuItem1 { get; set; }

        public int RelatedRestaurantId { get; set; }

        public bool isActive { get; set; }

        public virtual List<FavoriteRecipe> FavoriteRecipes { get; set; }

        public virtual List<RecipeRead> RecipeReads { get; set; }

        public Recipe()
        {
            FavoriteRecipes = new List<FavoriteRecipe>();
            RecipeReads = new List<RecipeRead>();
        }



    }
}
