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
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            return View();
        }

        [HttpGet]
        public ActionResult GenerateDraftCPS(int id)
        {
            string userName;
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel model = new DesignCPSViewModel();
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            if (!mgr.DraftCPSExists(id.ToString().Trim()))
            {
                 model = mgr.getDraftCPSModelToShow(id.ToString().Trim());
            }
            else
            {
                model = mgr.getAlreadyCreatedDraftCPSToShow(id.ToString());
            }
            TempData["Model"] = model;
            return View(model);
        }

        [HttpGet]
        public ActionResult SaveGeneratedDraftCPS(DesignCPSViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveGeneratedDraftCPS(DesignCPSViewModel mdl, string action)
        {
            string userName;
            if (Session["UserID"] != null)
            {
                userName = Session["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            DesignCPSViewModel tempDataModel = (DesignCPSViewModel)TempData["Model"];
            DesignCPSViewModel draftModel = new DesignCPSViewModel();

            draftModel.academicYear = tempDataModel.academicYear;
            draftModel.searchId = tempDataModel.searchId;
            draftModel.firstName = tempDataModel.firstName;
            draftModel.lastName = tempDataModel.lastName;
            draftModel.majorName = tempDataModel.majorName;
            draftModel.assignedFaculty = tempDataModel.assignedFaculty;
            draftModel.ClassesForCapstonNormal = tempDataModel.ClassesForCapstonNormal;
            draftModel.ClassesForThesisNormal = tempDataModel.ClassesForThesisNormal;
            draftModel.CapstonCourse = tempDataModel.CapstonCourse;
            
            List<Course> thesisShown = tempDataModel.ThesisCourse;
            List<Course> fcShown = tempDataModel.FoundationClassesList;
            List<Course> ccShown = tempDataModel.CoreClassesList;
            List<RubricClasses> ecCapShown = tempDataModel.ClassesForCapstonNormal;
            List<RubricClasses> ecThShown = tempDataModel.ClassesForThesisNormal;
            List<Course> ecAllList = tempDataModel.ElectiveClassesList;
            
            //List<String> ecShown = tempDataModel.CourseWholeNameListForElective;

            draftModel.programCompletionOption = mdl.programCompletionOption;
            draftModel.CapstonCourse.EnrolledSemester = mdl.CapstonCourse.EnrolledSemester;
          
            List<Course> fc = mdl.FoundationClassesList;
            List<Course> cc = mdl.CoreClassesList;
            List<Course> th = mdl.ThesisCourse;
            List<Course> ecTh = mdl.ThesisElectiveClassesList;
            List<Course> ecCap = mdl.CapstonElectiveClassesList;

            //List<Course> ec = mdl.ElectiveClassesList;

            // List<Course> ecNewList = new List<Course>();
            //Course ecNewCourse = new Course();

            List<Course> ccNewList = new List<Course>();
            Course ccNewCourse = new Course();
            List<Course> fcNewList = new List<Course>();
            Course fcNewCourse = new Course();
            List<Course> thNewList = new List<Course>();
            List<Course> ecCAPNewList = new List<Course>();
            List<Course> ecTHSNewList = new List<Course>();

            CPSDraftToFinalManager cpsmgr = new CPSDraftToFinalManager();
            int num = 0;
            foreach (Course c in ecCap)
            {
                int index = th.IndexOf(c);
                string cname = c.CourseWholeName.Trim();
                if (!cname.Equals("---Select---"))
                {
                    Course ecNewCourse = getCourseByWholeName(cname, ecAllList);
                    ecNewCourse.EnrolledSemester = c.EnrolledSemester;
                    ecNewCourse.CourseSubjectWithRubric = ecCapShown[num].rubric;
                    ecCAPNewList.Add(ecNewCourse);
                }
                num++;
            }
            num = 0;
            foreach (Course c in ecTh)
            {
                int index = th.IndexOf(c);
                string cname = c.CourseWholeName.Trim();
                Course crs = new Course();
                if (!cname.Equals("---Select---"))
                {
                    crs = getCourseByWholeName(cname, ecAllList);
                    crs.EnrolledSemester = c.EnrolledSemester;
                    crs.CourseSubjectWithRubric = ecThShown[num].rubric;
                    ecTHSNewList.Add(crs);
                }
                num++;
            }
            num = 0;
            foreach (Course c in th)
            {
                int index = th.IndexOf(c);
                Course cname = thesisShown[num];
                if (!cname.Equals("---Select---"))
                {
                    cname.EnrolledSemester = c.EnrolledSemester;
                    thNewList.Add(cname);
                }
                num++;
            }
            num = 0;
            if (fcShown.Count > 0)
            {
                int count = 0;
                foreach (Course c in fc)
                {
                    int i = fc.IndexOf(c);
                    fcNewCourse = fcShown[count];
                    fcNewCourse.EnrolledSemester = c.EnrolledSemester;
                    fcNewList.Add(fcNewCourse);
                    count++;
                }
                count = 0;
            }
            int cnt = 0;
            foreach (Course c in cc)
            {
              
                int j = cc.IndexOf(c);
                ccNewCourse = ccShown[cnt];
                ccNewCourse.EnrolledSemester = c.EnrolledSemester;
                ccNewList.Add(ccNewCourse);
                cnt++;
            }
            cnt = 0;
            /*foreach (Course c in ec)
            {
                string cname = c.CourseWholeName;
                ecNewCourse = getCourseByWholeName(cname, ecSn);
                ecNewCourse.EnrolledSemester = c.EnrolledSemester;
                ecNewCourse.CourseSubjectWithRubric = c.CourseSubjectWithRubric;
                ecNewList.Add(ecNewCourse);
            }*/
            //

            draftModel.ThesisCourse = thNewList;
            draftModel.FoundationClassesList = fcNewList;
            draftModel.CoreClassesList = ccNewList;
            draftModel.CapstonElectiveClassesList = ecCAPNewList;
            draftModel.ThesisElectiveClassesList = ecTHSNewList;

            if (draftModel.programCompletionOption.Equals("Thesis"))
            {
                draftModel.ElectiveClassesList = ecTHSNewList;
            }
            else {
                draftModel.ElectiveClassesList = ecCAPNewList;
            }
            switch (action)
            {
                case "save":
                    cpsmgr.insertUpdateNewDraftCPSToCPSDB(draftModel,true);
                    TempData["Message"] = "Draft CPS submitted Successfully, Start with another.";

                    return RedirectToAction("CreateDraftCPS", "FacultyAdvisor");

                case "saveDraft":
                    cpsmgr.insertUpdateNewDraftCPSToCPSDB(draftModel,false);
                    TempData["Message"] = "CPS Draft Saved Successfully";
                    return RedirectToAction("GenerateDraftCPS", "DraftCPS", new { id = Convert.ToInt32(draftModel.searchId) });

                case "back":
                    return RedirectToAction("Faculty", "Home", new { id = Convert.ToInt32(Session["UserID"]) });
            }

            return View();
        }

        public Course getCourseByWholeName(string wholename, List<Course> ECList)
        {
            foreach (Course c in ECList)
            {
                if (wholename.Equals(c.CourseWholeName))
                {
                    return c;
                }
            }
            return null;
        }

       // [AcceptVerbs(HttpVerbs.Get)]
        /*  public JsonResult LoadCourseBySubjectRubric(string subRub)
          {
              //Your Code For Getting Physicans Goes 
              CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
             // var list = mgr.getWholeNamebyUsingSubjectAndRubric(subRub);

              var data = list.Select(m => new SelectListItem()
              {
                  Text = m.CourseWholeName,
                  Value = m.CourseShortName,
              });
              return Json(data, JsonRequestBehavior.AllowGet);
          }*/

        /*   [HttpPost]
           public ActionResult GetLoadCourseBySubjectRubric(string subRub)
           {

               CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
               var list = mgr.getWholeNamebyUsingSubjectAndRubric(subRub);
               SelectList listWC = new SelectList(list, "CourseShortName", "CourseWholeName", 0);

               return Json(listWC);
           }*/

    }
}