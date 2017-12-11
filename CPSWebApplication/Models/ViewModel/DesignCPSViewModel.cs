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
        [Display(Name = "First Name")]
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

        public List<Course> CapstonElectiveClassesList { get; set; }

        public List<Course> ThesisElectiveClassesList { get; set; }

        public List<Course> CapstonElectiveSpecialClassesList { get; set; }

        public List<Course> ThesisElectiveSpecialClassesList { get; set; }


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
        public List<List<String>> ListOFCourseWholeNameListElective { get; set; }
        public List<String> CourseCreditHrsListForElective { get; set; }

        public List<String> CourseGradeList { get; set; }
        public List<String> CourseSemesterList { get; set; }

        public List<String> LevelGroupOption { get; set; }

        public List<String> CourseSubjectLevelRubricSelectionOption { get; set; }

        [Required]
        [Display(Name = "Program Completion Options")]
        public string programCompletionOption { get; set; }

        public string ElectiveSubject { get; set; }
        public string ElectiveLevel { get; set; }
        public string ElectiveWholeName { get; set; }
        public string ElectiveCreditHrs { get; set; }
        public string ElectiveSemester { get; set; }
        public string ElectiveGrade { get; set; }

        public int countElectives { get; set; }

        public int countElectivesCapston { get; set; }

        public int countElectivesThesis { get; set; }

        public Course CapstonCourse { get; set; }
        public List<Course> ThesisCourse { get; set; }

        public List<RubricClasses> ClassesForCapstonNormal { get; set; }
        public List<RubricClasses> ClassesForThesisNormal { get; set; }
        public List<RubricClasses> ClassesForCapstonSpecial { get; set; }
        public List<RubricClasses> ClassesForThesisSpecial { get; set; }
     
        public List<string> ListSpecializationOption { get; set; }
        [Required]
        [Display(Name = "Choose Specialization")]
        public string SpecializationType { get; set; }

        [Required]
        [Display(Name = "Specialization")]
        public bool SpecializationSelected { get; set; }
        //To show studentList with cps
        [Required]
        [Display(Name = "Academic Advisor")]
        public string SignatureAcademicAdvisor { get; set; }

        [Display(Name = "Signed Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SignedDate { get; set; }

        public List<CPS> cpsList { get; set; }

        public string AllowAcademic { get; set; }
        public string NeedModificationFromFaculty { get; set; }
        public string SaveCPSAcademic { get; set; }
        public string FinalizeCPSAllow { get; set; }
        public List<StudentDetails> listNewStudent { get; set; }
        [Display(Name = "Student Full Name")]
        public string StudentName { get; set; }

        [Display(Name = "Student Signature")]
        public string StudentSignature { get; set; }

        [Display(Name = "Faculty Signature")]
        public string FacultySignature { get; set; }

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