using CoStudyingPanel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CoStudyingPanel.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult AddAnnouncement()
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = Session["login"] as User;
            DepartmentAnnouncement announcement = new DepartmentAnnouncement();
            announcement.DepartmentId = user.DepartmentId;
            return View(announcement);
        }

        [HttpPost]
        public ActionResult AddAnnouncement(DepartmentAnnouncement announcement)
        {
            db.DepartmentAnnouncements.Add(new DepartmentAnnouncement()
            {
                CreatedOn = CustomTime.GetNowTime(),
                begin = announcement.begin,
                DepartmentId = announcement.DepartmentId,
                finish = announcement.finish,
                isOnSchedule = announcement.isOnSchedule,
                Text = announcement.Text
            });
            db.SaveChanges();
            return RedirectToAction("ListAnnouncements");
        }
        public ActionResult ListAnnouncements()
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<DepartmentAnnouncement> announcements = db.DepartmentAnnouncements.Include("Department").Where(a => a.isActive).OrderByDescending(a => a.CreatedOn).ToList();
            return View(announcements);
        }
        public ActionResult RemoveAnnouncement(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            DepartmentAnnouncement department = db.DepartmentAnnouncements.Find(id);
            department.isActive = false;
            db.SaveChanges();
            return RedirectToAction("ListAnnouncements");
        }

        public ActionResult EditUniversity(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            University uni = db.Universities.Find(id);
            return View(uni);
        }
        [HttpPost]
        public ActionResult EditUniversity(University university)
        {
            List<University> universities = db.Universities.Where(a => a.isActive && a.Id != university.Id).ToList();
            foreach (University item in universities)
            {
                if (item.UniversityName == university.UniversityName)
                {
                    ModelState.AddModelError("", university.UniversityName + " zaten kayıtlı olduğu için değiştirilemez!");
                    return View(university);
                }
            }
            University uni = db.Universities.Find(university.Id);
            uni.UniversityName = university.UniversityName;
            uni.City = university.City;
            db.SaveChanges();
            return RedirectToAction("ListUniversities", "Home");
        }


        public ActionResult PassiveUniversity(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            
            University uni = db.Universities.Find(id);
            uni.isActive = false;
            db.SaveChanges();
            return RedirectToAction("ListUniversities", "Home");
        }

        public ActionResult ListUniversities(int page = 1, int pagesize = 10)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<University> Universities = db.Universities.Include("Faculties").Where(a => a.isActive).OrderByDescending(a => a.CreatedOn).ToList();
            PagedList<University> final = new PagedList<University>(Universities, page, pagesize);
            return View(final);
        }

        public ActionResult AddUniversity()
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddUniversity(University university)
        {
            List<University> universities = db.Universities.Where(a => a.isActive && a.City == university.City).ToList();
            foreach(University item in universities)
            {
                if(item.UniversityName == university.UniversityName)
                {
                    ModelState.AddModelError("", university.UniversityName + " kayıtlı olduğu için yeniden eklenemez!");
                    return View(university);
                }
            }

            db.Universities.Add(new University()
            {
                CreatedOn = CustomTime.GetNowTime(),
                isActive = true,
                City = university.City,
                UniversityName = university.UniversityName
            });
            db.SaveChanges();


            return RedirectToAction("ListUniversities", "Home");
        }


        public ActionResult AddFaculty()
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.isActive == true).OrderBy(a => a.UniversityName), "Id", "UniversityName");

            return View();
        }

        [HttpPost]
        public ActionResult AddFaculty(Faculty faculty)
        {
            List<Faculty> faculties = db.Faculties.Where(a => a.isActive && a.UniversityId == faculty.UniversityId).ToList();
            foreach (Faculty item in faculties)
            {
                if (item.FacultyName == faculty.FacultyName)
                {
                    ModelState.AddModelError("", faculty.FacultyName + " kayıtlı olduğu için yeniden eklenemez!");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.isActive == true).OrderBy(a => a.UniversityName), "Id", "UniversityName");

                    return View(faculty);
                }
            }

            db.Faculties.Add(new Faculty()
            {
                CreatedOn = CustomTime.GetNowTime(),
                isActive = true,
                UniversityId = faculty.UniversityId,
                FacultyName = faculty.FacultyName,
            });
            db.SaveChanges();


            return RedirectToAction("ListFaculties", "Home");
        }

        public ActionResult ListFaculties(int page = 1, int pagesize = 10)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Faculty> faculties = db.Faculties.Include("University").Where(a => a.isActive).OrderByDescending(a => a.CreatedOn).ToList();
            PagedList<Faculty> final = new PagedList<Faculty>(faculties, page, pagesize);
            return View(final);
        }

        public ActionResult RemoveFaculty(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            Faculty faculty = db.Faculties.Find(id);
            faculty.isActive = false;
            db.SaveChanges();
            return RedirectToAction("ListFaculties", "Home");
        }

        public ActionResult AddDepartman()
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int id = (int)TempData["UniId"];

            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.isActive == true && a.UniversityId == id).OrderBy(a => a.FacultyName), "Id", "FacultyName");
            DepartmentModel model = new DepartmentModel();
            model.UniversityId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDepartman(DepartmentModel department)
        {
            List<Department> departments = db.Departments.Where(a => a.isActive && a.FacultyId == department.FacultyId).ToList();
            foreach (Department item in departments)
            {
                if (item.DepartmentName == department.DepartmentName)
                {
                    ModelState.AddModelError("", department.DepartmentName + " kayıtlı olduğu için yeniden eklenemez!");
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.isActive == true && a.UniversityId == department.UniversityId).OrderBy(a => a.FacultyName), "Id", "FacultyName");
                    return View(department);
                }
            }

            db.Departments.Add(new Department()
            {
                CreatedOn = CustomTime.GetNowTime(),
                isActive = true,
                FacultyId = department.FacultyId,
                DepartmentName = department.DepartmentName,
            });
            db.SaveChanges();


            return RedirectToAction("ListDepartments", "Home");
        }
        public ActionResult AddDepartment()
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.isActive == true).OrderBy(a => a.UniversityName), "Id", "UniversityName");

            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(DepartmentModel model)
        {
            TempData["UniId"] = model.UniversityId;

            return RedirectToAction("AddDepartman");
        }

        public ActionResult ListDepartments(int page = 1, int pagesize = 10)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Department> faculties = db.Departments.Include("Faculty").Include("Faculty.University").Where(a => a.isActive).OrderByDescending(a => a.CreatedOn).ToList();
            PagedList<Department> final = new PagedList<Department>(faculties, page, pagesize);
            return View(final);
        }


        public ActionResult RemoveDepartment(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            Department department = db.Departments.Find(id);
            department.isActive = false;
            db.SaveChanges();
            return RedirectToAction("ListFaculties", "Home");
        }
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult Login()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return View();
        }

        [HttpPost]
        public ActionResult Login(User loginuser)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int count = 0;
            if (loginuser.Username.Length >= 70)
            {
                ModelState.AddModelError("", "Email 70 Karakterden Uzun Olamaz!");
                count++;
            }
            if (loginuser.Password.Length >= 70)
            {
                ModelState.AddModelError("", "Şifre 70 Karakterden Uzun Olamaz!");
                count++;
            }
            if (count > 0)
            {
                return View(loginuser);
            }
            List<User> users = db.Users.ToList();
            User user = users.FirstOrDefault(x => x.Username == loginuser.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı!");
                return View(user);
            }

            if (user != null)
            {
                if (user.Role == "User")
                {
                    ModelState.AddModelError("", "Panele Erişiminiz Bulumamaktadır!");
                    return View(user);
                }
                string pass = ManageHashing.SHA256(loginuser.Password, user.Salt);
                if (user.Password != pass)
                {
                    ModelState.AddModelError("", "Hatalı Şifre - Tekrar Deneyiniz!");
                    return View(user);
                }
                else
                {
                    Session["login"] = user;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();


        }
        [HttpPost]
        public ActionResult Search(string text)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = Session["login"] as User;
            if (user.Role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            List<University> unis = db.Universities.ToList();
            List<University> universities = new List<University>();

            if (!string.IsNullOrEmpty(text))
            {
                universities = unis.Where(a => a.UniversityName.ToUpper().Contains(text.ToUpper())
                || a.City.ToUpper().Contains(text.ToUpper())).OrderByDescending(a => a.CreatedOn).ToList();
            }

            List<User> allusers = db.Users.ToList();
            List<User> users = new List<User>();

            if (!string.IsNullOrEmpty(text))
            {
                users = allusers.Where(a => a.Username.ToUpper().Contains(text.ToUpper()) || a.Name.ToUpper().Contains(text.ToUpper())
                || a.Email.ToUpper().Contains(text.ToUpper()) 
                || a.Role.ToUpper().Contains(text.ToUpper())
                ).OrderByDescending(a => a.CreatedOn).ToList();
            }



            SearchResult result = new SearchResult();
            result.Universities = universities;
            result.Users = users;
            result.text = text;

            return View(result);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session["login"] = null;
            return RedirectToAction("Login", "Home");

        }
    }
}