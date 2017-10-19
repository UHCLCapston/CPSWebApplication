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
        [Display(Name = "Enter Student ID to Design CPS")]
        public string searchId { get; set; }

       

        [Required]
        [Display(Name = "Select the foundation classes to assign student")]
        public List<Course> FoundationClassesList { get; set; }

        [Required]
        [Display(Name = "Make sure below core classes")]
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