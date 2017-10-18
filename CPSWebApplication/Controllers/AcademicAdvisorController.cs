using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class AcademicAdvisorController : Controller
    {
        // GET: AcademicAdvisor

        public ActionResult DesignCPS() {
            return View();
        }



        public ActionResult GenerateCPS()
        {
            return View();
        }

        public ActionResult ModifyCPS()
        {
            return View();
        }



        public ActionResult FinalizeCPS()
        {
            return View();
        }

        public ActionResult AduitCPS()
        {
            return View();
        }

      



    }
}