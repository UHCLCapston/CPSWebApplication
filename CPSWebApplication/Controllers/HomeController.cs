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
            return View();
        }
       
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AcademicAdvisor(int id)
        {
            UserModel user = userManager.GetUserNameByID(id);
            return View(user);

        }

        public ActionResult Faculty(int id)
        {
            UserModel user = userManager.GetUserNameByID(id);
            return View(user);
        }

        public ActionResult Student(int id)
        {
            UserModel user = userManager.GetUserNameByID(id);
            return View(user);
        }
    }
}