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
        // GET: Questions

        
        public ActionResult ListQuestions(String ExamId)
        {
            var questionRepository = new GenericDataRepository<Question>();
            var questions =
                questionRepository.GetList(q => q.Exam == Helpers.Context.Exams.FirstOrDefault(e => e.Id == ExamId), q => q.Choices);
            return View(questions);
        }

        public ActionResult AddQuestion(String ExamId)
        {
            AddQuestionsToExamViewModel vm = new AddQuestionsToExamViewModel() {ExamId = ExamId};
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddQuestion(AddQuestionsToExamViewModel vm)
        {
            
        }

        public ActionResult AddChoicesToQuestion(String QuestionId)
        {
            
        }

        [HttpPost]
        public ActionResult AddChoicesToQuestion(AddChoiceToQuestionViewModel vm)
        {

        }
    }
}