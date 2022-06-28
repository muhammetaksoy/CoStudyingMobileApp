using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoStudyApp.Models.DBModels
{
    public class Room:EntityBase
    {
        public virtual List<UserRoom> UserRooms { get; set; }

        public virtual List<Invite> Invites { get; set; }

        //public virtual List<ChatChannel> ChatChannels { get; set; }

        public string Type { get; set; } // Club, StudyRoom

        public string RoomName { get; set; }

        public int CreatorId { get; set; }

        public int CreatorDepartmentId { get; set; }

        public string RoomAccessCode { get; set; }

        public bool isPrivateRoom { get; set; }

        public string Description { get; set; }

        public Room()
        {
            UserRooms = new List<UserRoom>();
            Invites = new List<Invite>();
        }


    }
}
