using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;
using WGS.Models;
using WGS.ViewModels;

namespace WGS.Views
{
    public class RegistrationController : Controller
    {

        public ActionResult RegisterInstructor()
        {

            return View();
        }


        public ActionResult RegisterNewStudent()
        {
            return View();
        }

        public ActionResult RegisterExistingStudent()
        {
            ExistingStudentRegistrationViewModel vm = new ExistingStudentRegistrationViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult RegisterExistingStudent(ExistingStudentRegistrationViewModel model)
        {
            if (ModelState.IsValid == false) return View(model);
            var level = Helpers.Context.Levels.Include(c => c.Students).FirstOrDefault(l => l.Name == model.LevelName);
            var password = Guid.NewGuid().ToString().Split('-').FirstOrDefault();
            var student = new Student()
            {
                Email = String.Concat(model.GrNo, "@wgs.pk"),
                FirstName = model.FirstName,
                LastName = model.LastName,
                GrNo = model.GrNo,
                UserName = model.GrNo,
                Password = password
            };

            IdentityResult rs = Helpers.ApplicationUserManager.Create(student, password);
            //var s = Helpers.ApplicationUserManager.Find(userName: model.GrNo, password: password) as Student;

            if (rs == IdentityResult.Success)
            {
                level?.Students.Add(student);
                Helpers.Context.SaveChanges();
                ViewBag.Message = "Registration Successfull";
                ExistingStudentRegistrationViewModel vm = new ExistingStudentRegistrationViewModel();
                return View(vm);
            }

            rs.Errors.ForEach(r => ModelState.AddModelError(String.Empty, r));
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterInstructor(InstructorRegistrationViewModel model)
        {
            var password = Guid.NewGuid().ToString().Split('-').FirstOrDefault();
            var username = model.Identifier;
            var email = String.Concat(model.Identifier, "@wgs.pk");
            Instructor instructor = new Instructor()
            {
                Identifier = model.Identifier,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = username,
                Email = email,
                Password = password
            };

            IdentityResult rs = Helpers.ApplicationUserManager.Create(instructor, password);
            if (rs == IdentityResult.Success)
            {
                ViewBag.Message = "Registration Successful";
                return View();
            }

            rs.Errors.ForEach(e => ModelState.AddModelError(string.Empty, e));
            return View();
        }
    }
}