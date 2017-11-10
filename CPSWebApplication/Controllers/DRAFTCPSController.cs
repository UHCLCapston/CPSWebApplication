using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class DraftCPSController : Controller
    {
        // GET: DRAFTCPS
        public ActionResult GenerateDraftCPS()
        {
            string userName;
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
           return View();
        }

        [HttpGet]
        public ActionResult GenerateDraftCPS(int id)
        {
            string userName;
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
            DesignCPSViewModel model = mgr.getDraftCPSModelToShow(id.ToString());
            return View(model);
        }

    }
}