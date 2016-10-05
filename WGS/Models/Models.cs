using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using FluentBootstrap;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WGS.Models
{
    public class Instructor : AppUser
    {
        public String Identifier { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Level> Levels { get; set; }


    }
    public class Student : AppUser
    {
        public String GrNo { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Level> Levels { get; set; }
    }

    public class AppUser : IdentityUser
    {
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

    }
    public class Level
    {
        [Key]
        public string Name { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<AppUser> Users { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
    public class Exam
    {
        
        public String Id { get; set; }
        public String Name { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Level Level { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

    }
    public class Enrollment
    {
     
        public String Id { get; set; }
        public AppUser AppUser { get; set; }
        public Exam Exam { get; set; }
    }
    public class Question
    {
        
        public String Id { get; set; }
        public String QuestionText { get; set; }
        public ICollection<Choice> Choices { get; set; }
        public Exam Exam { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Instructor GivenBy { get; set; }
    }
    public class Choice
    {

        public String Id { get; set; }
        public String ChoiceText { get; set; }
        public Question Question { get; set; }
        public Boolean IsCorrect { get; set; }
        public String Reason { get; set; }
    }
    public class Answer
    {
       public String Id { get; set; }
        public String AnswerText { get; set; }
        public Question Question { get; set; }
        public Student GivenBy { get; set; }
    }
}