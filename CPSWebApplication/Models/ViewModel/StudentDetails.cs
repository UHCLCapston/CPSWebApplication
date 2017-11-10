using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CPSWebApplication.Models.ViewModel
{
    public class StudentDetails
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentID { get; set; }
        public string UHCLEmail { get; set; }
        public string AdmittedSemester { get; set; }
        public string currentSemester { get; set; }
        public string MajorName { get; set; }
        public string CGPA { get; set; }

        public string enrolledCoursesAndSemester { get; set; }
        public string completedCoursesAndGrades { get; set; }
        public string programCompletionType { get; set; }
        public string assignedFoundation { get; set; }


        public StudentDetails(){}
        public StudentDetails(string studentID, string assignedFoundation)
        {
            StudentID = studentID;
            this.assignedFoundation = assignedFoundation;
        }
    }
}