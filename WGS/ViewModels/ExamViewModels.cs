using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WGS.Models;

namespace WGS.ViewModels
{
    public class CreateExamViewModel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        [Display(Name="Activate Immediately?")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name="Starting Date")]
        [DataType(DataType.DateTime)]
        public DateTime DateFrom { get; set; }
        [Required]
        [Display(Name = "Ending Date")]
        [DataType(DataType.DateTime)]
        public DateTime DateTo { get; set; }
        [Required]
        [Display(Name = "Level for exam")]
        public SelectList Levels => new SelectList(Helpers.Context.Levels.ToList(), "Name", "Name");
        public String LevelName { get; set; }
    }

    public class ChooseExamLevelViewModel
    {
        public SelectList Levels => new SelectList(Helpers.Context.Exams.ToList(),"Name","Name" );
        public String LevelName { get; set; }
    }

    public class AddExamEnrollmentsViewModel
    {

    }

    public class AddQuestionsToExamViewModel
    {
    }

}