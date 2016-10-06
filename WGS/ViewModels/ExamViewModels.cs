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
        [Display(Name = "Activate Immediately?")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "Starting Date")]
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
        public SelectList Levels => new SelectList(Helpers.Context.Exams.ToList(), "Name", "Name");
        public String LevelName { get; set; }
    }

    public class AddExamEnrollmentsViewModel
    {

    }

    public class AddQuestionsToExamViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
        public String ExamId { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Enter Question Text")]
        public String QuestionText { get; set; }
        public List<Choice> Choices { get; set; }

    }

    public class AddChoiceToQuestionViewModel
    {
        public IEnumerable<Choice> Choices { get; set; }
        public String QuestionId { get; set; }

        [Required(ErrorMessage = "Text field cannot be empty")]
        [Display(Name = "Enter Choice")]
        public String ChoiceText { get; set; }

        [Display(Name = "Is it the right choice?")]
        public Boolean IsCorrect { get; set; }
        public String Reason { get; set; }
        public String ExamId { get; set; }
    }
}