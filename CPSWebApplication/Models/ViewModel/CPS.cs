using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CPSWebApplication.Models.ViewModel
{
    public class CPS
    {
       
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentID { get; set; }
        public string AcademicYear { get; set; }
        public string Major { get; set; }
        public string CoreCourseDetails { get; set; }
        public string ElectiveCourseDetails { get; set; }
        public string ProgramCompletionType { get; set; }
        public string AssignedFoundationCourseDetails { get; set; }
        public string AssignedFacultyAdvioser { get; set; }
        public string AssignedAcademicAdvisor { get; set; }
        public string IsDraft { get; set; }
        public string IsActive { get; set; }
        public string IsFinalised { get; set; }
        public string IsAudited { get; set; }
        public string LastFinalizeDate { get; set; }
        public string LastDraftDate { get; set; }

        public string IsBlankCreated { get; set; }
        public string BlankCreatedDate{ get; set; }

        public string IsSpecialize { get; set; }
        public string SpecializaionUnder { get; set; }

        public string AcademicAdvisorSignature { get; set; }

        public CPS() {
        }

        public CPS(string firstName, string lastName, string studentID, string academicYear, string major, string coreCourseDetails, string electiveCourseDetails, string assignedFoundationCourseDetails, string assignedFacultyAdvioser) : this(firstName, lastName, studentID, academicYear, major, coreCourseDetails, electiveCourseDetails, assignedFoundationCourseDetails)
        {
            AssignedFacultyAdvioser = assignedFacultyAdvioser;
        }

        public CPS(string firstName, string lastName, string studentID, string academicYear, string major, string coreCourseDetails, string assignedFoundationCourseDetails, string assignedFacultyAdvioser)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentID = studentID;
            AcademicYear = academicYear;
            Major = major;
            CoreCourseDetails = coreCourseDetails;
            AssignedFoundationCourseDetails = assignedFoundationCourseDetails;
            AssignedFacultyAdvioser = assignedFacultyAdvioser;
        }

        public CPS(string firstName, string lastName, string studentID, string lastDraftDate, string blankCreatedDate)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentID = studentID;
            LastDraftDate = lastDraftDate;
            this.BlankCreatedDate = blankCreatedDate;
        }

        public CPS(string firstName, string lastName, string studentID, string academicYear, string major, string coreCourseDetails, string electiveCourseDetails, string assignedFoundationCourseDetails, string assignedFacultyAdvioser, string programCompletionType) : this(firstName, lastName, studentID, academicYear, major, coreCourseDetails, electiveCourseDetails, assignedFoundationCourseDetails)
        {
            AssignedFacultyAdvioser = assignedFacultyAdvioser;
        }

        public CPS(string firstName, string lastName, string studentID, string academic_Year, string major, string coreCourseDetails, string electiveCourseDetails, string foundationCourseDeatils, string assignedFacultyAdvisor, string programCompletionType, string lastDraftDate)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentID = studentID;
            AcademicYear = academic_Year;
            Major = major;
            CoreCourseDetails = coreCourseDetails;
            ElectiveCourseDetails = electiveCourseDetails;
            AssignedFoundationCourseDetails = foundationCourseDeatils;
            AssignedFacultyAdvioser = assignedFacultyAdvisor;
            ProgramCompletionType = programCompletionType;
            LastDraftDate = lastDraftDate;
        }

        public CPS(string firstName, string lastName, string studentID, string academicYear, string major, string coreCourseDetails, string electiveCourseDetails, string programCompletionType, string assignedFoundationCourseDetails, string assignedFacultyAdvioser, string lastFinalizeDate,string lastDraftDate, string isSpecialize, string specializaionUnder, string academicAdvisorSignature) : this(firstName, lastName, studentID, academicYear, major, coreCourseDetails, electiveCourseDetails, programCompletionType, assignedFoundationCourseDetails, assignedFacultyAdvioser, lastDraftDate)
        {
            LastFinalizeDate = lastFinalizeDate;
            IsSpecialize = isSpecialize;
            SpecializaionUnder = specializaionUnder;
            AcademicAdvisorSignature = academicAdvisorSignature;
        }
    }
}