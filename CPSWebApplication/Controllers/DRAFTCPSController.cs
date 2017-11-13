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
            TempData["Model"] = model;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveGeneratedDraftCPS(DesignCPSViewModel mdl, string action)
        {
            string userName;
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
            DesignCPSViewModel tempDataModel = (DesignCPSViewModel)TempData["Model"];
            DesignCPSViewModel draftModel = new DesignCPSViewModel();

            draftModel.academicYear = tempDataModel.academicYear;
            draftModel.searchId = tempDataModel.searchId;
            draftModel.firstName = tempDataModel.firstName;
            draftModel.lastName= tempDataModel.lastName;
            draftModel.majorName = tempDataModel.majorName;
            draftModel.assignedFaculty = tempDataModel.assignedFaculty;
            draftModel.programCompletionOption = mdl.programCompletionOption;

            List<Course> fcShown = tempDataModel.FoundationClassesList;
            List<Course> ccShown = tempDataModel.CoreClassesList;
            List<Course> ecSn = tempDataModel.ElectiveClassesList;
            List<String> ecShown = tempDataModel.CourseWholeNameListForElective;

            List<Course> fc = mdl.FoundationClassesList;
            List<Course> cc = mdl.CoreClassesList;
            List<Course> ec = mdl.ElectiveClassesList;

            List<Course> ecNewList = new List<Course>();
            Course ecNewCourse = new Course();
            List<Course> ccNewList = new List<Course>();
            Course ccNewCourse = new Course();
            List<Course> fcNewList = new List<Course>();
            Course fcNewCourse = new Course();

            CPSDraftToFinalManager cpsmgr = new CPSDraftToFinalManager();
   
            switch (action)
            {
                case "save":

                    if(fcShown.Count > 0) {
                    foreach (Course c in fc)
                        {
                            int i = fc.IndexOf(c);                           
                            fcNewCourse = fcShown[i];
                            fcNewCourse.EnrolledSemester = c.EnrolledSemester;
                            fcNewCourse.GradesRecieved = c.GradesRecieved;
                            fcNewList.Add(fcNewCourse);
                        }

                    }
                    foreach (Course c in cc)
                    {
                        int i = cc.IndexOf(c);
                        ccNewCourse = ccShown[i];
                        ccNewCourse.EnrolledSemester = c.EnrolledSemester;
                        ccNewCourse.GradesRecieved = c.GradesRecieved;
                        ccNewList.Add(ccNewCourse);
                    }


                    
                    foreach (Course c in ec)
                    {
                        string cname = c.CourseWholeName;
                        ecNewCourse = getCourseByWholeName(cname,ecSn);
                        ecNewCourse.EnrolledSemester = c.EnrolledSemester;
                        ecNewCourse.GradesRecieved = c.GradesRecieved;
                        ecNewCourse.CourseSubjectWithRubric = c.CourseSubjectWithRubric;
                        ecNewList.Add(ecNewCourse);
                    }

                    draftModel.FoundationClassesList = fcNewList;
                    draftModel.ElectiveClassesList = ecNewList;
                    draftModel.CoreClassesList = ccNewList;

                    cpsmgr.insertUpdateNewDraftCPSToCPSDB(draftModel);
                    TempData["Message"] = "Profile Updated Successfully, Start with another.";
                    
                    return RedirectToAction("CreateDraftCPS", "FacultyAdvisor");
                    
                case "back":
                    return RedirectToAction("Faculty", "Home", new { id = Convert.ToInt32(Session["UserID"]) });
            }

            return View();
        }


        public Course getCourseByWholeName(string wholename, List<Course> ECList )
        {
            foreach(Course c in ECList)
            {
                if (wholename.Equals(c.CourseWholeName))
                {
                    return c;
                } 
            }
            return null;
        }
        /*
        public ActionResult GeneateDraftCPSReload(DesignCPSViewModel mdl, string action)
        {
           DesignCPSViewModel previousView = (DesignCPSViewModel)TempData["Model"];
            string mjr = "SWEN";
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            string type = mdl.programCompletionOption;

            string TotalNumberElectives = mgr.getNumberOfElectivesAsPerCompletionType(type, mjr);
            if (action.Equals("change"))
            {
                previousView.programCompletionOption = type;
                previousView.countElectives = Convert.ToInt32(TotalNumberElectives);
            }
            return View(previousView);

        }
        public ActionResult generateNumberOfElectives(string strType)
        {
            string mjr = "SWEN";
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            string TotalNumberElectives = mgr.getNumberOfElectivesAsPerCompletionType(strType, mjr);

            return Json(TotalNumberElectives, JsonRequestBehavior.AllowGet);
        }
        public ActionResult generateElectiveListForSelectedOption(string subject, string level) {

            return View();
        }
        public ActionResult generateElectiveListForSelectedSubject(string subject)
        {

            return View();
        }*/



    }
}