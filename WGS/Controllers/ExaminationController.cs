using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using WGS.Models;
using WGS.ViewModels;

namespace WGS.Controllers
{
    public class ExaminationController : Controller
    {
        public ActionResult ExamHome() => View();
        // GET: Exam
        public ActionResult CreateExam() => View(new CreateExamViewModel());

        public ActionResult ListExam() => View(Helpers.Context.Exams.Include(e => e.Level).ToList());

        [HttpPost]
        public ActionResult CreateExam(CreateExamViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Exam exam = new Exam()
            {
                Id=Guid.NewGuid().ToString(),
                Name = model.Name,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                IsActive = model.IsActive
            };

            var Level = Helpers.Context.Levels.FirstOrDefault(l => l.Name == model.LevelName);
            if (
                Helpers.Context.Exams.Any(
                    e => e.Name == model.Name &&
                    e.DateFrom == model.DateFrom &&
                    e.DateTo == model.DateTo))
            {
                ModelState.AddModelError(string.Empty, "This exam already exists.");
                return View();
            }
            exam.Level = Level;
            Helpers.Context.Exams.Add(exam);
            Helpers.Context.SaveChanges();
            ViewBag.Message = "Exam successfully added.";
            return View(new CreateExamViewModel());
        }

      
    }
}