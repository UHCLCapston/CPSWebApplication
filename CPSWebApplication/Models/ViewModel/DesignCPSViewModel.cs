using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CPSWebApplication.Models.ViewModel
{
    public class DesignCPSViewModel
    {

        [Required]
        [Display(Name = "Student ID")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Major Name")]
        public string majorName { get; set; }

        [Required]
        [Display(Name = "Academic Year")]
        public string academicYear { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public string searchId { get; set; }
        public List<Course> FoundationClassesList { get; set; }

        public List<Course> CoreClassesList { get; set; }

        public string Message { get; set; }

        public List<Course> ElectiveClassesList { get; set; }

        [Required]
        [Display(Name = "Faculty Advisor" )]
        public string assignedFaculty { get; set; }

        public List<String> DfacultiesList { get; set; }

        public List<String> ProgramCompletionOptionList { get; set; }

        public List<String> DepartmentListForElective { get; set; }
        public List<String> CourseLevelSelectionListForElective { get; set; }

        public List<String> CourseShortNameListForElective { get; set; }

        public List<String> CourseLongTitleListForElective { get; set; }
        public List<String> CourseWholeNameListForElective { get; set; }
        public List<String> CourseCreditHrsListForElective { get; set; }

        public List<String> CourseGradeList { get; set; }
        public List<String> CourseSemesterList { get; set; }
        public string programCompletionOption { get; set; }

        public string ElectiveSubject { get; set; }
        public string ElectiveLevel { get; set; }
        public string ElectiveWholeName { get; set; }
        public string ElectiveCreditHrs { get; set; }
        public string ElectiveSemester { get; set; }
        public string ElectiveGrade { get; set; }


        //To show studentList with cps
        public List<CPS> cpsList { get; set; }

        public DesignCPSViewModel()
        {
        }

        public DesignCPSViewModel(List<Course> foundationClassesList, List<Course> coreClassesList)
        {
            FoundationClassesList = foundationClassesList;
            CoreClassesList = coreClassesList;
        }

        public DesignCPSViewModel(string searchId)
        {
            this.searchId = searchId;
        }

        public DesignCPSViewModel(string searchId, List<Course> foundationClassesList, List<Course> coreClassesList) : this(searchId)
        {
            FoundationClassesList = foundationClassesList;
            CoreClassesList = coreClassesList;
        }
    }
}