using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.DB;
using System.Text.RegularExpressions;



namespace CPSWebApplication.Models.EntityManager
{
    public class GenerateCPSManager
    {

        public List<Course> getAssignedFoundationCourses (string studentId, string ctlg, string mjr)
        {
           var result = "";
            List<Course> list = new List<Course>();
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                if (student.Any())
                {
                    result = student.FirstOrDefault().AssignedFoundation;
                }
               
            }

            if(! result.Equals("None") && !result.Equals("")) { 
            List<string> str = result.Split(',').ToList();
            foreach(string c in str)
            {
                string cShrtName = c;
                Course crs = getCourseDetailUsingCourseShortName(c, ctlg, mjr);
                list.Add(crs);
            }
            }
            return list;
        }

        public List<List<Course>> getRegisteredCoursesDetails (string studentId, string ctlg, string mjr)
        {
            List<List<Course>> list = new List<List<Course>>();

            List<Course> registeredCoreCourses = new List<Course>();
            List<Course> registeredElectiveCourses = new List<Course>();
            List<Course> registeredFoundationCourses = new List<Course>();
            var result = "";
            
           
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                if (student.Any())
                {
                    result = student.FirstOrDefault().enrolledCoursesAndSemesters;
                }

            }

            if(result != null) { 
            List<string> str = result.Split('|').ToList();
            List<string> spStr = new List<string>();

            foreach(string c in str)
            {
                spStr= c.Split(':').ToList();

                string csSname= spStr[0];
                string csSem = spStr[1];

                Course crs = getCourseDetailUsingCourseShortName(csSname,  ctlg, mjr);
                string type = crs.CourseType;

                if (crs.CourseType.Equals("C"))
                {
                    Course coreCs = new Course(csSname, crs.CourseFullName, crs.CourseSubject, crs.Courselevel, crs.CreditHrs, crs.CourseType, csSem);
                    registeredCoreCourses.Add(coreCs);
                }
                else if (crs.CourseType.Equals("F"))
                {
                    Course coreCs = new Course(csSname, crs.CourseFullName, crs.CourseSubject, crs.Courselevel, crs.CreditHrs, crs.CourseType, csSem);
                    registeredFoundationCourses.Add(coreCs);
                }
                else if (crs.CourseType.Equals("E"))
                {
                    Course coreCs = new Course(csSname, crs.CourseFullName, crs.CourseSubject, crs.Courselevel, crs.CreditHrs, crs.CourseType, csSem);
                    registeredElectiveCourses.Add(coreCs);
                }

            }
            list.Add(registeredFoundationCourses);
            list.Add(registeredCoreCourses);
            list.Add(registeredElectiveCourses);
            }
            return list;

        }//end of getRegisteredCourseDetails

        public Course getCourseDetailUsingCourseShortName(string csname, string ctlg, string mjr)
        {
            Course crs= new Course();
            if (ctlg.Equals("Catalog16_17")) {
                if (mjr.Equals("SWEN"))
                {
                    using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
                    {
                        var course = db.AcademicCatalog16_17.Where(o => o.Course.ToLower().Equals(csname));
                        if (course.Any())
                        {
                            string csShortName = course.FirstOrDefault().Course;
                            string csLongName = course.FirstOrDefault().Long_Title;
                            string csCredits = course.FirstOrDefault().Credit_Hr;
                            string csType = course.FirstOrDefault().SWEN;
                            string csSubject = course.FirstOrDefault().Dept;
                            string csCatalog = course.FirstOrDefault().Course_No;

                            crs = new Course(csShortName, csLongName, csSubject, csCatalog, csCredits, csType);
                            crs.CourseWholeName = csShortName + " " + csLongName;
                        }
                    }

                  
                    return crs;
                }
                else if(mjr.Equals("CSCI"))
                {
                    using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
                    {
                        var course = db.AcademicCatalog16_17.Where(o => o.Course.ToLower().Equals(csname));
                        if (course.Any())
                        {
                            string csShortName = course.FirstOrDefault().Course;
                            string csLongName = course.FirstOrDefault().Long_Title;
                            string csCredits = course.FirstOrDefault().Credit_Hr;
                            string csType = course.FirstOrDefault().CSCI;
                            string csSubject = course.FirstOrDefault().Dept;
                            string csCatalog = course.FirstOrDefault().Course_No;

                            crs = new Course(csShortName, csLongName, csSubject, csCatalog, csCredits, csType);
                        }
                    }
                    return crs;
                }
                else if (mjr.Equals("SENG"))
                {
                    using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
                    {
                        var course = db.AcademicCatalog16_17.Where(o => o.Course.ToLower().Equals(csname));
                        if (course.Any())
                        {
                            string csShortName = course.FirstOrDefault().Course;
                            string csLongName = course.FirstOrDefault().Long_Title;
                            string csCredits = course.FirstOrDefault().Credit_Hr;
                            string csType = course.FirstOrDefault().SENG;
                            string csSubject = course.FirstOrDefault().Dept;
                            string csCatalog = course.FirstOrDefault().Course_No;

                            crs = new Course(csShortName, csLongName, csSubject, csCatalog, csCredits, csType);
                        }
                    }
                    return crs;
                }

            }
            else if (ctlg.Equals("Catalog17_18"))
            {

            }
     
            return crs;
        }//getCourseDetailsUsingCourseShortName

        public List<List<Course>> getAllCompletedCourseDetails(string studentId, string ctlg, string mjr) {
            List<List<Course>> list = new List<List<Course>>();

            List<Course> completedCoreCourses = new List<Course>();
            List<Course> completedElectiveCourses = new List<Course>();
            List<Course> completedFoundationCourses = new List<Course>();
            var result = "";
          
            
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                if (student.Any())
                {
                    result = student.FirstOrDefault().completedCoursesAndGrades;
                }
            }

            if (result != null) { 
            List<string> str = result.Split('|').ToList();
            List<string> spStr = new List<string>();

            foreach (string c in str)
            {
                spStr = c.Split(',').ToList();

                string csSname = spStr[0];
                string csGrade = spStr[1];
                string csSem = spStr[2];

                Course crs = getCourseDetailUsingCourseShortName(csSname, ctlg, mjr);
                string type = crs.CourseType;

                if (crs.CourseType.Equals("C"))
                {
                    Course coreCs = new Course(csSname, crs.CourseFullName, crs.CourseSubject, crs.Courselevel, crs.CreditHrs, crs.CourseType, csSem,csGrade);
                    completedCoreCourses.Add(coreCs);
                }
                else if (crs.CourseType.Equals("F"))
                {
                    Course coreCs = new Course(csSname, crs.CourseFullName, crs.CourseSubject, crs.Courselevel, crs.CreditHrs, crs.CourseType, csSem,csGrade);
                    completedFoundationCourses.Add(coreCs);
                }
                else if (crs.CourseType.Equals("E"))
                {
                    Course coreCs = new Course(csSname, crs.CourseFullName, crs.CourseSubject, crs.Courselevel, crs.CreditHrs, crs.CourseType, csSem,csGrade);
                    completedElectiveCourses.Add(coreCs);
                }
            }

            list.Add(completedFoundationCourses);
            list.Add(completedCoreCourses);
            list.Add(completedElectiveCourses);
            }
            return list;

        }//get all completedCourseDetails

        public List<Course> getUpdatedListOfFoundationCourses(List<List<Course>> regCoursesList, List<List<Course>> completedCoursesList, List<Course> assignedFoundationCourses)
        {
            List<Course> registeredList = new List<Course>();
            List<Course> completedList = new List<Course>();
            bool flagR = false;
            bool flagC = false;
            if (regCoursesList.Count > 0)
            {
                registeredList = regCoursesList[0];
                flagR = true;
            }
            if(completedCoursesList.Count > 0) { 
               completedList = completedCoursesList[0];
                flagC = true;
            }
            List<Course> list = new List<Course>();

            if (flagR && flagC)
            {
                list = registeredList.Union(completedList).ToList<Course>();
                var dict = assignedFoundationCourses.ToDictionary(p => p.CourseShortName);
                foreach (var crs in list)
                {
                    dict[crs.CourseShortName] = crs;
                }
                list = dict.Values.ToList();
               
            }
            else if (flagR && !flagC)
            {
                list = registeredList;
                var dict = assignedFoundationCourses.ToDictionary(p => p.CourseShortName);
                foreach (var crs in list)
                {
                    dict[crs.CourseShortName] = crs;
                }
                list = dict.Values.ToList();
            }
            else if (!flagR && flagC)
            {
                list = completedList;
                var dict = assignedFoundationCourses.ToDictionary(p => p.CourseShortName);
                foreach (var crs in list)
                {
                    dict[crs.CourseShortName] = crs;
                }
                list = dict.Values.ToList();
            }
            else {
                list = assignedFoundationCourses;
            }
            
            return list;
        }//getUpdatedListOfFoundationCourses

        public List<Course> getUpdatedListOfCoreCourses(List<List<Course>> regCoursesList, List<List<Course>> completedCoursesList, List<Course> assignedCoreCourses)
        {
            List<Course> registeredList = new List<Course>();
            List<Course> completedList = new List<Course>();
            bool flagR = false;
            bool flagC = false;
            if (regCoursesList.Count > 0)
            {
                registeredList = regCoursesList[1];
                flagR = true;
            }
            if (completedCoursesList.Count > 0)
            {
                completedList = completedCoursesList[1];
                flagC = true;
            }
            List<Course> list = new List<Course>();

            if (flagR && flagC)
            {
                list = registeredList.Union(completedList).ToList<Course>();
                
                var dict = assignedCoreCourses.ToDictionary(p => p.CourseShortName);
                foreach (var crs in list)
                {
                    dict[crs.CourseShortName] = crs;
                }
                list = dict.Values.ToList();
            }
            else if (flagR && !flagC)
            {
                list = registeredList;
                list =list.Union(assignedCoreCourses).ToList<Course>();
                var dict = assignedCoreCourses.ToDictionary(p => p.CourseShortName);
                foreach (var crs in list)
                {
                    dict[crs.CourseShortName] = crs;
                }
                list = dict.Values.ToList();
            }
            else if (!flagR && flagC)
            {
                list = completedList;
                list =list.Union(assignedCoreCourses).ToList<Course>();
                var dict = assignedCoreCourses.ToDictionary(p => p.CourseShortName);
                foreach (var crs in list)
                {
                    dict[crs.CourseShortName] = crs;
                }
               list = dict.Values.ToList();

            }
            else
            {
                list = assignedCoreCourses;
            }



            return list;
        }//getUpdatedListOfCoreCourses
        public List<Course> getUpdatedListOfElectiveCourses(List<List<Course>> regCoursesList, List<List<Course>> completedCoursesList)
        {
            List<Course> registeredList = new List<Course>();
            List<Course> completedList = new List<Course>();
            bool flagR = false;
            bool flagC = false;
            if (regCoursesList.Count >0)
            {
                registeredList = regCoursesList[2];
                flagR = true;
            }
            if (completedCoursesList.Count >0)
            {
                completedList = completedCoursesList[2];
                flagC = true;
            }
            List<Course> list = new List<Course>();

            if (flagR && flagC)
            {
                list = registeredList.Union(completedList).ToList<Course>();
            }
            else if (flagR && !flagC)
            {
                list = registeredList;
            }
            else if (!flagR && flagC)
            {
                list = completedList;
            }

            return list;
        }//getUpdatedListOfElectiveCourses


        public DesignCPSViewModel getModelForGenerateCPS(string studentId)
        {
            DesignCPSViewModel vmodel = new DesignCPSViewModel();
            CPSDesignManager mg = new CPSDesignManager();
            string mj = mg.getStudentMajor(studentId);
            string ct = mg.catalogNeedsTofollow(studentId);

            vmodel.searchId = studentId;
            vmodel.lastName = mg.getStudentLastName(studentId);
            vmodel.firstName = mg.getStudentFirstName(studentId);
            vmodel.majorName = mj;

            List<Course> assignedFC = getAssignedFoundationCourses(studentId,ct,mj);
            List<Course> coreCS = mg.getListCoreCourses(mj, ct);
            List<List<Course>> regAllCS = getRegisteredCoursesDetails(studentId,ct,mj);
            List<List<Course>> cmptdAllCS = getAllCompletedCourseDetails(studentId,ct,mj);

            List<Course> fcListView = getUpdatedListOfFoundationCourses(regAllCS, cmptdAllCS, assignedFC);
            List<Course> ccListView = getUpdatedListOfCoreCourses(regAllCS, cmptdAllCS, coreCS);
            List<Course> ecListView = getUpdatedListOfElectiveCourses(regAllCS, cmptdAllCS);

            vmodel.CoreClassesList = ccListView;
            vmodel.ElectiveClassesList = ecListView;
            vmodel.FoundationClassesList = fcListView;

            ct = Regex.Replace(ct, "^Catalog", "Academic Year");
            vmodel.academicYear = ct;

            return vmodel;
        }

    }// end of class generateCPS
   public  class CourseComparer : IEqualityComparer<Course>
    {
        public bool Equals(Course p1, Course p2)
        {
            return p1.CourseShortName == p2.CourseShortName;
        }

        public int GetHashCode(Course obj)
        {
            throw new NotImplementedException();
        }
    }//end of class courseComparar

}