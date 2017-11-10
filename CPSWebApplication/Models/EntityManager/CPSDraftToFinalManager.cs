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
          //  string cRegisered = "";
          //  string grade = "";                      /// need to be develop for exist student
       
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

                    var info = result.SingleOrDefault(b => b.StudentID == studentId); ;
                    if (info != null)
                    {
                        info.FirstName = cps.FirstName;
                        info.LastName = cps.LastName;
                        info.FoundationCourseDeatils = cps.AssignedFoundationCourseDetails;
                        info.AssignedFacultyAdvisor = cps.AssignedFacultyAdvioser;
                        info.FoundationCourseDeatils = cps.AssignedFoundationCourseDetails;
                        info.CoreCourseDetails = cps.CoreCourseDetails;
                        info.Academic_Year = cps.AcademicYear;
                        info.Major = cps.Major;
                        info.IsDraft = "No";
                        info.IsActive = "Yes";
                        info.IsAudited = "No";
                        info.IsBlankCreated = "Yes";
                        info.IsFinalised = "No";
                        info.BlankCreatedDate = dateTime;
                        db.SaveChanges();

                    }
                    else
                    {
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
                    }
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
            UserManager mgr = new UserManager();
            string  name =mgr.GetFacultyNameByID(id);
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {

                 var results = db.CPS.Where(p => p.AssignedFacultyAdvisor.Contains(name)).Select(p => new ViewModel.CPS
                 {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    StudentID = p.StudentID,
                    BlankCreatedDate = p.BlankCreatedDate,
                    LastDraftDate = p.LastDraftDate

                }).ToList();
                return results;
            }
        }



        public void getStudentCPS(String studentId)
        {

        }
        public DesignCPSViewModel getDraftCPSModelToShow(string id)
        {
            CPSDesignManager mgr = new CPSDesignManager();

            //completeion optionlist

            return null;
        }



    }//end of class

}//end of namespace