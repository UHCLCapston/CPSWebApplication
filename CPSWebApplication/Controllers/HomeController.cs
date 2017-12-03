using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;


namespace CPSWebApplication.Controllers
{
    public class HomeController : Controller
    {
        UserManager userManager = new UserManager();
       public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                string userID = Session["UserID"].ToString();
            }
            return View();
        }
       
        public ActionResult Welcome()
        {
            if (Session["UserID"] != null)
            {
                string userID = Session["UserID"].ToString();
            }
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserID"] != null)
            {
                string userID = Session["UserID"].ToString();
            }
            
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["UserID"] != null)
            {
                string userID = Session["UserID"].ToString();
            }
            
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult AcademicAdvisor(int id)
        {
            if (Session["UserID"] != null)
            {
                UserModel user1 = userManager.GetUserNameByID(id);
                return View(user1);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            UserModel user = userManager.GetUserNameByID(id);
            return View(user);
        }

        public ActionResult Faculty(int id)
        {
            if (Session["UserID"] != null)
            {
                UserModel user1 = userManager.GetUserNameByID(id);
                return View(user1);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            UserModel user = userManager.GetUserNameByID(id);
            return View(user);
        }

        public ActionResult Student(int id)
        {
            if (Session["UserID"] != null)
            {
                UserModel user1 = userManager.GetUserNameByID(id);
                return View(user1);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            UserModel user = userManager.GetUserNameByID(id);
            return View(user);
            
        }

        public ActionResult Secretary(int id)
        {
            if (Session["UserID"] != null)
            {
                UserModel user1 = userManager.GetUserNameByID(id);
                return View(user1);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            UserModel user = userManager.GetUserNameByID(id);
            return View(user);

        }

    }
}