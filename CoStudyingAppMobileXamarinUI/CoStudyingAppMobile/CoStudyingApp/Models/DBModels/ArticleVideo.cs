using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ArticleVideo:EntityBase
    {
        public int ArticleId { get; set; }

        public string VideoPath { get; set; }

        public virtual Article Article { get; set; }
    }
}
