using CoStudyApp.Models.DBModels;
using CoStudying.Models;
using CoStudying.Models.BusinessModels;
using CoStudying.Models.DBModels;
using CoStudying.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoStudying.Controllers
{
    public class LogonController : ApiController
    {

        public DatabaseContext GetDatabaseContext()
        {
            DatabaseContext context = new DatabaseContext();
            context.Configuration.LazyLoadingEnabled = false;
            return context;
        }


        [HttpGet]
        public HttpResponseMessage ValidateEmail([FromUri] string email) // 200:USABLE ,  400:InvalidUsername , 409 : Already taken , 500 ınternal server
        {
            try
            {
                var context = GetDatabaseContext();
                bool isUsed = context.Users.Any(x => x.Email == email);
                if (isUsed)
                {
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public HttpResponseMessage ValidateUsername([FromUri] string userName) // 200:USABLE ,  400:InvalidUsername , 409 : Already taken
        {
            try
            {
                var context = GetDatabaseContext();
                bool isUsed = context.Users.Any(x => x.Username == userName);
                if (isUsed)
                {
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [HttpGet]
        public RegisterResponseModel BasicRegister([FromBody] User user)
        {
            bool flag1 = false;
            try
            {
                var context = GetDatabaseContext();
                bool isFound = context.Users.Any(x => x.Email == user.Email || x.Username == user.Username);
                if (isFound)
                {
                    return new RegisterResponseModel() { isSuccess = false, ReturnMessage = "Kullanıcı adı veya eposta kullanılıyor.", User = null };
                }
                string newSalt = ManageHashing.CreateSalt();
                string newPass = ManageHashing.SHA256(user.Password, newSalt);
                User newUser = new User()
                {
                    //isActive = true,
                    CreatedOn = CustomTime.GetNowTime(),
                    //DateOfBirth = CustomTime.GetNowTime(),
                    Email = user.Email,
                    Username = user.Username,
                    Name = user.Name,
                    //WalletCurrency = "TRY",
                    Salt = newSalt,
                    Password = newPass,
                    Role = "User",
                    DepartmentId = user.DepartmentId,
                    ProfilePicturePath = "https://avios.pl/wp-content/uploads/2018/01/no-avatar.png",
                    Surname = user.Surname,
                    isFacebookLogin = false,
                    isGoogleLogin = false,
                    isStandartLogin = false,
                    //isAppleLogin = false,
                };
                if (user.isFacebookLogin)
                {
                    //newUser.isFacebookLogin = true;
                    //newUser.FacebookLoginPass = newPass;
                }
                else if (user.isGoogleLogin)
                {
                    //newUser.isGoogleLogin = true;
                    //newUser.GoogleLoginPass = newPass;
                    //newUser.Name = NameConversions.FixGoogleName(user.Name);
                }
                //else if (user.isAppleLogin)
                //{
                //    newUser.isAppleLogin = true;
                //    newUser.AppleLoginPass = newPass;
                //}
                else
                {
                    newUser.isStandartLogin = true;
                }
                //if (user.RegisterCode != null)
                //    newUser.RegisterCode = user.RegisterCode;
                if (user.ProfilePicturePath != null && !user.isGoogleLogin)
                    newUser.ProfilePicturePath = user.ProfilePicturePath;
                context.Users.Add(newUser);
                flag1 = true;
                context.SaveChanges();
                //mailService.SendWelcomeMessage(user.Username, user.Email);
                return new RegisterResponseModel() { isSuccess = true, ReturnMessage = "Success", User = newUser };
            }
            catch (Exception ex)
            {

                return new RegisterResponseModel() { customException = ex,isSuccess = false, ReturnMessage = ex.Message + (flag1 ? "-ok" : "false"), User = null };
            }
        }

        [Authorize]
        [HttpGet]
        public LoginModel Login()
        {
            try
            {
                var context = GetDatabaseContext();
                UserInfo loggedUser = UserInfoMethod.CurrentUser();
                int userId = loggedUser.UserId;
                User currentUser = context.Users.Include("ChannelUsers").Include("UserRooms").
                    AsNoTracking().Where(x => x.Id == userId).FirstOrDefault();
                int countNotifications = context.Notifications.Where(x => x.UserId == userId && x.isRead == false).Take(20).Count();
                Department department = context.Departments.Include("Faculty").AsNoTracking().Where(x => x.Id == currentUser.DepartmentId).FirstOrDefault();
                List<FriendShip> friendShips = context.FriendShips.Where(x =>  x.User1Id == currentUser.Id || x.User2Id == currentUser.Id).ToList();
                List<FriendShipRequest> friendShipRequests = context.FriendShipRequests.Where(x => x.RecieverId == currentUser.Id && x.isAccepted == false).OrderByDescending(x=> x.Id).ToList();
                currentUser.Salt = "";
                currentUser.Password = "";
                return new LoginModel()
                {
                    friendShipRequests = friendShipRequests,
                    friendShips = friendShips,
                    department = department,
                    faculty = department.Faculty,
                    currentUser = currentUser,
                    countUnreadNotifications = countNotifications
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
