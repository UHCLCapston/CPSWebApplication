using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CPSWebApplication.Models.ViewModel
{
    public class CSCI_Courses
    {
        [Key]

        public string Course { get; set; }
        public string CourseName { get; set; }
        public string CourseLevel { get; set; }

        public string CreditHrs { get; set; }

        public string UnderSpecilization { get; set; }

    }
}