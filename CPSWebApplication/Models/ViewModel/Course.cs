using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPSWebApplication.Models.ViewModel
{
    public class Course
    {

        public string CourseShortName { get; set; }
        public string CourseFullName { get; set; }

        public string CourseSubject { get; set; }
        public string Courselevel { get; set; }

        public string CreditHrs { get; set; }

        public Course()
        {

        }

        public Course(string courseShortName, string courseFullName, string courseSubject, string courselevel, string creditHrs)
        {
            CourseShortName = courseShortName;
            CourseFullName = courseFullName;
            CourseSubject = courseSubject;
            Courselevel = courselevel;
            CreditHrs = creditHrs;
        }
    }
}