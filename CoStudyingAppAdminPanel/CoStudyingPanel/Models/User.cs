using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoStudyingPanel.Models
{
    [Table("Users")]
    public class User:EntityBase
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }

        public bool isActive { get; set; }
        public string PhoneNumber { get; set; }
        public string SchoolName { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Role { get; set; }      
        public bool isStandartLogin { get; set; }
        public bool isFacebookLogin { get; set; }
        public bool isGoogleLogin { get; set; }
        //public virtual List<FriendShipRequest> FriendShipRequests { get; set; }
        //public virtual List<FriendShip> FriendShips { get; set; }
        public virtual List<Notification> Notifications { get; set; }

        public virtual List<ChannelUser> ChannelUsers { get; set; }

        public virtual List<UserRoom> UserRooms { get; set; }

        public virtual Department Department { get; set; }
   
        public User()
        {
            //FriendShipRequests = new List<FriendShipRequest>();
            ChannelUsers = new List<ChannelUser>();
            UserRooms = new List<UserRoom>();
            Notifications = new List<Notification>();
            //FriendShips = new List<FriendShip>();

        }

    }
}
