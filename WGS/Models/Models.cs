using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluentBootstrap;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WGS.Models
{
    public class Instructor : User
    {
        public IEnumerable<Question> Questions { get; set; }

        public Instructor(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
        }
    }
    public class Student : User
    {
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

        public Student(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
        }
    }

    public class User : IdentityUser
    {
        private ClaimsPrincipal claimsPrincipal;

        public User(ClaimsPrincipal claimsPrincipal)
        {
            this.claimsPrincipal = claimsPrincipal;
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
     
    }

    public class Level
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public IEnumerable<Exam> Exams { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
    public class Exam
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Level Level { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }

    }
    public class Enrollment
    {
        public String Id { get; set; }
        public User User { get; set; }
        public Exam Exam { get; set; }
    }
    public class Question
    {
        public String Id { get; set; }
        public String QuestionText { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
        public Exam Exam { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
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