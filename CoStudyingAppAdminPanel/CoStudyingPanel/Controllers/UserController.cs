using CoStudyingPanel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoStudyingPanel.Controllers
{
    public class UserController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult EditUser(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            User changeuser = db.Users.Find(user.Id);
            changeuser.Name = user.Name;
            changeuser.Surname = user.Surname;
            changeuser.Username = user.Username;
            changeuser.Email = user.Email;
            changeuser.Role = user.Role;
            changeuser.PhoneNumber = user.PhoneNumber;
            changeuser.SchoolName = user.SchoolName;
            changeuser.DepartmentId = user.DepartmentId;
            db.SaveChanges();
            if (changeuser.isActive)
            {
                return RedirectToAction("ActiveUsers");
            }
            else
                return RedirectToAction("PassiveUsers");
        }

        public ActionResult ActiveUsers(int page = 1, int pagesize = 10)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            PagedList<User> users = new PagedList<User>(db.Users.OrderByDescending(a => a.CreatedOn).Where(a => a.isActive).ToList(), page, pagesize);
            return View(users);

        }
        public ActionResult PassiveUsers(int page = 1, int pagesize = 10)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            PagedList<User> users = new PagedList<User>(db.Users.OrderByDescending(a => a.CreatedOn).Where(a => a.isActive == false).ToList(), page, pagesize);
            return View(users);

        }

        public ActionResult DeactivateUser(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
         
           User changeuser = db.Users.Find(id);
           changeuser.isActive = false;
           db.SaveChanges();
            return RedirectToAction("ActiveUsers");
        }
        public ActionResult ActivateUser(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            User changeuser = db.Users.Find(id);
            changeuser.isActive = true;
            db.SaveChanges();
            return RedirectToAction("PassiveUsers");
        }


    }
}