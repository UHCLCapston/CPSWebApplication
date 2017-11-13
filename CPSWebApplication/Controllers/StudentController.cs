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
            string id = Session["UserId"].ToString();
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel vm = mgr.getBlanckCPSToViewFromCPS(id);
            return View(vm);
        }

        [HttpGet]
        public ActionResult ViewDraftCPS()
        {
            string id;
            if (Session["UserID"] != null)
            {
                id = Session["UserID"].ToString();
            }
            id = Session["UserID"].ToString();
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel vm = mgr.getModelForGenerateDraftCPS(id);
            return View(vm);
        }

        public ActionResult ViewfinalCPS()
        {
            string id;
            if (Session["UserID"] != null)
            {
                id = Session["UserName"].ToString();
            }
            return View();
        }

    }
}