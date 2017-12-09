using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class AuditCPSController : Controller
    {
        // GET: AuditCPS
        [HttpGet]
        public ActionResult MakeAuditCPS(int id)
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
            if (mgr.AuditingNeeded(id.ToString().Trim()))
            {
                model = mgr.getAlreadyCreatedDraftCPSToShow(id.ToString());
                //new if else needed for new and prev audit
            }
            else if (mgr.AuditingOnSavedCPS(id.ToString().Trim())){

            }else if (mgr.AuditingOnModifiedCPS(id.ToString().Trim())){

            }
            else
            {
                TempData["Message"] = "Student Draft CPS is not yet ready to Audit";
                return RedirectToAction("AduitCPS", "AcademicAdvisor");
            }           
            TempData["Model"] = model;
            return View(model);
        }

        public ActionResult MakeAuditCPS()
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

        [HttpPost]
        public ActionResult SaveChangesOnAuditCPS(DesignCPSViewModel mdl, String action)
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
            draftModel.ClassesForCapstonSpecial = tempDataModel.ClassesForCapstonSpecial;
            draftModel.ClassesForThesisSpecial = tempDataModel.ClassesForThesisSpecial;

            List<Course> thesisShown = tempDataModel.ThesisCourse;
            List<Course> fcShown = tempDataModel.FoundationClassesList;
            List<Course> ccShown = tempDataModel.CoreClassesList;
            List<RubricClasses> ecCapShown = tempDataModel.ClassesForCapstonNormal;
            List<RubricClasses> ecThShown = tempDataModel.ClassesForThesisNormal;
            List<RubricClasses> ecCapSPLShown = tempDataModel.ClassesForCapstonSpecial;
            List<RubricClasses> ecThSPLShown = tempDataModel.ClassesForThesisSpecial;

            List<Course> ecAllList = tempDataModel.ElectiveClassesList;

            //List<String> ecShown = tempDataModel.CourseWholeNameListForElective;

            draftModel.programCompletionOption = mdl.programCompletionOption;
            draftModel.CapstonCourse.EnrolledSemester = mdl.CapstonCourse.EnrolledSemester;

            List<Course> fc = mdl.FoundationClassesList;
            List<Course> cc = mdl.CoreClassesList;
            List<Course> th = mdl.ThesisCourse;
            List<Course> ecTh = mdl.ThesisElectiveClassesList;
            List<Course> ecCap = mdl.CapstonElectiveClassesList;
            List<Course> ecThSpl = mdl.ThesisElectiveSpecialClassesList;
            List<Course> ecCapSpl = mdl.CapstonElectiveSpecialClassesList;
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

            List<Course> ecCAPNewSPLList = new List<Course>();
            List<Course> ecTHSNewSPLList = new List<Course>();

            CPSDraftToFinalManager cpsmgr = new CPSDraftToFinalManager();
            int num = 0;
            foreach (Course c in ecCap)
            {
                int index = th.IndexOf(c);
                string cname = c.CourseWholeName.Trim();
                if (!cname.Equals("---Select---"))
                {
                    Course ecNewCourse = cpsmgr.getCourseByWholeName(cname, ecAllList);
                    ecNewCourse.EnrolledSemester = c.EnrolledSemester;
                    ecNewCourse.GradesRecieved = c.GradesRecieved;
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
                    crs = cpsmgr.getCourseByWholeName(cname, ecAllList);
                    crs.EnrolledSemester = c.EnrolledSemester;
                    crs.GradesRecieved = c.GradesRecieved;
                    crs.CourseSubjectWithRubric = ecThShown[num].rubric;
                    ecTHSNewList.Add(crs);
                }
                num++;
            }
            num = 0;
            foreach (Course c in ecThSpl)
            {
                int index = th.IndexOf(c);
                string cname = c.CourseWholeName.Trim();
                Course crs = new Course();
                if (!cname.Equals("---Select---"))
                {
                    crs = cpsmgr.getCourseByWholeName(cname, ecAllList);
                    crs.EnrolledSemester = c.EnrolledSemester;
                    crs.GradesRecieved = c.GradesRecieved;
                    crs.CourseSubjectWithRubric = ecThSPLShown[num].rubric;
                    ecTHSNewSPLList.Add(crs);
                }
                num++;
            }

            num = 0;
            foreach (Course c in ecCapSpl)
            {
                int index = th.IndexOf(c);
                string cname = c.CourseWholeName.Trim();
                Course crs = new Course();
                if (!cname.Equals("---Select---"))
                {
                    crs = cpsmgr.getCourseByWholeName(cname, ecAllList);
                    crs.EnrolledSemester = c.EnrolledSemester;
                    crs.GradesRecieved = c.GradesRecieved;
                    crs.CourseSubjectWithRubric = ecCapSPLShown[num].rubric;
                    ecCAPNewSPLList.Add(crs);
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
                    cname.GradesRecieved = c.GradesRecieved;
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
                    fcNewCourse.GradesRecieved = c.GradesRecieved;
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
                ccNewCourse.GradesRecieved = c.GradesRecieved;
                ccNewList.Add(ccNewCourse);
                cnt++;
            }
            cnt = 0;
            draftModel.ThesisCourse = thNewList;
            draftModel.FoundationClassesList = fcNewList;
            draftModel.CoreClassesList = ccNewList;
            draftModel.CapstonElectiveClassesList = ecCAPNewList;
            draftModel.ThesisElectiveClassesList = ecTHSNewList;
            draftModel.CapstonElectiveSpecialClassesList = ecCAPNewSPLList;
            draftModel.ThesisElectiveSpecialClassesList = ecTHSNewSPLList;
            if (draftModel.programCompletionOption.Equals("Thesis"))
            {
                draftModel.ElectiveClassesList = ecTHSNewList;
                draftModel.SpecializationSelected = false;
            }
            else if (draftModel.programCompletionOption.Equals("Capstone"))
            {
                draftModel.ElectiveClassesList = ecCAPNewList;
                draftModel.SpecializationSelected = false;
            }
            else if (draftModel.programCompletionOption.Trim().Equals("Thesis With Specialization"))
            {
                draftModel.ElectiveClassesList = ecTHSNewSPLList;
                draftModel.programCompletionOption = "Thesis";
                draftModel.SpecializationSelected = true;
                draftModel.SpecializationType = mdl.SpecializationType;
            }
            else if (draftModel.programCompletionOption.Trim().Equals("Capstone With Specialization"))
            {
                draftModel.ElectiveClassesList = ecCAPNewSPLList;
                draftModel.programCompletionOption = "Capstone";
                draftModel.SpecializationSelected = true;
                draftModel.SpecializationType = mdl.SpecializationType;
            }

            switch (action)
            {
                case "sendToModify":
                    draftModel.SaveCPSAcademic = "Yes";
                    draftModel.FinalizeCPSAllow = "No";
                    draftModel.NeedModificationFromFaculty = "Yes";
                    draftModel.AllowAcademic = "Yes";
                    cpsmgr.insertUpdateNewDraftCPSToCPSDB(draftModel);
                    TempData["Message"] = "CPS is send to modification from faculty";
                    return RedirectToAction("MakeAuditCPS", "AuditCPS", new { id = Convert.ToInt32(draftModel.searchId) });

                case "saveToFinal":
                    draftModel.SaveCPSAcademic = "Yes";
                    draftModel.FinalizeCPSAllow = "Yes";
                    draftModel.NeedModificationFromFaculty = "No";
                    draftModel.AllowAcademic = "Yes";
                    draftModel.SignatureAcademicAdvisor = mdl.SignatureAcademicAdvisor;
                    cpsmgr.insertUpdateNewDraftCPSToCPSDB(draftModel);
                    TempData["Message"] = "CPS is finalized, start with another ";
                    return RedirectToAction("AuditCPS", "AcademicAdvisor");

                case "saveDraft":
                    draftModel.SaveCPSAcademic = "Yes";
                    draftModel.FinalizeCPSAllow = "No";
                    draftModel.NeedModificationFromFaculty = "No";
                    draftModel.AllowAcademic = "Yes";
                    cpsmgr.insertUpdateNewDraftCPSToCPSDB(draftModel);
                    TempData["Message"] = "CPS Saved Successfully";
                    return RedirectToAction("MakeAuditCPS", "AuditCPS", new { id = Convert.ToInt32(draftModel.searchId) });

                case "back":
                    return RedirectToAction("AcademicAdvisor", "Home", new { id = Convert.ToInt32(Session["UserID"]) });
            }
            return View();
        }

    }
}