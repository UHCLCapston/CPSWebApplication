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
            GenerateCPSManager gmgr = new GenerateCPSManager();
            DesignCPSViewModel viewM=  mg.getModelForDesignCPSToView(id);           
            if(Session["UserID"] != null)
            {
                flag = true;
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
            TempData["StudentID"] = id.ToString().Trim();
            TempData["AcademicYear"] = viewM.academicYear;
            TempData["CoreCourses"] = viewM.CoreClassesList;
            TempData["ElectiveCourses"] = viewM.ElectiveClassesList;
            List<Course> listAll = viewM.FoundationClassesList;
            TempData["foundationList"] = listAll;
            if (mg.alreadyDesignedCPS(id.ToString().Trim()))
            {
               DesignCPSViewModel model = gmgr.getModelForGenerateCPS(id.ToString().Trim());
                List<Course> listAssigned = model.FoundationClassesList;
                foreach(Course c in listAll){
                    foreach(Course cAsg in listAssigned)
                    {
                        if (cAsg.CourseShortName.Equals(c.CourseShortName))
                        {
                            c.IsSelected = true;
                        }

                    }
                }
                viewM.FoundationClassesList = listAll;
                TempData["foundationList"] = listAll;
                return View(viewM);
            }
           
            return View(viewM);
        }


       
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentCPSDesignTest(DesignCPSViewModel mdl, string action)
        {
            bool saveOnChanges = false;
            List<Course> fclist = (List<Course>)TempData["foundationList"];

            List<Course> ccList = (List<Course>)TempData["CoreCourses"];
            List<Course> ecList = (List<Course>)TempData["ElectiveCourses"];

            string acYear = TempData["AcademicYear"].ToString();
            string stId = TempData["StudentID"].ToString();

            List<Course> fc = mdl.FoundationClassesList;
            string facultyName = mdl.assignedFaculty;
            List<Course> assignedCourses = new List<Course>();
            CPSDesignManager mgr = new CPSDesignManager();
            CPSDraftToFinalManager cpsmgr = new CPSDraftToFinalManager();
            List<int> listIndex = new List<int>();
            int count = 0;
            foreach (Course c in fc)
            {
                if (c.IsSelected)
                {
                    int i = fc.IndexOf(c);
                    listIndex.Add(count);
                }
                count = count + 1;
            }

            if (listIndex.Count > 0)
            {
                foreach (int i in listIndex)
                {
                    Course course = fclist.ElementAt(i);
                    assignedCourses.Add(course);
                    saveOnChanges = true;
                }
            }
            else
            {
                assignedCourses = null;
                saveOnChanges = true;

            }
            switch (action)
            {
                case "save":               
                    if (saveOnChanges)
                    {
                        mgr.updateStudentDetails(stId,assignedCourses,facultyName);
                        cpsmgr.insertNewBlanckCPSToCPSDB(stId,ccList,ecList,acYear);
                        TempData["Message"] = "Blank CPS Design Created Successfully, Start with another.";
                    }
                    return RedirectToAction("DesignCPS", "AcademicAdvisor");

                case "saveAsDraft":
                    if (saveOnChanges)
                    {
                        mgr.updateStudentDetails(stId, assignedCourses, facultyName);
                        cpsmgr.insertNewBlanckCPSToCPSDB(stId, ccList, ecList, acYear);
                    }
                    TempData["Message"] = "Blank CPS saved successfully";

                    return RedirectToAction("StudentCPSDesign", "DesignCPS", new { id = Convert.ToInt32(stId) });

                case "back":
                    return RedirectToAction("AcademicAdvisor", "Home", new { id =Convert.ToInt32(Session["UserID"]) });

            }

           
            return View();
        }

    }
}