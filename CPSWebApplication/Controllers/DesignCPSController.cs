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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult StudentCPSDesign(int id)
        {
            bool flag = false;
            CPSDesignManager mg = new CPSDesignManager();

            string  mjr = mg.getStudentMajor(id.ToString());
            string ctlg = mg.catalogNeedsTofollow(id.ToString());
            
            string lastName = mg.getStudentLastName(id.ToString());

            DesignCPSViewModel v = new DesignCPSViewModel();
            v.searchId = id.ToString();
            v.lastName = lastName;
            v.majorName = mjr;
        
            List<Course> fclist = mg.getListFoundation(mjr, ctlg);
            v.FoundationClassesList = fclist;
            v.CoreClassesList = mg.getListCoreCourses(mjr, ctlg);
            ctlg = Regex.Replace(ctlg, "^Catalog", "Academic Year");
            v.academicYear = ctlg;

            TempData["StudentID"] = id.ToString();
            TempData["foundationList"] = fclist;
            if(Session["UserID"] != null)
            {
                flag = true;
            }
            
            return View(v);
        }


       
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentCPSDesignTest(DesignCPSViewModel mdl, string action)
        {
            bool saveOnChanges = false;
            List<Course> fclist = (List<Course>)TempData["foundationList"];
            string stId = TempData["StudentID"].ToString();

            List<Course> fc = mdl.FoundationClassesList;

            List<Course> assignedCourses = new List<Course>();
            CPSDesignManager mgr = new CPSDesignManager();
            
            List<int> listIndex = new List<int>();


            switch (action)
            {
                case "save":
                    int count = 0;
                    foreach (Course c in fc)
                    {
                        if (c.IsSelected)
                        {
                            int i= fc.IndexOf(c);
                            listIndex.Add(count);
                        }
                        count = count + 1;
                    }

                    foreach (int i in listIndex)
                    {
                        Course course = fclist.ElementAt(i);
                        assignedCourses.Add(course);
                        saveOnChanges = true;
                    }

                    if (saveOnChanges)
                    {
                        mgr.updateStudentDetails(stId, assignedCourses);
                        TempData["Message"] = "Profile Updated Successfully, Start with another.";
                    }

                    return RedirectToAction("DesignCPS", "AcademicAdvisor");


                case "back":
                    return RedirectToAction("AcademicAdvisor", "Home", new { id =Convert.ToInt32(Session["UserID"]) });

            }

           
            return View();
        }

    }
}