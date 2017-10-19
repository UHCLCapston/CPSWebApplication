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
        public ActionResult DesignCPS()
        {
            return View();
        }


        public ActionResult DesignCPS(DesignCPSViewModel mdl) {


            CPSDesignManager mg = new CPSDesignManager();
            DesignCPSViewModel v = new DesignCPSViewModel();
            

            string id = mdl.searchId;
            if (ModelState.IsValid) {
                if (!mg.doesStudentExist(id))
                {
                    return RedirectToAction("", "");
                }
            }
                string mjr = mg.getStudentMajor(id.ToString());
                string ctlg = mg.catalogNeedsTofollow(id.ToString());

                v.CoreClassesList = mg.getListCoreCourses(mjr, ctlg);
                v.FoundationClassesList = mg.getListFoundation(mjr, ctlg);
           

            return View(v);

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