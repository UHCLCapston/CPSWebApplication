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
        public void insertNewBlanckCPSToCPSDB(string studentId, List<Course>clist, List<Course> eList, string year)
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

                    var info = result.SingleOrDefault(b => b.StudentID == studentId); 
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
        public ViewModel.CPS getStudentCPS(String studentId)
        {
              ViewModel.CPS  cps ;
            using(capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.CPS.SingleOrDefault(b => b.StudentID == studentId);
                if (result != null)
                {
                    cps= new ViewModel.CPS(result.StudentID, result.LastName, result.FirstName, result.Academic_Year, result.Major, result.FoundationCourseDeatils,result.CoreCourseDetails,result.ElectiveCourseDetails,result.AssignedFacultyAdvisor);
                    return cps;
                }
                return null;
            }
        }

        public List<String> getOptions(string option)
        {
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.CPSDesignOptions.SingleOrDefault(b => b.Option == option);
                if (result != null)
                {
                    string str = result.Data;
                    return str.Split(',').ToList<string>();
                }
            }
            return null;
        }
        public List<String> getSemesterOptions(string option, string ctlg)
        {
            string str = "";
            String jstr = "";
            List<string> semlist = new List<string>(); 
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.CPSDesignOptions.SingleOrDefault(b => b.Option == option);
                if (result != null)
                {
                     str = result.Data;      
                }
            }
            List<string> list=str.Split(',').ToList<string>();

            string s = ctlg.Replace("Catalog", "");
            List<string> arr = s.Split('_').ToList<string>();

            foreach(string i in list)
            {
                jstr = i + arr[0];
                semlist.Add(jstr);
                jstr = i + arr[1];
                semlist.Add(jstr);
                jstr = i + (Convert.ToInt32(arr[1]) + 1).ToString();
                semlist.Add(jstr);
            }

            return semlist;
        }

        public List<List<string>> getAllListWithOptionForSelection(string major, string ctlg)
        {
            List<List<string>> lists = new List<List<string>>();
            List<string> prgCompletionType = new List<string>();
            List<string> semOptions = new List<string>();
            List<string> credithrsOption = new List<string>();
            List<string> gradeOptions = new List<string>();
            List<string> levelOption = new List<string>();

            
            levelOption = getOptions("courselevels");
            semOptions = getSemesterOptions("semesters",ctlg);
            credithrsOption = getOptions("creditHrs");
            gradeOptions = getOptions("gradesOption");
            prgCompletionType = getOptions("programComptionType");


            lists.Add(levelOption);
            lists.Add(semOptions);
            lists.Add(credithrsOption);
            lists.Add(gradeOptions);
            lists.Add(prgCompletionType);
            return lists;

        }

        public Course getProgramCompletionCourse(string major, string completionType, string ctlg) {

            GenerateCPSManager mgr = new GenerateCPSManager();
            string shortName = "";
            Course crs = new Course();

            if (major.Equals("SWEN"))
            {
                using (capf17gswen41Entities db = new capf17gswen41Entities())
                {
                    var result = db.SWENCompletions.SingleOrDefault(b => b.CompletionType == completionType);
                    if (result != null)
                    {
                        shortName= result.CompletionCourse;
                    }

                   crs = mgr.getCourseDetailUsingCourseShortName(shortName, ctlg, major);
                }
            }
            return crs;
        } 

        public string getNumberOfElectivesAsPerCompletionType (string completionType, string major)
        {
            if (major.Equals("SWEN"))
            {
                using (capf17gswen41Entities db = new capf17gswen41Entities())
                {
                    var result = db.SWENCompletions.SingleOrDefault(b => b.CompletionType == completionType);
                    if (result != null)
                    {
                        return result.OtherElective;
                    }
                }
            }
            else if (major.Equals("CSCI"))
            {

            }
            else if (major.Equals("SENG"))
            {

            }
            return null;
        }


        public DesignCPSViewModel getBlanckCPSToViewFromCPS(string id)
        {
            DesignCPSViewModel mdl = new DesignCPSViewModel();

            CPSDesignManager dmgr = new CPSDesignManager();
            GenerateCPSManager gmgr = new GenerateCPSManager();
            ViewModel.CPS cps = getStudentCPS(id);

            string ctlg = cps.AcademicYear.Replace("Academic Year", "Catalog").Trim();
            string mjr = cps.Major;
            List<Course> lsFC = new List<Course>();
            List<Course> lsCC = new List<Course>();
            List<Course> lsEC = new List<Course>();

            lsFC = gmgr.getAssignedFoundationCourses(id, ctlg, mjr);
            lsCC = dmgr.getListCoreCourses(mjr, ctlg);
            lsEC = dmgr.getListElectiveCourses(mjr, ctlg);

            mdl.firstName = cps.FirstName;
            mdl.lastName = cps.LastName;
            mdl.searchId = cps.StudentID;
            mdl.majorName = cps.Major;
            mdl.academicYear = cps.AcademicYear;
            mdl.assignedFaculty = cps.AssignedFacultyAdvioser;
            mdl.FoundationClassesList = lsFC;
            mdl.CoreClassesList = lsCC;
            mdl.ElectiveClassesList = lsEC;

            mdl.firstName = cps.FirstName;
            mdl.lastName = cps.LastName;
            mdl.searchId = cps.StudentID;
            mdl.majorName = cps.Major;
            mdl.academicYear = cps.AcademicYear;
            mdl.assignedFaculty = cps.AssignedFacultyAdvioser;
            mdl.FoundationClassesList = lsFC;
            mdl.CoreClassesList = lsCC;
            mdl.ElectiveClassesList = lsEC;
            return mdl;

        }

            public DesignCPSViewModel getDraftCPSModelToShow(string id)
        {
            DesignCPSViewModel mdl = new DesignCPSViewModel();

            CPSDesignManager dmgr = new CPSDesignManager();
            GenerateCPSManager gmgr = new GenerateCPSManager();
            ViewModel.CPS cps= getStudentCPS(id);

            string ctlg = cps.AcademicYear.Replace("Academic Year", "Catalog").Trim();
            string mjr = cps.Major;
            List<Course> lsFC = new List<Course>();
            List<Course> lsCC = new List<Course>();
            List<Course> lsEC = new List<Course>();

            string subject="";
            string level="";

            lsFC= gmgr.getAssignedFoundationCourses(id, ctlg, mjr);
            lsCC = dmgr.getListCoreCourses(mjr, ctlg);
            lsEC = dmgr.getListElectiveCourses(mjr, ctlg);

            mdl.firstName = cps.FirstName;
            mdl.lastName = cps.LastName;
            mdl.searchId = cps.StudentID;
            mdl.majorName = cps.Major;
            mdl.academicYear = cps.AcademicYear;
            mdl.assignedFaculty = cps.AssignedFacultyAdvioser;
            mdl.FoundationClassesList = lsFC;
            mdl.CoreClassesList = lsCC;
            mdl.ElectiveClassesList = lsEC;

            List<List<string>> selectionLists =getAllListWithOptionForSelection(mjr,ctlg);
            mdl.CourseLevelSelectionListForElective = selectionLists[0];
            mdl.CourseSemesterList = selectionLists[1];
            mdl.CourseCreditHrsListForElective = selectionLists[2];
            mdl.CourseGradeList = selectionLists[3];
            mdl.ProgramCompletionOptionList = selectionLists[4];

            mdl.DepartmentListForElective= dmgr.getElectiveSubjectForMajor(mjr, ctlg);
            mdl.ElectiveClassesList = dmgr.getListElectiveCourses(mjr, ctlg);

            //mdl.DepartmentListForElective =dmgr.getElectiveWholeNameListWithDepartmentAndLevel(subject, level);


            return mdl;
        }

       
    }//end of class

}//end of namespace