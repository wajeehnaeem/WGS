using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WGS.Models;

namespace WGS.ViewModels
{
    public class ExistingStudentRegistrationViewModel
    {
        
        [Required]
        [Display(Name="GR Number")]
        public String GrNo { get; set; }
        [Required]
        [Display(Name="First Name")]
        public String FirstName { get; set; }
        [Required]
        [Display(Name="Last Name")]
        public String LastName { get; set; }
        public SelectList Levels => new SelectList(Helpers.Context.Levels.ToList(), "Name", "Name");
        [Required]
        [Display(Name="Level Name")]
        public String LevelName{ get; set; }
    }

    public class NewStudentRegistrationViewModel
    {
        public SelectList Levels => new SelectList(Helpers.Context.Levels.ToList(), "Name", "Name");
        public String FirstName { get; set; }
        public String LastName { get; set; }
       public String LevelName { get; set; }
    }

    public class InstructorRegistrationViewModel
    {
        [Required]
        [Display(Name="ID")]
        public String Identifier { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public String FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public String LastName { get; set; }

    }
}