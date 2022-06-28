using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoStudyingPanel.Models
{
    [Table("FriendShipRequests")]
    public class FriendShipRequest:EntityBase
    {
        public int SenderId { get; set; }

        public int RecieverId { get; set; }

        public bool isAccepted { get; set; }


    }
}
