using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        [HttpGet]
        public ActionResult ViewBlankCPS()
        {
            string username;
            if (Session["UserID"] != null)
            {
                username = Session["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            string id = Session["UserId"].ToString();
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel vm = mgr.getBlanckCPSToViewFromCPS(id);
            return View(vm);
        }

        [HttpGet]
        public ActionResult ViewDraftCPS()
        {
            string uname;
            string uid;
            if (Session["UserID"] != null)
            {
                uname = Session["UserName"].ToString();
                uid = Session["UserID"].ToString();
            }
            else {
                return RedirectToAction("LogIn", "Account");
            }
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel vm = mgr.getAlreadyCreatedDraftCPSToShow(uid);
            if ((vm.FinalizeCPSAllow == null && vm.FinalizeCPSAllow.Equals("No")) || (vm.FinalizeCPSAllow != null && vm.FinalizeCPSAllow.Equals("Yes")))
            {
                return View(vm);
            }
            else
            {
                TempData["Message"] = "Draft CPS is not Ready for " + uname + ".";
                return RedirectToAction("Student", "Home", new { id = Convert.ToInt32(uid) });
            }
        }

        public ActionResult ViewfinalCPS()
        {
            string uname;
            string uid;
            if (Session["UserID"] != null)
            {
                uname = Session["UserName"].ToString();
                uid = Session["UserID"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel vm = mgr.getAlreadyCreatedDraftCPSToShow(uid);
            if (vm.FinalizeCPSAllow != null && vm.FinalizeCPSAllow.Equals("Yes"))
            {
                return View(vm);
            }
            else
            {
                TempData["Message"] = "Final CPS is not Ready for " + uname + ".";
                return RedirectToAction("Student", "Home", new { id = Convert.ToInt32(uid) });
            }
        }

    }
}