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

        public bool AuditingOnModifiedCPS(string v)
        {
            return false;
        }

        public bool AuditingOnSavedCPS(string v)
        {
            return false;
        }

        public ViewModel.CPS getStudentDraftCPS(string studentId)
        {
            ViewModel.CPS cps;
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.DraftCPS.SingleOrDefault(b => b.StudentID == studentId);
                if (result != null)
                {
                    cps = new ViewModel.CPS(result.FirstName, result.LastName, result.StudentID, result.Academic_Year, result.Major, result.CoreCourseDetails, result.ElectiveCourseDetails, result.FoundationCourseDeatils, result.AssignedFacultyAdvisor,result.ProgramCompletionType,result.LastDraftDate);
                    return cps;
                }
                return null;
            }
        }

        public int getMinCountForCourseDetailList(List<string> list)
        {
            int count=0;
            int mincount = 0;
            List<int> counts = new List<int>();
            foreach (string str in list)
            {
                count = str.Split(',').ToList<string>().Count();
                counts.Add(count);
            }
            counts.Sort();
            mincount = counts[0];

            return mincount;
        }
        public List<Course> retriveCourseList(string listStr, String ctlg, String mjr)
        {
            List<Course> newListCourse = new List<Course>();
            CPSDesignManager mgr = new CPSDesignManager();
            GenerateCPSManager cmgr = new GenerateCPSManager();
            Course course = new Course();

            if (!listStr.Equals("") ) { 
            List<string> deatillist = listStr.Split(':').ToList<String>();
            List<string> courseDeatilList = new List<string>();

            if(deatillist.Count > 0) {
               int minCount = getMinCountForCourseDetailList(deatillist);
                    foreach (String str in deatillist)
                {
                    courseDeatilList = str.Split(',').ToList<String>();
                    if(courseDeatilList.Count > 0)
                    {
                        if(courseDeatilList.Count.Equals(4))
                        {
                            string csName = courseDeatilList[0];
                            string rubric = courseDeatilList[1];
                            string semester = courseDeatilList[2];
                            string grade = courseDeatilList[3];
                            course = cmgr.getCourseDetailUsingCourseShortName(csName, ctlg, mjr);
                            course.EnrolledSemester = semester;
                            course.CourseSubjectWithRubric = rubric;
                            course.GradesRecieved = grade;
                        }
                        else if(courseDeatilList.Count.Equals(3))
                        {
                            string csName = courseDeatilList[0];
                            string semester = courseDeatilList[1];
                            string grade = courseDeatilList[2];
                            course = cmgr.getCourseDetailUsingCourseShortName(csName, ctlg, mjr);
                            course.EnrolledSemester = semester;
                            course.GradesRecieved = grade;
                        }
                          
                    }else
                        {
                            if (courseDeatilList.Count.Equals(3))
                            {
                                string csName = courseDeatilList[0];
                                string rubric = courseDeatilList[1];
                                string semester = courseDeatilList[2];
                                course = cmgr.getCourseDetailUsingCourseShortName(csName, ctlg, mjr);
                                course.EnrolledSemester = semester;
                                course.CourseSubjectWithRubric = rubric;
                            }
                            else if (courseDeatilList.Count.Equals(2))
                            {
                                string csName = courseDeatilList[0];
                                string semester = courseDeatilList[1];
                                course = cmgr.getCourseDetailUsingCourseShortName(csName, ctlg, mjr);
                                course.EnrolledSemester = semester;
                            }
                        }
                    newListCourse.Add(course);
                }
            }
                
           }
            return newListCourse;
        }
        public  DesignCPSViewModel getModelForGenerateDraftCPS(string studentId)
        {
            DesignCPSViewModel mdl = new DesignCPSViewModel();

            CPSDesignManager dmgr = new CPSDesignManager();
            GenerateCPSManager gmgr = new GenerateCPSManager();
            ViewModel.CPS cps = getStudentDraftCPS(studentId);

            string ctlg = cps.AcademicYear.Replace("Academic Year", "Catalog").Trim();
            string mjr = cps.Major;
            List<Course> lsFC = new List<Course>();
            List<Course> lsCC = new List<Course>();
            List<Course> lsEC = new List<Course>();

            string lsFCstr = cps.AssignedFoundationCourseDetails;
            string lsCSstr = cps.CoreCourseDetails;
            string lsEEstr = cps.ElectiveCourseDetails;

            lsFC = retriveCourseList(lsFCstr,ctlg,mjr);
            lsCC = retriveCourseList(lsCSstr,ctlg,mjr);
            lsEC= retriveCourseList(lsEEstr,ctlg,mjr);

            mdl.firstName = cps.FirstName;
            mdl.lastName = cps.LastName;
            mdl.searchId = cps.StudentID;
            mdl.majorName = cps.Major;
            mdl.academicYear = cps.AcademicYear;
            mdl.assignedFaculty = cps.AssignedFacultyAdvioser;
            mdl.programCompletionOption = cps.ProgramCompletionType;

            mdl.FoundationClassesList = lsFC;
            mdl.CoreClassesList = lsCC;
            mdl.ElectiveClassesList = lsEC;

            return mdl;
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
        public void insertUpdateNewDraftCPSToCPSDB(DesignCPSViewModel draftModel)
        {
            string str = "";
            List<Course> fc = draftModel.FoundationClassesList;
            List<Course> cc = draftModel.CoreClassesList;
            List<Course> ec = draftModel.ElectiveClassesList;
            List<string> fcstr = new List<string>();
            List<string> ccstr = new List<string>();
            List<string> ecstr = new List<string>();

            string fcListStr = "";
            string ccListStr = "";
            string ecListStr = "";

            if(fc.Count> 0 || fc != null) { 
                foreach (Course c in fc)
                {
                    str = c.CourseShortName + "," + c.EnrolledSemester+","+c.GradesRecieved;
                    fcstr.Add(str);

                }
            fcListStr = String.Join(":", fcstr);
            }
            foreach (Course c in ec)
            {
                str= c.CourseShortName + "," + c.CourseSubjectWithRubric + "," + c.EnrolledSemester+","+c.GradesRecieved;
                ecstr.Add(str);
            }
         
            foreach(Course c in cc)
            {
                str = c.CourseShortName + "," + c.EnrolledSemester+","+c.GradesRecieved;
                ccstr.Add(str);
            }

            ccListStr = String.Join(":", ccstr);
            ecListStr = String.Join(":", ecstr);

            var time = DateTime.Now;
            string dateTime = time.ToString("yyyy, MM, dd, hh, mm, ss");
            string isSpecial = "";
            string specialization = "";
            if (draftModel.SpecializationSelected)
            {
                isSpecial = "Yes";
                specialization = draftModel.SpecializationType;
            }
            else
            {
                isSpecial = "No";
            }
            string aaSignature= "";
            string finalDate="";
            if (draftModel.SignatureAcademicAdvisor != null)
            {
                aaSignature= draftModel.SignatureAcademicAdvisor;
                finalDate = dateTime;
            }
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {              
                    var info = db.DraftCPS.SingleOrDefault(b => b.StudentID == draftModel.searchId);
                    if (info != null)
                    {
                        info.StudentID = draftModel.searchId;
                        info.FirstName = draftModel.firstName;
                        info.LastName = draftModel.lastName;
                        info.AssignedFacultyAdvisor = draftModel.assignedFaculty;
                        info.Academic_Year = draftModel.academicYear;
                        info.Major = draftModel.majorName;
                        info.ProgramCompletionType = draftModel.programCompletionOption;
                        
                        info.FoundationCourseDeatils = fcListStr;
                        info.ElectiveCourseDetails = ecListStr;
                        info.CoreCourseDetails = ccListStr;

                        info.NeedAudit = draftModel.AllowAcademic;
                        info.IsAudited = draftModel.SaveCPSAcademic;
                        info.NeedModificationForFinal = draftModel.NeedModificationFromFaculty;
                        info.IsFinalised = draftModel.FinalizeCPSAllow;

                        info.IsDraft = "Yes";
                        info.IsActive = "Yes";
                        info.IsBlankCreated = "Yes";
                        info.LastDraftDate = dateTime;
                        if (draftModel.SpecializationSelected) {
                            info.IsSpecialization = "Yes";
                            info.SpecializationUnder = draftModel.SpecializationType;
                        }
                        else
                        {
                            info.IsSpecialization = "No";
                        }

                    if (draftModel.SignatureAcademicAdvisor != null)
                    {
                        info.AcademicAdvisorSignature = aaSignature;
                        info.LastFinalizeDate = dateTime;
                    }

                        db.SaveChanges();

                    }
                    else
                    {
                        DB.DraftCPS cps = new DB.DraftCPS();
                        cps.StudentID = draftModel.searchId;
                        cps.FirstName = draftModel.firstName;
                        cps.LastName = draftModel.lastName;
                        cps.AssignedFacultyAdvisor = draftModel.assignedFaculty;
                        cps.Academic_Year = draftModel.academicYear;
                        cps.Major = draftModel.majorName;
                        cps.ProgramCompletionType = draftModel.programCompletionOption;

                        cps.FoundationCourseDeatils = fcListStr;
                        cps.ElectiveCourseDetails = ecListStr;
                        cps.CoreCourseDetails = ccListStr;

                        cps.NeedAudit = draftModel.AllowAcademic;
                        cps.IsAudited = draftModel.SaveCPSAcademic;
                        cps.NeedModificationForFinal = draftModel.NeedModificationFromFaculty;
                        cps.IsFinalised = draftModel.FinalizeCPSAllow;

                        cps.IsDraft = "Yes";
                        cps.IsActive = "Yes";                       
                        cps.IsBlankCreated = "Yes";
                        cps.LastDraftDate = dateTime;
                        cps.IsSpecialization = isSpecial;
                        cps.SpecializationUnder = specialization;
                        cps.AcademicAdvisorSignature = aaSignature;
                        cps.LastFinalizeDate = finalDate;
                    
                        db.DraftCPS.Add(cps);
                    }
                    db.SaveChanges();
                }

            
        }


        public List<StudentDetails> getListOfAllStudentToDesignCPS()
        {
            UserManager mgr = new UserManager();
          using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.StudentDetails.Select(p => new ViewModel.StudentDetails
                {
                    FirstName = p.firstName,
                    LastName = p.lastName,
                    StudentID = p.studentID,
                }).ToList();
                return results;
            }

        }

        public List<StudentDetails> getListOfAllNewStudent()
        {
            UserManager mgr = new UserManager();
            var list1 = getListOfAllStudentToDesignCPS();
            List<ViewModel.CPS> list2 = getListOfAllBlankCPSGenerated();
            var newlist2 = new List<StudentDetails>();

            List<StudentDetails> newStudentList = new List<StudentDetails>();
            List<int> temp = new List<int>();
                       
                foreach(ViewModel.CPS cs in list2)
                {
                  StudentDetails st = new StudentDetails();                 
                  st.FirstName = cs.FirstName;
                  st.LastName = cs.LastName;
                  st.StudentID = cs.StudentID;
                newlist2.Add(st);                   
                }

            newStudentList = (from lst1 in list1
                              where !newlist2.Any(x => x.StudentID == lst1.StudentID && x.FirstName == lst1.FirstName)
                              select lst1).ToList();
            
            return newStudentList;
        }

        public List<ViewModel.CPS> getListOfAllBlankCPSGenerated()
        {
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var results = db.CPS.Select(p => new ViewModel.CPS
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

        public List<ViewModel.CPS> getListOfAllDraftCPSGeneratedForAudit()
        {
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var results = db.DraftCPS.Where(p=>p.NeedAudit.Equals("Yes")).Select(p => new ViewModel.CPS
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

        public List<ViewModel.CPS> getListOfAllFinalCPSGeneratedToView()
        {
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var results = db.DraftCPS.Where(p => p.IsFinalised.Equals("Yes")).Select(p => new ViewModel.CPS
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
                    cps= new ViewModel.CPS(result.FirstName, result.LastName, result.StudentID, result.Academic_Year, result.Major, result.CoreCourseDetails,result.ElectiveCourseDetails,result.FoundationCourseDeatils,result.AssignedFacultyAdvisor);
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
                semlist.Add(jstr.Trim());
                jstr = i + arr[1];
                semlist.Add(jstr.Trim());
                jstr = i + (Convert.ToInt32(arr[1]) + 1).ToString();
                semlist.Add(jstr.Trim());
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
            List<string> levelGroupOption = new List<string>();
     
            levelOption = getOptions("courselevels");
            semOptions = getSemesterOptions("semesters",ctlg);
            credithrsOption = getOptions("creditHrs");
            gradeOptions = getOptions("gradesOption");
            prgCompletionType = getOptions("programComptionType");
            prgCompletionType.Add("Capstone With Specialization");
            prgCompletionType.Add("Thesis With Specialization");

            levelGroupOption = getOptions("courseLevelGroup");

            lists.Add(levelOption);
            lists.Add(semOptions);
            lists.Add(credithrsOption);
            lists.Add(gradeOptions);
            lists.Add(prgCompletionType);
            lists.Add(levelGroupOption);
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

            mdl.firstName = cps.FirstName;
            mdl.lastName = cps.LastName;
            mdl.searchId = cps.StudentID;
            mdl.majorName = cps.Major;
            mdl.academicYear = cps.AcademicYear;
            mdl.assignedFaculty = cps.AssignedFacultyAdvioser;
            mdl.FoundationClassesList = lsFC;
            mdl.CoreClassesList = lsCC;
         
            return mdl;

        }

        public bool DraftCPSExists(string id)
        {
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.DraftCPS.SingleOrDefault(b => b.StudentID == id);
                if (result != null)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            
        }

        public bool AuditingNeeded(string id)
        {
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.DraftCPS.SingleOrDefault(b => b.StudentID == id);
                if (result != null)
                {
                    string str = result.NeedAudit;
                    if ( str != null && str.Equals("Yes"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                }
                else
                {
                    return false;
                }
            }
        }

        public bool doesBlankMatchDraft(string id)
        {
            ViewModel.CPS draft = new ViewModel.CPS();
            ViewModel.CPS blanckChange = new ViewModel.CPS();
            CPSDesignManager dmgr = new CPSDesignManager();
            GenerateCPSManager gmgr = new GenerateCPSManager();

            string changeDetailFacultyAdvisor = "";
            string changeDetailAssignedFoundationCourse = "";
            string draftDetailFacultyAdvisor = "";
            string draftDetailAssignedFoundationCourse = "";
            if (dmgr.alreadyDesignedCPS(id))
            {
                using (capf17gswen4Entities db = new capf17gswen4Entities())
                {
                    var changeDetails = db.CPS.SingleOrDefault(b => b.StudentID == id);
                   
                    if (changeDetails != null)
                    {
                        changeDetailFacultyAdvisor = changeDetails.AssignedFacultyAdvisor;
                        changeDetailAssignedFoundationCourse = changeDetails.FoundationCourseDeatils;
                    }
                    var result = db.DraftCPS.SingleOrDefault(b => b.StudentID == id);
                    if (result != null)
                    {
                        draftDetailFacultyAdvisor = result.AssignedFacultyAdvisor;
                        draftDetailAssignedFoundationCourse = result.FoundationCourseDeatils;
                    }
                    else
                    {
                        draft = null;
                    }
                }
            }

            List<string> list1=changeDetailAssignedFoundationCourse.Split(',').ToList<string>();
            List<string> list2 = draftDetailAssignedFoundationCourse.Split(':').ToList<string>();
            int count = 0;

            if (list1.Count.Equals(1) && list2.Count.Equals(1))
            {
                if (list1[0].Equals(list2[0]) && changeDetailFacultyAdvisor.Equals(draftDetailFacultyAdvisor))
                {
                    return true;
                }
            }
            if (list1.Count.Equals(list2.Count)) { 
                foreach(string str in list1)
                {
                    int i = list1.IndexOf(str);
                    if ((!list1[i].Equals("")) && str.Contains(list1[i])) {
                        count++;
                    }
                }
            }

            if (changeDetailFacultyAdvisor.Equals(draftDetailFacultyAdvisor) && count.Equals(list1.Count()))
            {
                return true;
            }
            return false;
        }

        public DesignCPSViewModel getAlreadyCreatedDraftCPSToShow(string id)
        {
            const string CAPSTONE = "Capston";
            ViewModel.CPS draft = new ViewModel.CPS();
            ViewModel.CPS blanckChange = new ViewModel.CPS();
            DesignCPSViewModel model = new DesignCPSViewModel();
            CPSDesignManager dmgr = new CPSDesignManager();
            GenerateCPSManager gmgr = new GenerateCPSManager();
            

            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var result = db.DraftCPS.SingleOrDefault(b => b.StudentID == id);
                if (result != null)
                {
                    //draft = new ViewModel.CPS(result.FirstName,result.LastName, result.StudentID, result.Academic_Year, result.Major, result.CoreCourseDetails, result.ElectiveCourseDetails, result.ProgramCompletionType,result.FoundationCourseDeatils, result.AssignedFacultyAdvisor,result.LastFinalizeDate,result.LastDraftDate,result.IsSpecialization,result.SpecializationUnder,result.AcademicAdvisorSignature);
                    draft.FirstName = result.FirstName;
                    draft.LastName = result.LastName;
                    draft.StudentID = result.StudentID;
                    draft.AcademicYear = result.Academic_Year;
                    draft.AssignedFacultyAdvioser = result.AssignedFacultyAdvisor;
                    draft.Major = result.Major;
                    draft.ProgramCompletionType = result.ProgramCompletionType;
                    draft.IsSpecialize = result.IsSpecialization;
                    draft.SpecializaionUnder = result.SpecializationUnder;
                    draft.AssignedFoundationCourseDetails = result.FoundationCourseDeatils;
                    draft.CoreCourseDetails = result.CoreCourseDetails;
                    draft.ElectiveCourseDetails = result.ElectiveCourseDetails;
                    draft.LastFinalizeDate = result.LastFinalizeDate;
                    draft.IsFinalised = result.IsFinalised;
                    draft.AcademicAdvisorSignature = result.AcademicAdvisorSignature;

                }
                else
                {
                    draft = null;
                }
            }
            

            model.searchId = draft.StudentID;
            model.firstName = draft.FirstName;
            model.lastName = draft.LastName;
            model.academicYear = draft.AcademicYear;
            model.majorName = draft.Major;
            model.programCompletionOption = draft.ProgramCompletionType;
            model.assignedFaculty = draft.AssignedFacultyAdvioser;
            model.FinalizeCPSAllow = draft.IsFinalised;           
            model.SignatureAcademicAdvisor = draft.AcademicAdvisorSignature;
           // model.SignedDate = draft.LastFinalizeDate;

            string ctlg = draft.AcademicYear.Replace("Academic Year", "Catalog").Trim();
            string mjr = draft.Major.Trim();
            string fcListStr = draft.AssignedFoundationCourseDetails;
            string ccListStr = draft.CoreCourseDetails;
            string ecListStr = draft.ElectiveCourseDetails;

            List<Course> fcList = retriveCourseList(fcListStr, ctlg, mjr);
            List<Course> ccList = retriveCourseList(ccListStr, ctlg, mjr);
            List<Course> ecList = retriveCourseList(ecListStr, ctlg, mjr);

            model.FoundationClassesList = fcList;
            model.CoreClassesList = ccList;
            model.ElectiveClassesList = dmgr.getCourseElectiveWholeNameListWithDepartment(mjr, ctlg);

            if (model.programCompletionOption.Equals("Thesis"))
            {
                if(draft.IsSpecialize !=null && draft.IsSpecialize.Equals("Yes"))
                {
                    model.ThesisElectiveSpecialClassesList = ecList;
                    model.SpecializationType = draft.SpecializaionUnder;
                    model.programCompletionOption = "Thesis With Specialization";
                }
                else
                {
                    model.ThesisElectiveClassesList = ecList;
                }
            }
            else if (model.programCompletionOption.Equals("Capstone"))
            {
               if (draft.IsSpecialize != null && draft.IsSpecialize.Equals("Yes"))
                {
                    model.CapstonElectiveSpecialClassesList = ecList;
                    model.SpecializationType = draft.SpecializaionUnder;
                    model.programCompletionOption = "Capstone With Specialization";
                }
                else
                {
                    model.CapstonElectiveClassesList = ecList;
                }
            }

            model.ElectiveCreditHrs = "3";
            List<List<string>> selectionLists = getAllListWithOptionForSelection(mjr, ctlg);
            model.CourseSemesterList = selectionLists[1];
            model.CourseSemesterList.Insert(0, "-select-");
            model.CourseGradeList = selectionLists[3];
            model.ProgramCompletionOptionList = selectionLists[4];
            model.countElectivesCapston = Convert.ToInt32(getNumberOfElectivesAsPerCompletionType(CAPSTONE, mjr));
            model.countElectivesThesis = Convert.ToInt32(getNumberOfElectivesAsPerCompletionType("Thesis", mjr));

            model.ThesisCourse = getThesisCourse(mjr, ctlg);
            model.CapstonCourse = getCapstonCourse(mjr, ctlg);

            model.ClassesForCapstonNormal = getClassesForCapstonNormal(mjr, ctlg, model.countElectivesCapston);
            model.ClassesForThesisNormal = getClassesForThesisNormal(mjr, ctlg, model.countElectivesThesis);

            model.ListSpecializationOption = getListOfSpecialization(mjr);
            model.ListSpecializationOption.Insert(0, "---Select---");
            model.ClassesForThesisSpecial = getClassesForThesisSpecial(mjr, ctlg, model.countElectivesThesis);
            model.ClassesForCapstonSpecial = getClassesForCapstonSpecial(mjr, ctlg, model.countElectivesCapston);
            var time = DateTime.Now;
            string dateTime = time.ToString("yyyy, MM, dd, hh, mm, ss");

            
            model.SignedDate = DateTime.Now;

            return model;
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

            List<List<string>> selectionLists =getAllListWithOptionForSelection(mjr,ctlg);
            mdl.CourseLevelSelectionListForElective = selectionLists[0];
            mdl.CourseSemesterList = selectionLists[1];
            mdl.CourseSemesterList.Insert(0, "-select-");
            mdl.CourseCreditHrsListForElective = selectionLists[2];
            mdl.CourseGradeList = selectionLists[3];
            mdl.ProgramCompletionOptionList = selectionLists[4];

            List<string> levelGroup = selectionLists[5];
            List<string> departments = dmgr.getElectiveSubjectForMajor(mjr, ctlg);
            mdl.LevelGroupOption = levelGroup;
           // string completionType = "Capston";
          //  mdl.CourseSubjectLevelRubricSelectionOption = getCourseRubricCreatedOptions(mjr ,completionType);
            //mdl.CourseSubjectLevelRubricSelectionOption.Insert(0, "---Select---");

            mdl.DepartmentListForElective= dmgr.getElectiveSubjectForMajor(mjr, ctlg);
            mdl.countElectives = 7;
            mdl.countElectivesCapston = Convert.ToInt32(getNumberOfElectivesAsPerCompletionType("Capston", mjr));
            mdl.countElectivesThesis = Convert.ToInt32(getNumberOfElectivesAsPerCompletionType("Thesis", mjr)); 
            mdl.ElectiveCreditHrs = "3";
            mdl.ElectiveClassesList = dmgr.getCourseElectiveWholeNameListWithDepartment(mjr,ctlg);
            mdl.CourseWholeNameListForElective = dmgr.getElectiveWholeNameListWithDepartment(mjr,ctlg);
            mdl.CourseWholeNameListForElective.Insert(0, "---Select---");

            mdl.ThesisCourse = getThesisCourse(mjr, ctlg);
            mdl.CapstonCourse = getCapstonCourse(mjr, ctlg);
            mdl.ListSpecializationOption = getListOfSpecialization(mjr);
            mdl.ListSpecializationOption.Insert(0, "---Select---");


            mdl.ClassesForCapstonNormal = getClassesForCapstonNormal(mjr, ctlg, mdl.countElectivesCapston); 
            mdl.ClassesForThesisNormal = getClassesForThesisNormal(mjr, ctlg, mdl.countElectivesThesis);

            mdl.ClassesForThesisSpecial = getClassesForThesisSpecial(mjr, ctlg, mdl.countElectivesThesis);
            mdl.ClassesForCapstonSpecial = getClassesForCapstonSpecial(mjr, ctlg, mdl.countElectivesCapston);


            return mdl; 
        }

        private List<string> getListOfSpecialization(string mjr)
        {
            if (mjr.Equals("SWEN"))
            {
                using (capf17gswen41Entities db = new capf17gswen41Entities())
                {
                    var results = db.SWENCompletions.Select(c => c.SpecializationListOptions).Distinct().ToList<string>();
                   return  results[0].Split(',').ToList<string>();
                }
            }
            return null;
        }

        private List<RubricClasses> getClassesForThesisSpecial(string mjr, string ctlg, int count)
        {
            List<RubricClasses> list = new List<RubricClasses>();
            List<string> rubricList = getCourseRubricCreatedOptions(mjr, "Thesis","Special");
            foreach (string r in rubricList)
            {
                List<string> wcList = new List<string>();
                List<Course> clist = getCoursesbyUsingSubjectAndRubric(r.Trim());
                foreach (Course c in clist)
                {
                    string name = c.CourseWholeName;
                    wcList.Add(name);
                }
                wcList.Insert(0, "---Select---");
                RubricClasses rbc = new RubricClasses(r, wcList);
                list.Add(rbc);
            }

            return list;
        }

        private List<RubricClasses> getClassesForThesisNormal(string mjr, string ctlg, int count)
        {
            List<RubricClasses> list = new List<RubricClasses>();
            List<string> rubricList = getCourseRubricCreatedOptions(mjr, "Thesis","Normal");

            foreach (string r in rubricList)
            {
                List<string> wcList = new List<string>();
                List<Course> clist = getCoursesbyUsingSubjectAndRubric(r.Trim());
                foreach(Course c in clist)
                {
                    string name =c.CourseWholeName;
                    wcList.Add(name);
                }
                wcList.Insert(0, "---Select---");
                RubricClasses rbc = new RubricClasses(r, wcList);
              list.Add(rbc);
            }

            return list;
        }

        private List<RubricClasses> getClassesForCapstonSpecial(string mjr, string ctlg,int count)
        {
            List<RubricClasses> list = new List<RubricClasses>();
            List<string> rubricList = getCourseRubricCreatedOptions(mjr, "Capston ", "Special");

            foreach (string r in rubricList)
            {
                List<string> wcList = new List<string>();
                List<Course> clist = getCoursesbyUsingSubjectAndRubric(r.Trim());
                foreach (Course c in clist)
                {
                    string name = c.CourseWholeName;
                    wcList.Add(name);
                }
                wcList.Insert(0, "---Select---");
                RubricClasses rbc = new RubricClasses(r, wcList);
                list.Add(rbc);
            }

            return list;
        }

        private List<RubricClasses> getClassesForCapstonNormal(string mjr, string ctlg,int count)
        {
            List<RubricClasses> list = new List<RubricClasses>();
            List<string> rubricList = getCourseRubricCreatedOptions(mjr, "Capston", "Normal");

            foreach (string r in rubricList)
            {
                List<string> wcList = new List<string>();

                List<Course> clist = getCoursesbyUsingSubjectAndRubric(r.Trim());
                foreach (Course c in clist)
                {
                    string name = c.CourseWholeName;
                    wcList.Add(name);
                }
                wcList.Insert(0, "---Select---");
                RubricClasses rbc = new RubricClasses(r, wcList);
                list.Add(rbc);
            }

            return list;
        }
        
        public List<Course> getCoursesbyUsingSubjectAndRubric(string subRub)
        {
            List<Course> list = new List<Course>();
            if (subRub.Equals("SENG/CSCI/SWEN 5x3x-6x3x"))
            {
                using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
                {
                    var results = db.AcademicCatalog16_17.Where(p => (p.Dept.Equals("SENG") || p.Dept.Equals("CSCI") || p.Dept.Equals("SWEN")) && (p.SWEN.Equals("E"))&& (p.Course_No.StartsWith("5") || p.Course_No.StartsWith("6"))).Select(p => new Course{
                       CourseShortName = p.Course,
                       CourseFullName = p.Long_Title 
                    }).ToList<Course>();
                    if (results != null)
                    {
                        foreach (Course c in results)
                        {
                            list.Add(new Course(c.CourseShortName,c.CourseShortName + " " + c.CourseFullName));
                        }
                    }
                    return list;

                }
            }
            else if(subRub.Equals("SENG/CSCI/SWEN 4x3x-6x3x"))
            {
                using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
                {
                    var results = db.AcademicCatalog16_17.Where(p => (p.Dept.Equals("SENG") || p.Dept.Equals("CSCI") || p.Dept.Equals("SWEN")) && (p.SWEN.Equals("E")) && (p.Course_No.StartsWith("4") ||p.Course_No.StartsWith("5")|| p.Course_No.StartsWith("6"))).Select(p => new Course
                    {
                        CourseShortName = p.Course,
                        CourseFullName = p.Long_Title
                    }).ToList<Course>();
                    if (results != null)
                    {
                        foreach (Course c in results)
                        {
                            list.Add(new Course(c.CourseShortName, c.CourseShortName + " " + c.CourseFullName));
                        }
                    }
                    return list;

                }
            }
            else if (subRub.Equals("SWEN Technical Elective"))
            {
                using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
                {
                    var results = db.AcademicCatalog16_17.Where(p =>( p.Dept.Equals("SWEN")) && (p.SWEN.Equals("E")) && (p.Course_No.StartsWith("5") || p.Course_No.StartsWith("6"))).Select(p => new Course
                    {
                        CourseShortName = p.Course,
                        CourseFullName = p.Long_Title
                    }).ToList<Course>();
                    if (results != null)
                    {
                        foreach (Course c in results)
                        {
                            list.Add(new Course(c.CourseShortName, c.CourseShortName + " " + c.CourseFullName));
                        }
                    }
                    return list;

                }
            }
            else if (subRub.Equals("SWEN/DMST/SENG 5x3x-6x3x"))
            {
                using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
                {
                    var results = db.AcademicCatalog16_17.Where(p => (p.Dept.Equals("SENG") || p.Dept.Equals("DMST") || p.Dept.Equals("SWEN")) && (p.SWEN.Equals("E")) && (p.Course_No.StartsWith("5") || p.Course_No.StartsWith("6"))).Select(p => new Course
                    {
                        CourseShortName = p.Course,
                        CourseFullName = p.Long_Title
                    }).ToList<Course>();
                    if (results != null)
                    {
                        foreach (Course c in results)
                        {
                            list.Add(new Course(c.CourseShortName, c.CourseShortName + " " + c.CourseFullName));
                        }
                    }
                    return list;

                }
            }

            return list;
        }

        private List<string> getCourseRubricCreatedOptions(string mjr, string type, string spcl)
        {
            string str = "";
            if (mjr.Equals("SWEN"))
            {
                if (spcl.Equals("Special"))
               { 
                    using (capf17gswen41Entities db = new capf17gswen41Entities())
                    {
                        var result = db.SWENCompletions.SingleOrDefault(b => b.CompletionType == type);
                        if (result != null)
                        {
                            str= result.ElectiveSubjectWithLevelSpecialOptions;
                        }

                        return str.Split(',').ToList<string>();
                    }
               }
                else
                {
                    using (capf17gswen41Entities db = new capf17gswen41Entities())
                    {
                        var result = db.SWENCompletions.SingleOrDefault(b => b.CompletionType == type);
                        if (result != null)
                        {
                            str = result.ElectiveSubjectWithLevelOptions;
                        }

                        return str.Split(',').ToList<string>();
                    }
                }

            }

            return null;
        }

        private List<Course> getThesisCourse(string mjr, string ctlg)
        {
            string courseShtName = "";
            GenerateCPSManager cmgr = new GenerateCPSManager();
            List<Course> list = new List<Course>(); 
            using (capf17gswen41Entities db = new capf17gswen41Entities())
            {
                var result = db.SWENCompletions.SingleOrDefault(b => b.CompletionType == "Thesis");
                if (result != null)
                {
                    courseShtName = result.CompletionCourse;
                }
              list.Add( cmgr.getCourseDetailUsingCourseShortName(courseShtName, ctlg, mjr));
              list.Add(cmgr.getCourseDetailUsingCourseShortName(courseShtName, ctlg, mjr));
            }
            return list;
        }

        private Course getCapstonCourse(string mjr, string ctlg)
        {
            string courseShtName = "";
            GenerateCPSManager cmgr = new GenerateCPSManager();
            using (capf17gswen41Entities db = new capf17gswen41Entities())
            {
                var result = db.SWENCompletions.SingleOrDefault(b => b.CompletionType == "Capston");
                if (result != null)
                {
                    courseShtName = result.CompletionCourse;
                }
              return  cmgr.getCourseDetailUsingCourseShortName(courseShtName, ctlg, mjr);
            }
           
        }
    }//end of class

}//end of namespace