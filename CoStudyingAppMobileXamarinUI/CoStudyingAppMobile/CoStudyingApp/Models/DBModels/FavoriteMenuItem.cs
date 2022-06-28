using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FavoriteMenuItem:EntityBase
    {
        public int UserId { get; set; }

        public int MenuItemId { get; set; }

        public virtual User User { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
