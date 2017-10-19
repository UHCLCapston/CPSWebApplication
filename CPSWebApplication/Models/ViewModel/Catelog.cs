using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CPSWebApplication.Models.ViewModel
{
    public class Catelog16_17
    {
        [Key]
        public string Course { get; set; }
        public string SWEN { get; set; }
        public string CSCI { get; set; }
        public string SENG { get; set; }

        public string Subject { get; set; }
        public string Catalog { get; set; }
        public string LongTitle { get; set; }
        public String CreditHrs { get; set; }


    }
}