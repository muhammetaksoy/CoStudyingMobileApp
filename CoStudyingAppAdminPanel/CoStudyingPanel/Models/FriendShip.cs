using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoStudyingPanel.Models
{
    [Table("FriendShips")]
    public class FriendShip:EntityBase
    {
        public int User1Id { get; set; }

        public int User2Id { get; set; }


    }
}
