using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;
using System.Text.RegularExpressions;

namespace CPSWebApplication.Controllers
{
    public class DesignCPSController : Controller
    {
        // GET: DesignCPS
        public ActionResult Search()
        {
            return View();
        }

       
        public ActionResult StudentCPSDesign(int id)
        {
            CPSDesignManager mg = new CPSDesignManager();

            string  mjr = mg.getStudentMajor(id.ToString());
            string ctlg = mg.catalogNeedsTofollow(id.ToString());
            ctlg = Regex.Replace(ctlg, "^Catalog", "Academic Year");
            string lastName = mg.getStudentLastName(id.ToString());

            DesignCPSViewModel v = new DesignCPSViewModel();
            v.searchId = id.ToString();
            v.lastName = lastName;
            v.majorName = mjr;
            v.academicYear = ctlg;
            v.CoreClassesList = mg.getListCoreCourses(mjr, ctlg);
            v.FoundationClassesList = mg.getListFoundation(mjr, ctlg);

            return View(v);
        }

    }
}