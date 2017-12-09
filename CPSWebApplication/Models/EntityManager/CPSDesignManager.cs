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
        public  bool alreadyDesignedCPS(string studentId)
        {
            using (capf17gswen4Entities db = new capf17gswen4Entities())
            {
                var cps = db.CPS.Where(o => o.StudentID.ToLower().Equals(studentId));
                if (cps.Any())
                {
                    if (cps.FirstOrDefault().IsBlankCreated.Equals("Yes"))
                    {
                        return true;
                    }
                    else { return false; }

                }

            }
            return false;
        }

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

     
        public List<string> getElectiveSubjectForMajor(string major, string catalog)
        {
            List<string> list = new List<string>();

            if (catalog.Equals("Catalog16_17"))
            {
                if (major.Equals("SWEN"))
                {
                    list = getElectiveSubjectsForSWEN();
                }
                else if (major.Equals("CSCI"))
                {
                    list = getElectiveSubjectsForCSCI();

                }
                else if (major.Equals("SENG"))
                {
                    list = getElectiveSubjectsForSENG();
                }
                return list;
            }
            return null;
        }

        private List<string> getElectiveSubjectsForSENG()
        {
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(j => j.SENG == "E").Select(c => c.Dept).Distinct().ToList();
                return results;
            }

        }

        private List<string> getElectiveSubjectsForCSCI()
        {
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(j => j.CSCI == "E").Select(c => c.Dept).Distinct().ToList();
                return results;
            }
        }

        private List<string> getElectiveSubjectsForSWEN()
        {
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(j => j.SWEN == "E").Select(c => c.Dept).Distinct().ToList();
                return results;
            }
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
                    list = getAllFoundationDetailsSENG();

                }
            }
            else if (catalog.Equals("Catalog17_18"))
            {

            }

            return list;
        }

        private List<Course> getAllFoundationDetailsSENG()
        {
            throw new NotImplementedException();
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
                    list = getAllCoreDetaisForCSCI();

                }
                else if (major.Equals("SENG"))
                {
                    list = getAllCoreDetaisForSENG();
                }


            }
            else if (catalog.Equals("Catalog17_18"))
            {

            }

            return list;
        }

        private List<Course> getAllCoreDetaisForSENG()
        {
            throw new NotImplementedException();
        }

        private List<Course> getAllCoreDetaisForCSCI()
        {
            throw new NotImplementedException();
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
                    list = getAllElectiveDetailsForCSCI();

                }
                else if (major.Equals("SENG"))
                {
                    list = getAllElectiveDetailsForSENG();

                }


            }
            else if (catalog.Equals("Catalog17_18"))
            {

            }

            return list;
        }

        private List<Course> getAllElectiveDetailsForSENG()
        {
            throw new NotImplementedException();
        }

        private List<Course> getAllElectiveDetailsForCSCI()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllFoundationSWEN()
        {
           /* using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("F")).Select(p => new
                {
                    p.Course
                    
                }).Cast<string>().ToList(); ;

                return results;
            }*/

            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.SWEN.Contains("F")).Select(p => new
                {
                    p.Course

                }).Cast<string>().ToList(); 

                return results;
            }

        }

        public List<Course> getAllFoundationDetailsSWEN()
        {

          /*  using (CPSCreationEntities db = new CPSCreationEntities())
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
            }*/


            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.SWEN.Contains("F")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.Long_Title,
                    CourseSubject = p.Dept,
                    Courselevel = p.Course_No,
                    CreditHrs = p.Credit_Hr

                }).ToList(); ;

                return results;
            }


        }

        public List<Course> getAllFoundationDetailsCSCI()
        {

          /*  using (CPSCreationEntities db = new CPSCreationEntities())
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
            }*/

            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.CSCI.Contains("F")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.Long_Title,
                    CourseSubject = p.Dept,
                    Courselevel = p.Course_No,
                    CreditHrs = p.Credit_Hr

                }).ToList(); ;

                return results;
            }


        }

        public  List<Course> getElectiveListWithDepartmentAndLevel(string subject, string level)
        {

            string firstNumber = level.Substring(level.Length - 3);
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.Dept.Contains(subject) && p.Course_No.StartsWith(firstNumber) ).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.Long_Title,
                    CourseSubject = p.Dept,
                    Courselevel = p.Course_No,
                    CreditHrs = p.Credit_Hr

                }).ToList();

                return results;
            }

        }
        public List<string> getElectiveWholeNameListWithDepartmentAndLevel(string subject, string level)
        {
            string str = "";
            List<String> list = new List<string>();
            string firstNumber = level.Substring(level.Length - 3);
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.Dept.Contains(subject) && p.Course_No.StartsWith(firstNumber)).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.Long_Title,
                    CourseSubject = p.Dept,
                    Courselevel = p.Course_No,
                    CreditHrs = p.Credit_Hr

                }).ToList();

                foreach(Course c in results)
                {
                    list.Add(c.CourseShortName + " " + c.CourseFullName);
                }
                return list;
            }

        }



        public List<string> getAllFoundationCSCI()
        {
            return null;
        }

        public List<String> getAllCoreForSWEN()
        {
            /* using (CPSCreationEntities db = new CPSCreationEntities())
             {
                 var results = db.Catalog16_17.Where(p => p.SWEN.Contains("C")).Select(p => new
                 {
                     p.Course
                 }).Cast<string>().ToList(); ;

                 return results;
             }*/
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.SWEN.Contains("C")).Select(p => new
                {
                    p.Course
                }).Cast<string>().ToList(); ;

                return results;
            }

        }

        public List<string> getElectiveWholeNameListWithDepartment(string mjr,string ctlg)
        {
            List<String> list = new List<string>();
            List<Course> ec = getListElectiveCourses(mjr, ctlg);
            foreach (Course c in ec) {
                list.Add(c.CourseShortName + " " + c.CourseFullName);
            }
            return list;
        }

        public List<Course> getCourseElectiveWholeNameListWithDepartment(string mjr, string ctlg)
        {
            List<Course> ec = getListElectiveCourses(mjr, ctlg);
            foreach (Course c in ec)
            {
                c.CourseWholeName = (c.CourseShortName + " " + c.CourseFullName);
            }
            return ec;
        }


        public List<Course> getAllCoreDetaisForSWEN()
        {
            /* using (CPSCreationEntities db = new CPSCreationEntities())
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
             }*/
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.SWEN.Contains("C")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.Long_Title,
                    CourseSubject = p.Dept,
                    Courselevel = p.Course_No,
                    CreditHrs = p.Credit_Hr

                }).ToList();

                return results;
            }



        }

        public List<String> getAllElectiveForSWEN()
        {
          /*  using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var results = db.Catalog16_17.Where(p => p.SWEN.Contains("E")).Select(p => new
                {
                    p.Course
                }).Cast<string>().ToList(); ;

                return results;
            }*/

            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.SWEN.Contains("E")).Select(p => new
                {
                    p.Course
                }).Cast<string>().ToList(); ;

                return results;
            }

        }

        public List<Course> getAllElectiveDetailsForSWEN()
        {
           /* using (CPSCreationEntities db = new CPSCreationEntities())
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
            */
            using (c533317sp04prakhyanEntities db = new c533317sp04prakhyanEntities())
            {
                var results = db.AcademicCatalog16_17.Where(p => p.SWEN.Contains("E")).Select(p => new Course
                {
                    CourseShortName = p.Course,
                    CourseFullName = p.Long_Title,
                    CourseSubject = p.Dept,
                    Courselevel = p.Course_No,
                    CreditHrs = p.Credit_Hr

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
                Boolean flag = false;
                var result = db.FacultyDetails.SingleOrDefault(b => b.First_Name == firstNameFaculty);
                if (result != null)
                {
                    var check = result.AdvisingStudentList;
                    if(check != null)
                    {
                       flag= true;
                    }

                    if (flag)
                    {
                        if (result.AdvisingStudentList.Contains(studentListStr))
                        {
                            result.AdvisingStudentList = result.AdvisingStudentList;
                        }
                        else
                        {
                            result.AdvisingStudentList += studentListStr;
                        }
                    }
                    else
                    {
                        result.AdvisingStudentList = studentListStr;
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
        public string getStudentFirstName(string id)
        {
            using (CPSCreationEntities db = new CPSCreationEntities())
            {
                var student = db.StudentDetails.Where(o => o.studentID.ToLower().Equals(id));
                if (student.Any())
                {
                    return student.FirstOrDefault().firstName;
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
            v.firstName = mg.getStudentFirstName(id.ToString());


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