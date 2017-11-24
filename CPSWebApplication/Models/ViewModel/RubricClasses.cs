using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPSWebApplication.Models.ViewModel
{
    public class RubricClasses
    {
        public string rubric { get; set; }
        public List<string> classes { get; set; }

        public RubricClasses() { }

        public RubricClasses(string rubric, List<string> classes)
        {
            this.rubric = rubric;
            this.classes = classes;
        }

        public RubricClasses(string rubric)
        {
            this.rubric = rubric;
        }


    }
}