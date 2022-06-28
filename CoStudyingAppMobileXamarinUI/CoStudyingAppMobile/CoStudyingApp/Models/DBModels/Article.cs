using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Article:EntityBase
    {
        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }

        public string Text4 { get; set; }

        public string Text5 { get; set; }

        public int UserId { get; set; }

        public int RelatedRestaurantId { get; set; }

        public int RelatedMenuItem1 { get; set; }

        public int RelatedMenuItem2 { get; set; }

        public int RelatedMenuItem3 { get; set; }

        public int RelatedMenuItem4 { get; set; }

        public int RelatedMenuItem5 { get; set; }

        public string Header { get; set; }

        public string Link1 { get; set; }

        public string Link2 { get; set; }

        public virtual User User { get; set; }

        public virtual List<ArticleRead> ArticleReads { get; set; }

        public virtual List<FavoriteArticle> FavoriteArticles { get; set; }

        public virtual List<ArticlePhoto> ArticlePhotos { get; set; }

        public virtual List<ArticleVideo> ArticleVideos { get; set; }

        public Article()
        {
            ArticleReads = new List<ArticleRead>();
            FavoriteArticles = new List<FavoriteArticle>();
            ArticlePhotos = new List<ArticlePhoto>();
            ArticleVideos = new List<ArticleVideo>();

        }

    }
}
