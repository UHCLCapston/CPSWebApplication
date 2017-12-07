using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class AuditCPSController : Controller
    {
        // GET: AuditCPS
        [HttpGet]
        public ActionResult MakeAuditCPS(int id)
        {
            string userName;
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel model = new DesignCPSViewModel();
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            if (mgr.AuditingNeeded(id.ToString().Trim()))
            {
                model = mgr.getAlreadyCreatedDraftCPSToShow(id.ToString());
                //new if else needed for new and prev audit
            }
            else if (mgr.AuditingOnSavedCPS(id.ToString().Trim())){

            }else if (mgr.AuditingOnModifiedCPS(id.ToString().Trim())){

            }
            else
            {
                TempData["Message"] = "Student Draft CPS is not yet ready to Audit";
                return RedirectToAction("AduitCPS", "AcademicAdvisor");
            }           
            TempData["Model"] = model;
            return View(model);
        }

        public ActionResult MakeAuditCPS()
        {
            string userName;
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SaveChangesOnAuditCPS(DesignCPSViewModel mdl, String action)
        {
            string userName;
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
            switch (action)
            {
                case "save":
                    

                case "saveDraft":
                   
                case "back":
                    return RedirectToAction("AcademicAdvisor", "Home", new { id = Convert.ToInt32(Session["UserID"]) });
            }
            return View();
        }

    }
}