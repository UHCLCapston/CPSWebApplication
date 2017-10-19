using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CPSWebApplication.Models.ViewModel
{
    public class DesignCPSViewModel
    {
        
        public string lastName { get; set; }
        public string majorName { get; set; }
        public string academicYear { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public string searchId { get; set; }
        public List<Course> FoundationClassesList { get; set; }

        public List<Course> CoreClassesList { get; set; }

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