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

        public List<String> getListOffAllDepartment()
        {
            return null;
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


    }
}