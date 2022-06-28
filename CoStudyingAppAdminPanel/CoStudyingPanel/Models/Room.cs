using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoStudyingPanel.Models
{
    [Table("Rooms")]
    public class Room:EntityBase
    {
        public virtual List<UserRoom> UserRooms  { get; set; }

        public virtual List<ChatChannel> ChatChannels { get; set; }

        public string Type { get; set; }


    }
}
