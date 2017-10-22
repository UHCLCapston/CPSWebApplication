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