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
            return View();
        }

        [HttpPost]
        public ActionResult GenerateDraftCPS(int id)
        {
            return View();
        }




    }
}