
using CoStudyApp.Models.DBModels;
using CoStudying.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CoStudying.Models
{
    public class DatabaseContext:DbContext
    {
       
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoom> UserRooms { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ChannelUser> ChannelUsers { get; set; }
        public DbSet<ChatChannel> ChatChannels { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Documentation> Documentations { get; set; }
        public DbSet<FriendShip> FriendShips { get; set; }
        public DbSet<FriendShipRequest> FriendShipRequests { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<DepartmentAnnouncement> DepartmentAnnouncements { get; set; }
        public DbSet<University> Universities { get; set; }

        public DbSet<UserRoomTask> UserRoomTasks { get; set; }

        public DbSet<PersonalTask> PersonalTasks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}