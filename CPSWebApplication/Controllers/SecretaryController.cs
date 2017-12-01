using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.DB;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class SecretaryController : Controller
    {
        private capf17gswen4Entities db = new capf17gswen4Entities();

        // GET: Secretary
        public ActionResult StudentList()
        {
            return View(db.CPS.ToList());
        }

        // GET: Secretary/Details/5
        [HttpGet]
        public ActionResult Details(string id)
        {
            bool flag = false;
            string studentId = id.ToString();

            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel vm = mgr.getModelForGenerateDraftCPS(studentId);

            if (Session["UserID"] != null)
            {
                flag = true;
            }

            return View(vm);
        }
        [HttpPost]
        public ActionResult Details()
        {
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
