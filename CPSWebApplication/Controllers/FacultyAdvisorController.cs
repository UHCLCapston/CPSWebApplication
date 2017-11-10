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
            if (Session["UserID"] != null)
            {
                id = Session["UserName"].ToString();
            }

            return View();
        }

        public ActionResult CreateDraftCPS()
        {
            string id;
            string userId = Session["UserID"].ToString();
            if (Session["UserID"] != null)
            {
                id = Session["UserName"].ToString();
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


        public ActionResult ViewFinalCPS()
        {
            string id;
            if (Session["UserID"] != null)
            {
                id = Session["UserName"].ToString();
            }
            return View();
        }

    }
}