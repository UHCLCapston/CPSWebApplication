using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CPSWebApplication.Models.ViewModel
{
    public class FacultyDetails
    {
        [Key]
        public string FacultyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string AdvisorDepartment { get; set; }
        public string AdvisingStudentList { get; set; }
    }
}