using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using WGS.Models;
using WGS.ViewModels;

namespace WGS.Controllers
{
    public class QuestionsController : Controller
    {
        public ActionResult ListQuestions(String ExamId)
        {
            if (String.IsNullOrEmpty(ExamId))
            {
                ViewBag.Error = "Exam parameter missing."; return View("Error");
            }
            var questionRepository = new GenericDataRepository<Question>();
            var examRepository = new GenericDataRepository<Exam>();
            var exam = examRepository.GetSingle(e => e.Id == ExamId, Exam => Exam.Questions);

            var questions = exam?.Questions;
            var QuestionVm = new AddQuestionsToExamViewModel() { Questions = questions, ExamId = ExamId};
            return View(QuestionVm);
        }
        
        [HttpPost]
        public ActionResult AddQuestion(AddQuestionsToExamViewModel vm, String ExamId)
        {
            if (String.IsNullOrEmpty(ExamId))
            {
                ViewBag.Error = "Exam parameter missing."; return View("Error");
            }
            var exam = new GenericDataRepository<Exam>().GetSingle(e => e.Id == vm.ExamId);

            Question question = new Question() { QuestionText = vm.QuestionText, Id = Guid.NewGuid().ToString(), Exam = exam };

            var QuestionRepository = new GenericDataRepository<Question>();
            QuestionRepository.Add(question);

            return RedirectToAction(actionName: nameof(ListQuestions), routeValues: new { @ExamId = vm.ExamId });
        }


        public ActionResult AddChoicesToQuestion(String QuestionId, String ExamId)
        {
            var question = new GenericDataRepository<Question>().GetSingle(e => e.Id == QuestionId, e => e.Choices);
            AddChoiceToQuestionViewModel vm = new AddChoiceToQuestionViewModel() { ExamId = ExamId, QuestionId = QuestionId, Choices = question.Choices };
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult AddChoicesToQuestion(AddChoiceToQuestionViewModel vm)
        {
            var questionRepository = new GenericDataRepository<Question>();
            var question = questionRepository.GetSingle(q => q.Id == vm.QuestionId, q => q.Choices);
            var choice = new Choice() { Id = Guid.NewGuid().ToString(), ChoiceText = vm.ChoiceText, IsCorrect = vm.IsCorrect, Reason = vm.Reason };
            var choiceExists = new GenericDataRepository<Choice>().ChechIfExists(c=>c.ChoiceText==vm.ChoiceText);
            question.Choices.Add(choice);
            questionRepository.Update(question);
            return RedirectToAction(actionName: "ListChoicesForQuestion", routeValues: new {vm.QuestionId, vm.ExamId});
        }

        public ActionResult ListChoicesForQuestion(String QuestionId, String ExamId)
        {
            AddChoiceToQuestionViewModel vm = new AddChoiceToQuestionViewModel();

            var question = new GenericDataRepository<Question>().GetSingle(e => e.Id == QuestionId, q => q.Choices);
            vm.QuestionId = QuestionId;
            vm.ExamId = ExamId;
            vm.Choices = question.Choices;
            return PartialView(vm);
        }
    }
}