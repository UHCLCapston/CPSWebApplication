using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.DB;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Models.EntityManager
{
    public class CPSDraftToFinalManager
    {
        public void insertNewBlanckCPSToCPSDB(string studentId, List<Course>clist,  string year)
        {
            string cName = "";
       
            List<String> str = new List<string>();
                foreach (Course c in clist)
               {
                cName = c.CourseShortName;
                str.Add(cName);
               }
            string cListStr = String.Join(",",str);
            ViewModel.CPS cps = getBlanckCPSData(studentId, cListStr, year);
            var time = DateTime.Now;
            string dateTime = time.ToString("yyyy, MM, dd, hh, mm, ss");

            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.CPS;
                if (result != null)
                {

                   /* if ((result.FirstOrDefault().StudentID).Equals(cps.StudentID))
                    {

                    }
                    else
                    { }*/
                        DB.CPS cpsDb = new DB.CPS();
                        cpsDb.FirstName = cps.FirstName;
                        cpsDb.LastName = cps.LastName;
                        cpsDb.StudentID = cps.StudentID;
                        cpsDb.Academic_Year = cps.AcademicYear;
                        cpsDb.Major = cps.Major;
                        cpsDb.IsDraft = "No";
                        cpsDb.IsActive = "Yes";
                        cpsDb.IsAudited = "No";
                        cpsDb.IsBlankCreated = "Yes";
                        cpsDb.IsFinalised = "No";
                        cpsDb.AssignedFacultyAdvisor = cps.AssignedFacultyAdvioser;
                        cpsDb.FoundationCourseDeatils = cps.AssignedFoundationCourseDetails;
                        cpsDb.CoreCourseDetails = cps.CoreCourseDetails;
                        cpsDb.BlankCreatedDate = dateTime;
                        db.CPS.Add(cpsDb);
                    
                    db.SaveChanges();
                }

            }



        }
        public ViewModel.CPS getBlanckCPSData(string studentId, string cListstr, string year )
        {
        
            CPSDesignManager cp = new CPSDesignManager();

            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var info = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                 if (info.Any())
                {
                    ViewModel.CPS cps = new ViewModel.CPS(info.FirstOrDefault().firstName, info.FirstOrDefault().lastName, info.FirstOrDefault().studentID, year, info.FirstOrDefault().majorName, cListstr, info.FirstOrDefault().AssignedFoundation,info.FirstOrDefault().AssignedFacultyAdvisor );
                    return cps;
                }

            }

                return null;
        }

        public List<ViewModel.CPS> getListBlackCPSUnderFacultyAdvioser(string id)
        {
            String strStudentIdlist="";
            List<String> studentIds = new List<String>();
            List<ViewModel.CPS> list = new List<ViewModel.CPS>();

            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var info = db.FacultyDetails.Where(o => o.FacultyId.ToLower().Equals(id));
                if (info.Any())
                {
                    strStudentIdlist = info.FirstOrDefault().AdvisingStudentList;
                }

            }

            studentIds = strStudentIdlist.Split(',').ToList<String>();
            foreach(string i in studentIds)
            {
                using (capf17gswen4Entities db = new capf17gswen4Entities())
                {
                    var info = db.CPS.Where(o => o.StudentID.ToLower().Equals(i));
                    if (info.Any())
                    {
                        string fName = info.FirstOrDefault().FirstName;
                        string lName = info.FirstOrDefault().LastName;
                        string stId = info.FirstOrDefault().StudentID;
                        string dateBCreated = info.FirstOrDefault().BlankCreatedDate;
                        string dateDraftLast = info.FirstOrDefault().LastDraftDate;

                        ViewModel.CPS cps = new ViewModel.CPS(fName, lName, stId, dateBCreated, dateDraftLast);
                        list.Add(cps);
                    }
                }
            }

            return list;
        }



    }
}