using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class FacultyAdvisorController:Controller
    {

        public ActionResult ViewBlanckCPS()
        {
            string id;
            string userId = "";
            if (Session["UserID"] != null)
            {
                id = Session["UserName"].ToString();
                userId = Session["UserID"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel mdl = new DesignCPSViewModel();

            List<CPS> listStudentCPSWork = mgr.getListBlackCPSUnderFacultyAdvioser(userId);
            mdl.cpsList = listStudentCPSWork;
            TempData["StudentList"] = listStudentCPSWork;

            return View(mdl);
        }

        [HttpPost]
        public ActionResult ViewBlanckCPS(DesignCPSViewModel mdl)
        {
            string studentId = mdl.searchId;

            return RedirectToAction("GenerateBlanckCPSView", "FacultyAdvisor", new { id = Convert.ToInt32(studentId) });
        }

        public ActionResult CreateDraftCPS()
        {
            string id;
            string userId = "";
            if (Session["UserID"] != null)
            {
                id = Session["UserName"].ToString();
                userId = Session["UserID"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel mdl = new DesignCPSViewModel();

            List<CPS> listStudentCPSWork = mgr.getListBlackCPSUnderFacultyAdvioser(userId);
            mdl.cpsList = listStudentCPSWork;
            TempData["StudentList"] = listStudentCPSWork;

            return View(mdl);
        }

        [HttpPost]
        public ActionResult CreateDraftCPS(DesignCPSViewModel mdl)
        {
            Boolean flag = false;
            string studentId = mdl.searchId;
            List<CPS> studentlist = (List<CPS>)TempData["StudentList"];
            foreach(CPS c in studentlist)
            {
                if (studentId.Equals(c.StudentID)) {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return RedirectToAction("GenerateDraftCPS", "DraftCPS", new { id = Convert.ToInt32(studentId) });
            }
            TempData["Message"] = "Student is not in your list";
            return RedirectToAction("CreateDraftCPS", "FacultyAdvisor");
        }

        [HttpGet]
        public ActionResult GenerateBlanckCPSView(int id)
        {
            bool flag = false;
            string studentId = id.ToString();

            // GenerateCPSManager gm = new GenerateCPSManager();
            //DesignCPSViewModel vm = gm.getModelForGenerateCPS(studentId);
            CPSDraftToFinalManager mgr = new CPSDraftToFinalManager();
            DesignCPSViewModel vm = mgr.getBlanckCPSToViewFromCPS(studentId);

            if (Session["UserID"] != null)
            {
                flag = true;
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }

            return View(vm);
        }

        public ActionResult ViewFinalCPS()
        {
            string id;
            if (Session["UserID"] != null)
            {
                id = Session["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");

            }
            return View();
        }

    }
}