using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class DesignCPSController : Controller
    {
        // GET: DesignCPS
        public ActionResult Search()
        {
            return View();
        }



        public ActionResult studentCPSDesign(int id)
        {
            CPSDesignManager mg = new CPSDesignManager();

            string  mjr = mg.getStudentMajor(id.ToString());
            string ctlg = mg.catalogNeedsTofollow(id.ToString());

            DesignCPSViewModel v = new DesignCPSViewModel();

            v.CoreClassesList = mg.getListCoreCourses(mjr, ctlg);
            v.FoundationClassesList = mg.getListFoundation(mjr, ctlg);

            return View(v);
        }

    }
}