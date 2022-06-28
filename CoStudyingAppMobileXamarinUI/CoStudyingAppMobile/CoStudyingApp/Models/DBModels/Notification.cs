using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Notification:EntityBase
    {
        public int UserId { get; set; }

        public string Text { get; set; }

        public string Header { get; set; }

        public int RelatedRestaurantId { get; set; }

        public int RelatedMenuItemId { get; set; }

        public int RelatedCampaignId { get; set; }

        public int RelatedArticleId { get; set; }

        public int RelatedRecipeId { get; set; }

        public string Type { get; set; }

        public bool isRead { get; set; }

        public string PhotoPath { get; set; }

        public virtual User  User { get; set; }

    }
}
