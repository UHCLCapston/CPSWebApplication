using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.DB;
using System.Text.RegularExpressions;

namespace CPSWebApplication.Models.EntityManager
{
    public class CPSDesignManager
    {
        public bool isTheStudentNew(string studentId)
        {
            bool flag = false;
            
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                if (student.Any())
                {
                    if( student.FirstOrDefault().currentSemester == student.FirstOrDefault().admittedSemester)
                    {
                        flag = true;
                    }
                    else { flag = false; }

                }

            }

            return flag;
        }//end of istheStudentNew

        public bool doesStudentExist(string studentId) {
            bool flag = false;
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                if (student.Any())
                {
                    if (student.FirstOrDefault().studentID == studentId)
                    {
                        flag = true;
                    }
                    else { flag = false; }

                }

            }
            return flag;
        }
        public string catalogNeedsTofollow(string studentId) {
            string strCatalog = "";
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                if (student.Any())
                {
                    string admittedSem = student.FirstOrDefault().admittedSemester;
                   if (admittedSem.Equals("Fall16") || admittedSem.Equals("Spring17") || admittedSem.Equals("Summer17"))
                    {
                        strCatalog = "Catalog16_17";
                    }
                    else if (admittedSem.Equals("Fall17") || admittedSem.Equals("Spring18") || admittedSem.Equals("Summer18"))
                    {
                        strCatalog = "Catalog17_18";
                    }


                }
            }

            return strCatalog;
        }

        public string getStudentMajor(string studentId)
        {

            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(studentId));
                if (student.Any())
                {
                    return student.FirstOrDefault().majorName;
                }
                else
                    return String.Empty;

            }

        }

        public List<Course> getListFoundation(string major, string catalog)
        {
            List<Course> list = new List<Course>();

            if (catalog.Equals("Catalog16_17"))
            {
                if (major.Equals("SWEN"))
                {
                    //getAllFoundationSWEN();
                    list = getAllFoundationDetailsSWEN();
                }
                else if (major.Equals("CSCI"))
                {
                    list = getAllFoundationDetailsCSCI();
                }
                else if (major.Equals("SENG")) {

                } 


            }
            else if (catalog.Equals("Catalog17_18"))
            {

            }

            return list;
        }
        public List<Course> getListCoreCourses(string major, string catalog)
        {
            List<Course> list = new List<Course>();

            if (catalog.Equals("Catalog16_17"))
            {
                if (major.Equals("SWEN"))
                {
                    list = getAllCoreDetaisForSWEN();
                }
                else if (major.Equals("CSCI"))
                {
                   
                }
                else if (major.Equals("SENG"))
                {

                }


            }
            else if (catalog.Equals("Catalog17_18"))
            {

            }

            return list;
        }

        public List<Course> getListElectiveCourses(string major, string catalog)
        {
            List<Course> list = new List<Course>();

            if (catalog.Equals("Catalog16_17"))
            {
                if (major.Equals("SWEN"))
                {
                    list = getAllElectiveDetailsForSWEN();
                }
                else if (major.Equals("CSCI"))
                {

                }
                else if (major.Equals("SENG"))
                {

                }


            }
            else if (catalog.Equals("Catalog17_18"))
            {

            }

            return list;
        }
        
        public List<string> getAllFoundationSWEN()
        {
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("F")).Select(p => new
                {
                    p.Course
                    
                }).Cast<string>().ToList(); ;

                return results;
            }
        }

        public List<Course> getAllFoundationDetailsSWEN()
        {

            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("F")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.LongTitle,
                    CourseSubject = p.Subject,
                    Courselevel = p.Catalog,
                    CreditHrs = p.creditHrs

                }).ToList(); ;

                return results;
            }
        }

        public List<Course> getAllFoundationDetailsCSCI()
        {

            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.CSCI.Contains("F")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.LongTitle,
                    CourseSubject = p.Subject,
                    Courselevel = p.Catalog,
                    CreditHrs = p.creditHrs

                }).ToList(); ;

                return results;
            }
        }

        public List<string> getAllFoundationCSCI()
        {
            return null;
        }

        public List<String> getAllCoreForSWEN()
        {
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("C")).Select(p => new
                {
                    p.Course
                }).Cast<string>().ToList(); ;

                return results;
            }
        }

        public List<Course> getAllCoreDetaisForSWEN()
        {
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("C")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.LongTitle,
                    CourseSubject = p.Subject,
                    Courselevel = p.Catalog,
                    CreditHrs = p.creditHrs

                }).ToList(); 

                return results;
            }
        }

        public List<String> getAllElectiveForSWEN()
        {
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("E")).Select(p => new
                {
                    p.Course
                }).Cast<string>().ToList(); ;

                return results;
            }
        }

        public List<Course> getAllElectiveDetailsForSWEN()
        {
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("E")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.LongTitle,
                    CourseSubject = p.Subject,
                    Courselevel = p.Catalog,
                    CreditHrs = p.creditHrs

                }).ToList();

                return results;
            }
        }

        public void updateStudentDetails(string studentId, List<Course>assignedFoundationClasses, string facultyAd )
        {
            string cShtName;
            List<string> list = new List<string>();
            string foundationsString = "";
            if (assignedFoundationClasses != null)
            {
                foreach (Course c in assignedFoundationClasses)
                {
                    cShtName = c.CourseShortName;
                    list.Add(cShtName);
                }

                foundationsString = string.Join(",", list.ToArray());
            }
            
                using (CPSCreationEntities db = new CPSCreationEntities())
                {
                    var result = db.StudentDetails.SingleOrDefault(b => b.studentID == studentId);
                    if (result != null)
                    {
                        result.AssignedFoundation = foundationsString;
                        result.AssignedFacultyAdvisor = facultyAd;
                        db.SaveChanges();
                    }
                }
            
        }//end of updateStudentDetails


        public void updateFacultyDetails(string studentId, string facultyName)
        {
            string firstNameFaculty = "";
            string[] words = facultyName.Split(' ');
            firstNameFaculty = words[0];
            string studentListStr = studentId + ",";

            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var result = db.FacultyDetails.SingleOrDefault(b => b.First_Name == firstNameFaculty);
                if (result != null)
                {
                    if (result.AdvisingStudentList.Contains(studentListStr)) {
                        result.AdvisingStudentList = result.AdvisingStudentList;
                    }
                    else
                    {
                        result.AdvisingStudentList += studentListStr;
                    }
                    db.SaveChanges();
                }
            }
        }

        public string getStudentLastName(string id)
        {
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(id));
                if (student.Any())
                {
                   return  student.FirstOrDefault().lastName;
                }

            }

            return "";
        }


        public List<string> getAllFacultyAdvisorForDepartment (string major)
        {
            List<String> list = new List<string>();
            String str = "";

            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.FacultyDetails.Where(p => p.AdvisorDepartment.Contains(major)).Select(p => new FacultyDetails
                {
                    FirstName = p.First_Name,
                    LastName = p.Last_Name,
                    FacultyId = p.FacultyId,
                }).ToList();

                foreach(FacultyDetails f in results)
                {
                    str = f.FirstName + " " + f.LastName;
                    list.Add(str);
                }
                return list;
            }

        }

        public DesignCPSViewModel getModelForDesignCPSToView(int id)
        {
            CPSDesignManager mg = new CPSDesignManager();

            string mjr = mg.getStudentMajor(id.ToString());
            string ctlg = mg.catalogNeedsTofollow(id.ToString());

            string lastName = mg.getStudentLastName(id.ToString());

            DesignCPSViewModel v = new DesignCPSViewModel();
            v.searchId = id.ToString();
            v.lastName = lastName;
            v.majorName = mjr;

            List<Course> fclist = mg.getListFoundation(mjr, ctlg);
            v.FoundationClassesList = fclist;
            v.CoreClassesList = mg.getListCoreCourses(mjr, ctlg);
            ctlg = Regex.Replace(ctlg, "^Catalog", "Academic Year");
            v.academicYear = ctlg;
            v.DfacultiesList = mg.getAllFacultyAdvisorForDepartment(mjr);

            return v;
        }

    }
}