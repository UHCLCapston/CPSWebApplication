using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.DB;

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
                    string admittedSem = student.FirstOrDefault().admittedSemester)
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

        public void updateStudentDetails()
        {

        }



    }
}