using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CPSWebApplication.Models.ViewModel
{
    public class UserLoginView
    {
        [Key]
        [Required(ErrorMessage ="*")]
        [Display(Name ="Login ID")]
        public string UHCLEmail { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UHCLEmailPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
        public string UserRole { get; set; }
    }
}