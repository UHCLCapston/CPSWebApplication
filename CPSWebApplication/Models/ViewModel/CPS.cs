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

    }
}