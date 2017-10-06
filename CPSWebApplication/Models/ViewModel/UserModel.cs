using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CPSWebApplication.Models.ViewModel
{
    public class UserModel
    {
        [Key]
        public string UHCLID { get; set; }
        public string UHCLIDPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UHCLEmail { get; set; }
        public string UHCLEmailPassword { get; set; }
        public string ContactNumber { get; set; }
        public String UserAddress { get; set; }

    }
}