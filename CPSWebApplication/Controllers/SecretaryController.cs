using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.DB;

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
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CPS cPS = db.CPS.Find(id);
            if (cPS == null)
            {
                return HttpNotFound();
            }
            return View(cPS);
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
