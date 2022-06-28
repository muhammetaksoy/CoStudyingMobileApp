using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class RecipeRead:EntityBase
    {
        public int UserId { get; set; }

        public int RecipeId { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
