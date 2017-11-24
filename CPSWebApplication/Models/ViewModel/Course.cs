using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPSWebApplication.Models.ViewModel
{
    public class Course : IEquatable<Course>
    {
        public string CourseShortName { get; set; }
        public string CourseFullName { get; set; }
        public string CourseSubject { get; set; }
        public string Courselevel { get; set; }
        public string CreditHrs { get; set; }
        public string CourseType { get; set; }
        public string EnrolledSemester { get; set; }
        public bool IsSelected { get; set; }
        public string GradesRecieved { get; set; }
        public bool Is4xxxLevelCourse { get; set; }
        public bool Is5xxxLevelCourse { get; set; }
        public bool Is6xxxLevelCourse { get; set; }
        public string CourseWholeName { get; set; }
        public string CourseLevelXXXType { get; set; }

        public string CourseSubjectWithRubric { get; set; }
        public Course()
        {}
        public Course(string courseShortName, string courseWholeName)
        {
            CourseShortName = courseShortName;
            CourseWholeName = courseWholeName;

        }
        public Course(string courseShortName, string courseFullName, string courseSubject, string courselevel, string creditHrs)
        {
            CourseShortName = courseShortName;
            CourseFullName = courseFullName;
            CourseSubject = courseSubject;
            Courselevel = courselevel;
            CreditHrs = creditHrs;
        }

        public Course(string courseShortName, string courseFullName, string courseSubject, string courselevel, string creditHrs, string courseType) : this(courseShortName, courseFullName, courseSubject, courselevel, creditHrs)
        {
            CourseType = courseType;
        }

        public Course(string courseShortName, string courseFullName, string courseSubject, string courselevel, string creditHrs, string courseType, string enrolledSemester) : this(courseShortName, courseFullName, courseSubject, courselevel, creditHrs)
        {
            CourseType = courseType;
            EnrolledSemester = enrolledSemester;
        }

        public Course(string courseShortName, string courseFullName, string courseSubject, string courselevel, string creditHrs, string courseType, string enrolledSemester, string gradesRecieved) : this(courseShortName, courseFullName, courseSubject, courselevel, creditHrs, courseType, enrolledSemester)
        {
            GradesRecieved = gradesRecieved;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Course);
        }

        public bool Equals(Course other)
        {
            return other != null &&
                   CourseShortName == other.CourseShortName &&
                   CourseFullName == other.CourseFullName &&
                   CourseSubject == other.CourseSubject &&
                   Courselevel == other.Courselevel &&
                   CreditHrs == other.CreditHrs &&
                   CourseType == other.CourseType &&
                   EnrolledSemester == other.EnrolledSemester &&
                   IsSelected == other.IsSelected &&
                   GradesRecieved == other.GradesRecieved;
        }

        public override int GetHashCode()
        {
            var hashCode = -629432352;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CourseShortName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CourseFullName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CourseSubject);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Courselevel);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CreditHrs);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CourseType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EnrolledSemester);
            hashCode = hashCode * -1521134295 + IsSelected.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GradesRecieved);
            return hashCode;
        }

        public static bool operator ==(Course course1, Course course2)
        {
            return EqualityComparer<Course>.Default.Equals(course1, course2);
        }

        public static bool operator !=(Course course1, Course course2)
        {
            return !(course1 == course2);
        }


    }
}